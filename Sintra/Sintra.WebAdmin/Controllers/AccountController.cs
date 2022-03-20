using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sintra.Application.DTOs.Account;
using Sintra.Application.DTOs.Email;
using Sintra.Application.Interfaces;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sintra.WebAdmin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailService emailService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAccountService accountService;

        public AccountController(UserManager<ApplicationUser> userManager, IEmailService emailService,
            SignInManager<ApplicationUser> signInManager, IAccountService accountService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.signInManager = signInManager;
            this.accountService = accountService;
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            /*await accountService.ForgotPassword(model, Request.Headers["origin"]);*/

            var account = await userManager.FindByEmailAsync(model.Email);

            // always return ok response to prevent email enumeration
            /*if (account == null) return;*/

            var code = await userManager.GeneratePasswordResetTokenAsync(account);

            string url = "/Account";
            url += Url.Action("ResetPassword", "Account", new
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

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ResetPasswordRequest model = new ResetPasswordRequest
            {
                Token = token
            };
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            await accountService.ResetPassword(model);
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(WebAuthenticationRequest model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(model.Email);
                if (user.IsBlocked != true)
                {
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Invalid login attempt");
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest model)
        {

            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                IsBlocked = true
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

    }
}
