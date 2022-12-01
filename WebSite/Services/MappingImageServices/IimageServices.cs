using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models;

namespace WebSite.Services.MappingImageServices
{
    public interface IimageServices
    {
        Task<IdentityResult> DeleteAsync(DataImage image);

        Task<IdentityResult> CreateAsync(DataImage dataImage);

        Task<DataImage> FindByUrlAsync(string url);

        Task<IdentityResult> DeleteFormImagesAsync(IEnumerable<DataImage> image);     
    }
}
