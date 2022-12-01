using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Pages.NavModels
{
    public class NavbarTop
    {
        public static string Home => "Home";

        public static string CategoryManager => "CategoryManager";


        public static string Docs => "Docs";

        public static string Personal => "Personal";

        public static string HomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Home);
        public static string CategoryManagerNavClass(ViewContext viewContext) => PageNavClass(viewContext, CategoryManager);
        public static string DocsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Docs);
        public static string PersonalNavClass(ViewContext viewContext) => PageNavClass(viewContext, Personal);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageTop"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active-menu-top" : null;
        }

      
        
    }
}
