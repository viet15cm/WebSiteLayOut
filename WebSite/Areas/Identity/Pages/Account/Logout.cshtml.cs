using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebSite.Models.Identity;

namespace WebSite.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<AppUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        public string ReturnUrl { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
            
            await _signInManager.SignOutAsync();
          

            if (ReturnUrl != null)
            {
                return LocalRedirect(ReturnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}

