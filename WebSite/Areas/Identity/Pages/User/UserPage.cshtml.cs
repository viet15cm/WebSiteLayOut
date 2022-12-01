using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebSite.DbContextLayer;
using WebSite.Models.Identity;

namespace WebSite.Areas.Identity.Pages.User
{
    //[Authorize(Roles = "Admin")]
    public class UserPageModel : PageModel
    {

        protected readonly UserManager<AppUser> _userManager;
        protected readonly ILogger<UserPageModel> _logger;
        protected readonly IdentityStoreServices _dbContext;

        protected readonly RoleManager<IdentityRole> _roleManager;

        [TempData]
        public string StatusMessage { get; set; } 
        public UserPageModel(UserManager<AppUser>  userManager, ILogger<UserPageModel> logger , IdentityStoreServices dbContext , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _logger = logger;
            _dbContext = dbContext;
            _roleManager = roleManager;
        }
        
    }
}
