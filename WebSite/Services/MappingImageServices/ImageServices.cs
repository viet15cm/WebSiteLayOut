using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.DbContextLayer;
using WebSite.Models;
using WebSite.Services.IdentityErrors;
using WebSite.Services.MappingImageServices;

namespace WebSite.Services.MappingImageServices
{
    public class ImageServices : IimageServices
    {
        private readonly DbContextLayer.IdentityStoreServices _context;

        private readonly AppIdentityErrorDescriber _identityErrorDescriber;

        public ImageServices(DbContextLayer.IdentityStoreServices appDbContext , AppIdentityErrorDescriber  identityErrorDescriber)
        {
            _context = appDbContext;
            _identityErrorDescriber  = identityErrorDescriber;
        }

        public async Task<IdentityResult> CreateAsync(DataImage dataImage)
        {
            if (dataImage == null)
            {
                return IdentityResult.Failed(_identityErrorDescriber.DataNullErorr());
            }

            try
            {
                var image = new DataImage()
                {
                    Url = dataImage.Url,
                    PostId = dataImage.PostId
                };

                await _context.AddAsync(image);

                await _context.SaveChangesAsync();

                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed(_identityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IdentityResult> DeleteAsync(DataImage image)
        {
        
            if (image == null)
            {
                return IdentityResult.Failed(_identityErrorDescriber.DataNullErorr());
            }

            try
            {
                _context.DataImages.Remove(image);

                await _context.SaveChangesAsync();

                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed(_identityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<DataImage> FindByUrlAsync(string url)
        {
            return await _context.DataImages.Where(x => x.Url == url).FirstOrDefaultAsync();
        }

        public async Task<IdentityResult> DeleteFormImagesAsync(IEnumerable<DataImage> dataImages)
        {
            try
            {
                foreach (var image in dataImages)
                {
                    _context.DataImages.Remove(image);
                }

                await _context.SaveChangesAsync();

                return IdentityResult.Success;
            }
            catch (Exception)
            {
                return IdentityResult.Failed(_identityErrorDescriber.DatabaseAllErorr());
            }

        }
    }
}
