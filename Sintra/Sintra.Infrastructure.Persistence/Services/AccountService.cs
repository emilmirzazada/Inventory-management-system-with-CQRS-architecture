using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Ocsp;
using Sintra.Application.DTOs.Account;
using Sintra.Application.DTOs.Email;
using Sintra.Application.DTOs.Jwt;
using Sintra.Application.Enums;
using Sintra.Application.Exceptions;
using Sintra.Application.Interfaces;
using Sintra.Application.Wrappers;
using Sintra.Domain.Entities;
using Sintra.Domain.Settings;
using Sintra.Infrastructure.Persistence.Contexts;
using Sintra.Infrastructure.Persistence.Dapper;
using Sintra.Infrastructure.Persistence.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Cache;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Infrastructure.Persistence.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IdentityContext context;
        private readonly IDapper dapper;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        public AccountService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<string>> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            IdentityContext context,
            IDapper dapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            this._emailService = emailService;
            this.context = context;
            this.dapper = dapper;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            try
            {
                var user = context.Users.
                            Include(x => x.RefreshToken)
                            .Where(x => x.Email == request.Email)
                            .FirstOrDefault();
                if (user == null)
                {
                    throw new ApiException($"No Accounts Registered with {request.Email}.");
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    throw new ApiException($"Invalid Credentials for '{request.Email}'.");
                }
                JwtTokenDto jwtTokenDto = await GenerateJWToken(user);
                AuthenticationResponse response = new AuthenticationResponse();
                response.Id = user.Id;
                response.Jwt = jwtTokenDto;
                response.Email = user.Email;
                response.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Roles = rolesList.ToList();


                if (user.RefreshToken == null || user.RefreshToken.IsExpired)
                {
                    var refreshToken = GenerateRefreshToken(ipAddress);
                    user.RefreshToken = refreshToken;
                    context.Users.Update(user);
                    context.SaveChanges();
                    response.RefreshToken = new RefreshTokenDto(refreshToken.Token, refreshToken.Expires);
                }
                else
                {
                    response.RefreshToken = new RefreshTokenDto(user.RefreshToken.Token, user.RefreshToken.Expires);
                }

                return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                MinAmount = 10,
                Percentage = 1,
                MaxAmount = 100,
                IsBlocked = true
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    
                    return new Response<string>(user.Id, message: $"User Registered");
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
            else
            {
                throw new ApiException($"Email {request.Email } is already registered.");
            }
        }

        private async Task<JwtTokenDto> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            DynamicParameters p = new DynamicParameters();
            p.Add("UserId", user.Id);
            List<IdentityRole<string>> roles =  dapper.GetAll<IdentityRole<string>>
                    (@$"select r.* from roles r
                        left join UserRoles ur on ur.RoleId=r.Id
                        where ur.UserId=@UserId", p, CommandType.Text);

            var roleClaims = new List<Claim>();

            foreach (var item in roles)
            {
                roleClaims.AddRange(await _roleManager.GetClaimsAsync(item));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id),
                new Claim("ip", ipAddress)
            }
            .Union(roleClaims);


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var expires = _dateTimeService.NowUtc.AddMinutes(_jwtSettings.DurationInMinutes);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials);
            var tokentHandler = new JwtSecurityTokenHandler();
            string token = tokentHandler.WriteToken(jwtSecurityToken);
            return new JwtTokenDto(token,expires);
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "account/confirmemail/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }

        public async Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return new Response<string>(user.Id, message: $"Account Confirmed for {user.Email}. You can now use the /api/Account/authenticate endpoint.");
            }
            else
            {
                throw new ApiException($"An error occured while confirming {user.Email}.");
            }
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = _dateTimeService.NowUtc.AddMonths(6),
                Created = _dateTimeService.NowUtc,
                CreatedByIp = ipAddress
            };
        }

        public async Task<JwtTokenDto> RevokeByRefreshToken(string token)
        {
            var refreshToken = context.RefreshTokens.Where(x => x.Token == token).FirstOrDefault();
            var user = context.Users.Where(x => x.RefreshTokenId == refreshToken.Id).FirstOrDefault();
            JwtTokenDto jwtTokenDto = await GenerateJWToken(user);
            return new JwtTokenDto(jwtTokenDto.Token, jwtTokenDto.Expires);
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return;

            var code = await _userManager.GeneratePasswordResetTokenAsync(account);
            var route = "account/resetpassword/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var url = $"/account/resetpassword?userId={account.Id}&token={code}";

            /*var tsy = UrlHelper("ResetPassword", "Account", new
            {
                userId = account.Id,
                token = code
            });*/

            var emailRequest = new EmailRequest()
            {
                /*Body = $"You reset token is - {code}",*/
                Body= $"Lutfen email hesabinizi onaylamak icin linke <a href='https://localhost:5001{url}'>tiklayiniz.</a>",
                /*To = model.Email,*/
                To ="emilmeerzezade@gmail.com",
                Subject = "Reset Password",
            };
            await _emailService.SendAsync(emailRequest);
        }

        public async Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);
            if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");
            var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
            if (result.Succeeded)
            {
                return new Response<string>(model.Email, message: $"Password Resetted.");
            }
            else
            {
                throw new ApiException($"Error occured while reseting the password.");
            }
        }
    }

}
