using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Services.MappingFileServices
{
    public interface IFileServices
    {
        Task<IdentityResult> CreateFileAsync(IFormFile formFile, string UrlImage);
        Task<IdentityResult> CreateFileAvatarAsync(IFormFile formFile, string UrlImage);
        Task<IdentityResult> DeleteFileAsync(string UrlImg);

        Task<IdentityResult> DeleteFileAvatarAsync(string UrlImg);

        Task<IdentityResult> CreateFileBannerAsync(IFormFile formFile, string UrlImage);
        Task<IdentityResult> DeleteFileBannerAsync(string UrlImg);
    }
}
