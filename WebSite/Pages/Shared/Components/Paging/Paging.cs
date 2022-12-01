using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Pages.Shared.Components.Paging
{
    public class Paging : ViewComponent
    {
        public class PagingModel
        {
            public int CurentPage { get; set; }
            public int CountPages { get; set; }
            public Func<int?, string> generateUrl { get; set; }
        }

        public IViewComponentResult Invoke(PagingModel paging)
        {
            return View(paging);
        }

    }
}
 