using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.DTOs.Account;
using Sintra.Application.DTOs.Email;
using Sintra.Application.Interfaces;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintra.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailService emailService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAccountService _accountService;
        public AccountController(UserManager<ApplicationUser> userManager, IEmailService emailService,
            SignInManager<ApplicationUser> signInManager, IAccountService accountService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.signInManager = signInManager;
            _accountService = accountService;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request, GenerateIPAddress()));
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAsync(request, origin));
        }
        
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            var account = await userManager.FindByEmailAsync(model.Email);


            var code = await userManager.GeneratePasswordResetTokenAsync(account);

            string url = Url.Action("ResetPassword", "Account", new
            {
                userId = account.Id,
                token = code
            });

            var emailRequest = new EmailRequest()
            {
                Body = $"Şifrənizi yeniləmək üçün <a href='https://localhost:5001{url}'>linkə</a> klikləyin",
                To = model.Email,
                Subject = "Şifrəni bərpa et",
            };
            await emailService.SendAsync(emailRequest);
            return Ok();
        }
        /*[HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            return Ok(await _accountService.ResetPassword(model));
        }*/
        [HttpPost("revokeByRefreshToken")]
        public async Task<IActionResult> RevokeByRefreshToken([FromQuery] string refreshToken)
        {
            return Ok(await _accountService.RevokeByRefreshToken(refreshToken));
        }
        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}