// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SchoolSystem.Application;
using SchoolSystem.Application.Builders;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Data;
using static System.Net.Mime.MediaTypeNames;

namespace SchoolSystem.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext context,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public string DefaultBirthday { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]
            [StringLength(25, ErrorMessage = "This field can't exceed 25 character.")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(25, ErrorMessage = "This field can't exceed 25 character.")]
            public string LastName { get; set; }

            [Required]
            [RegularExpression(@"^\(\d{3}\) \d{3} \d{2} \d{2}$", ErrorMessage = "Invalid phone number format. Expected format: (999) 999 99 99")]
            public string PhoneNumber { get; set; }

            [Required]
            [DataType(DataType.DateTime)]
            [Display(Name = "Birth Day")]
            public DateTime BirthDay { get; set; }

            [StringLength(100, ErrorMessage = "This field can't exceed 100 character")]
            public string ProfilePictureURL { get; set; }

            [Required(ErrorMessage = "This field can't be null")]
            [StringLength(200, ErrorMessage = "This field can't exceed 200 character.")]
            public string Address { get; set; }

            public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
            public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
            public Guid CreatedByUserGuid { get; set; }
            public Guid UpdatedByUserGuid { get; set; }
            public bool IsActive { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }


            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }


            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Role { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> RoleList { get; set; }

            [Required]
            public Guid NationalityGuid { get; set; }

            [ValidateNever]
            public IEnumerable<SelectListItem> NationalityList { get; set; }

            [Required]
            public string Gender { get; set; }
            public IEnumerable<SelectListItem> GenderList { get; set; }
        }



        public async Task OnGetAsync(string returnUrl = null)
        {

            if (!await _roleManager.RoleExistsAsync(StaticUserRoles.Role_Admin))
            {
                await _roleManager.CreateAsync(new ApplicationRole(StaticUserRoles.Role_Student));
                await _roleManager.CreateAsync(new ApplicationRole(StaticUserRoles.Role_Admin));
                await _roleManager.CreateAsync(new ApplicationRole(StaticUserRoles.Role_Teacher));
                await _roleManager.CreateAsync(new ApplicationRole(StaticUserRoles.Role_Parent));
            }

            Input = new()
            {

                RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                }),
                NationalityList = _context.Nationalities.Select(x => new SelectListItem
                {
                    Value = x.NationalityGuid.ToString(),
                    Text = x.Nationality
                }),
                GenderList = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "M",
                        Value = "Male"
                    },
                    new SelectListItem
                    {
                        Text = "F",
                        Value = "Female"
                    }
                }

            };

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            if (ModelState.IsValid)
            {
               
                ApplicationUser user = new UserBuilder()
                    .SetFirstName(Input.FirstName)
                    .SetLastName(Input.LastName)
                    .SetAddress(Input.Address)
                    .SetBirthDate(Input.BirthDay)
                    .SetGender(Input.Gender)
                    .SetProfilePictureURL(Input.ProfilePictureURL)
                    .SetNationalityGuid(Input.NationalityGuid)
                    .SetPhoneNumber(Input.PhoneNumber)
                    .SetCreatedByUserGuid(Input.CreatedByUserGuid)
                    .SetUpdatedByUserGuid(Input.UpdatedByUserGuid)
                    .SetIsActive(Input.IsActive)
                    .BuildUser();

                string userName = $"{Input.FirstName}{Input.LastName}";

                await _userStore.SetUserNameAsync(user, userName, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (!String.IsNullOrEmpty(Input.Role))
                    {
                        await _userManager.AddToRoleAsync(user, Input.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, StaticUserRoles.Role_Student);
                    }

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
