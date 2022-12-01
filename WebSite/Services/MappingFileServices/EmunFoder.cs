using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Services.MappingFileServices
{
    public class EmunFoder
    {

        public static string DefaultForderRootDirectory = "ImageManager";
        public static string DefaultForderImage = "PostImage";
        public  string ForderRootDirectory { get; set; }

        public string ForderImage  { get; set; }
    }
}
