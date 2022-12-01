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

using WebSite.Areas.Identity.Pages.User;
using WebSite.DbContextLayer;
using WebSite.Models.Identity;
using WebSite.Pages.Shared.Components.Paging;
using static WebSite.Pages.Shared.Components.Paging.Paging;

namespace WebSite.Areas.Identity.Pages.User
{

    public class IndexModel : UserPageModel
    {
        // Moi trang hien thi 5 phan tu
        public static int ITEMS_PER_PAGE = 5;

        [BindProperty]
        public string SearchUserName { get; set; }
        
        [BindProperty]
        public PagingModel pagingModel { get; set; }
        
        public IndexModel(UserManager<AppUser> userManager, ILogger<UserPageModel> logger, IdentityStoreServices dbContext , RoleManager<IdentityRole> roleManager) : base(userManager, logger, dbContext, roleManager)
        {

        }

        public List<UserAndRole> users { get; set; }

       public class UserAndRole : AppUser
        {
            public string UserRoles { get; set; }
        }

        public async Task<IActionResult> OnGet([FromQuery] int pageNumber)
        {
            var list = await _userManager.Users.OrderBy(u => u.UserName).ToListAsync();

            users =  list.Select(u => new UserAndRole
            { 
                Id = u.Id,
                UserName = u.UserName
            }).ToList();

            foreach (var item in users)
            {
                var roles = await _userManager.GetRolesAsync(item);
                item.UserRoles = string.Join(",", roles);
                
            }


            // code phân trang 
            var countPages = users.Count();
            var CurentPage = pageNumber;
            if (pageNumber == 0)
            {
                CurentPage = 1;
            }

          
            Func<int?, string> generateUrl = (int? _pagenumber) => {
                return Url.Page("./index", new { pageNumber = _pagenumber });
            };

            pagingModel = new Paging.PagingModel
            {
                CurentPage = CurentPage,    // trang hiện tại
                CountPages = countPages,   // tổng  số trang 
                generateUrl = generateUrl
            };

            var totalItems = countPages;
            // Tính số trang hiện thị (mỗi trang hiện thị ITEMS_PER_PAGE mục do bạn cấu hình = 10, 20 ...)
            int totalPages = (int)Math.Ceiling((double)totalItems / ITEMS_PER_PAGE);
            // Lấy phần tử trong  hang hiện tại (pageNumber là trang hiện tại - thường Binding từ route)
             users = users.Skip(ITEMS_PER_PAGE * (pageNumber - 1)).Take(ITEMS_PER_PAGE).ToList();

            return Page();

        }

    }
}
