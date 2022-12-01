using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Services.MappingFileServices
{
    public class ImageExtend
    {
        public static string FoderImg = "FileImg";
        public static string FoderPost = "PostImg";

        public static string PathRepresentWebRootPath(IWebHostEnvironment webHostEnvironment , EmunFoder emunFoder)
        {
            if (emunFoder != null)
            {
                return Path.Combine(webHostEnvironment.WebRootPath, emunFoder.ForderRootDirectory, emunFoder.ForderImage);
            }

            return null;
        }

        public static string DefaultPathRepresentWebRootPath(IWebHostEnvironment webHostEnvironment)
        {
            return Path.Combine(webHostEnvironment.WebRootPath, EmunFoder.DefaultForderRootDirectory, EmunFoder.DefaultForderImage);
        }

        public static string PathRepresentContentRootPath(IWebHostEnvironment webHostEnvironment,  EmunFoder emunFoder)
        {
            if (emunFoder != null)
            {
                return Path.Combine(webHostEnvironment.ContentRootPath, emunFoder.ForderRootDirectory, emunFoder.ForderImage);
            }

            return null;
        }

        public static string PathImgSrcIndex(string UrlImg, EmunFoder emunFoder)
        {
            if (UrlImg != null && emunFoder != null)
            {
                return string.Format("{0}/{1}/{2}", emunFoder.ForderRootDirectory, emunFoder.ForderImage, UrlImg);
            }
            return null;
        }

        public static string DefaultPathImgSrcIndex(string UrlImg)
        {       
                return string.Format("{0}/{1}/{2}", EmunFoder.DefaultForderRootDirectory, EmunFoder.DefaultForderImage, UrlImg);
        }

        public static string HttpContextAccessorPathImgSrcIndex(IHttpContextAccessor httpContextAccessor, string UrlImg, EmunFoder emunFoder)
        {
            if (UrlImg != null && emunFoder != null)
            {
                return string.Format("{0}://{1}/{2}/{3}/{4}",httpContextAccessor.HttpContext.Request.Scheme, httpContextAccessor.HttpContext.Request.Host.ToString(), emunFoder.ForderRootDirectory, emunFoder.ForderImage, UrlImg);
            }
            return null;
        }

        public static string DefaultHttpContextAccessorPathImgSrcIndex(IHttpContextAccessor httpContextAccessor, string UrlImg)
        {
            if (UrlImg != null)
            {
                return string.Format("{0}://{1}/{2}/{3}/{4}", httpContextAccessor.HttpContext.Request.Scheme, httpContextAccessor.HttpContext.Request.Host.ToString(), EmunFoder.DefaultForderRootDirectory, EmunFoder.DefaultForderImage, UrlImg);
            }
            return null;
        }
    }
}
