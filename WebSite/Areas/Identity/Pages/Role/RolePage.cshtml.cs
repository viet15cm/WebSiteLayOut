using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebSite.DbContextLayer;

namespace WebSite.Areas.Identity.Pages.Role
{
    //[Authorize(Policy = "Admin")]
    public class RolePageModel : PageModel
    {
        protected readonly RoleManager<IdentityRole> _roleManager;

        protected readonly IdentityStoreServices _context;
        protected readonly ILogger<RolePageModel> _logger;


        [TempData]
        public string StatusMessage { get; set; }

        public RolePageModel(RoleManager<IdentityRole> roleManager, ILogger<RolePageModel> logger , IdentityStoreServices context)
        {
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
        }

    }
}
