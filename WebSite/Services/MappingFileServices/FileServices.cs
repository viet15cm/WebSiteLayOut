using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebSite.DbContextLayer;
using WebSite.Services.IdentityErrors;

namespace WebSite.Services.MappingFileServices
{
    public class FileServices : IFileServices
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppIdentityErrorDescriber _appIdentityErrorDescriber;

        private readonly DbContextLayer.IdentityStoreServices _context;

        public FileServices( IWebHostEnvironment webHostEnvironment , AppIdentityErrorDescriber appIdentityErrorDescriber , DbContextLayer.IdentityStoreServices dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _appIdentityErrorDescriber = appIdentityErrorDescriber;
            _context = dbContext;
            EmunFoder = new EmunFoder()
            {
                ForderImage = "PostImage",
                ForderRootDirectory = "ImageManager"
            };

            EmunFoderAvatar = new EmunFoder()
            {
                ForderImage = "Avatar",
                ForderRootDirectory = "ImageManager"
            };
            EmunFoderBanner = new EmunFoder()
            {
                ForderImage = "Banner",
                ForderRootDirectory = "ImageManager"
            };

        }
        private EmunFoder EmunFoder { get; set; }

        private EmunFoder EmunFoderAvatar { get; set; }
        private EmunFoder EmunFoderBanner { get; set; }
        public async Task<IdentityResult> CreateFileAsync(IFormFile formFile, string UrlImage)
        {
            try
            {
                var IsDuplicate = await _context.DataImages.AnyAsync(x => x.Url == UrlImage);

                if (IsDuplicate)
                {
                    return IdentityResult.Failed(_appIdentityErrorDescriber.DuplicateUrlErorr());
                }

                if (!Directory.Exists(ImageExtend.PathRepresentWebRootPath(_webHostEnvironment, EmunFoder)))
                {
                    Directory.CreateDirectory(ImageExtend.PathRepresentWebRootPath(_webHostEnvironment, EmunFoder));
                }

                var uniqueFileName = UrlImage;

                var uploads = ImageExtend.PathRepresentWebRootPath(_webHostEnvironment, EmunFoder);

                var filePath = Path.Combine(uploads, uniqueFileName);

                using (var stream = File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }

                return IdentityResult.Success;

            }
            catch (DbUpdateException)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DbUpdateErorr());
            }
            catch (Exception)
            {

                return IdentityResult.Failed(_appIdentityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IdentityResult> CreateFileAvatarAsync(IFormFile formFile, string UrlImage)
        {
            try
            {
                var IsDuplicate = await _context.DataImages.AnyAsync(x => x.Url == UrlImage);

                if (IsDuplicate)
                {
                    return IdentityResult.Failed(_appIdentityErrorDescriber.DuplicateUrlErorr());
                }

                if (!Directory.Exists(ImageExtend.PathRepresentWebRootPath(_webHostEnvironment, EmunFoderAvatar)))
                {
                    Directory.CreateDirectory(ImageExtend.PathRepresentWebRootPath(_webHostEnvironment, EmunFoderAvatar));
                }

                var uniqueFileName = UrlImage;

                var uploads = ImageExtend.PathRepresentWebRootPath(_webHostEnvironment, EmunFoderAvatar);

                var filePath = Path.Combine(uploads, uniqueFileName);

                using (var stream = File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }

                return IdentityResult.Success;

            }
            catch (DbUpdateException)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DbUpdateErorr());
            }
            catch (Exception)
            {

                return IdentityResult.Failed(_appIdentityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IdentityResult> CreateFileBannerAsync(IFormFile formFile, string UrlImage)
        {
            try
            {
                var IsDuplicate = await _context.DataImages.AnyAsync(x => x.Url == UrlImage);

                if (IsDuplicate)
                {
                    return IdentityResult.Failed(_appIdentityErrorDescriber.DuplicateUrlErorr());
                }

                if (!Directory.Exists(ImageExtend.PathRepresentWebRootPath(_webHostEnvironment, EmunFoderBanner)))
                {
                    Directory.CreateDirectory(ImageExtend.PathRepresentWebRootPath(_webHostEnvironment, EmunFoderBanner));
                }

                var uniqueFileName = UrlImage;

                var uploads = ImageExtend.PathRepresentWebRootPath(_webHostEnvironment, EmunFoderBanner);

                var filePath = Path.Combine(uploads, uniqueFileName);

                using (var stream = File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }

                return IdentityResult.Success;

            }
            catch (DbUpdateException)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DbUpdateErorr());
            }
            catch (Exception)
            {

                return IdentityResult.Failed(_appIdentityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IdentityResult> DeleteFileAsync(string UrlImg)
        {

            //var t = Task.Factory.StartNew(() => File.Delete("file.txt"));
            //// ...
            //t.Wait();

            var task = new Task<IdentityResult>(() => {

                    try
                    {
                        if (UrlImg != null)
                        {
                            var path = Path.Combine(ImageExtend.PathRepresentWebRootPath(_webHostEnvironment, EmunFoder), UrlImg);
                            
                            if (File.Exists(path))
                            {
                                File.Delete(path);  
                            }
                        }

                        return IdentityResult.Success;
                    }
                    catch (Exception)
                    {

                        return IdentityResult.Failed(_appIdentityErrorDescriber.NotFindFileErorr());
                    }
                   
                });

                task.Start();

                return await task;

        }


        public async Task<IdentityResult> DeleteFileAvatarAsync(string UrlImg)
        {

            var task = new Task<IdentityResult>(() => {

                try
                {
                    if (UrlImg != null)
                    {
                        var path = Path.Combine(ImageExtend.PathRepresentWebRootPath(_webHostEnvironment, EmunFoderAvatar), UrlImg);

                        if (File.Exists(path))
                        {
                            File.Delete(path);

                        }
                    }

                    return IdentityResult.Success;
                }
                catch (Exception)
                {

                    return IdentityResult.Failed(_appIdentityErrorDescriber.NotFindFileErorr());
                }

            });

            task.Start();

            return await task;


        }

        public async Task<IdentityResult> DeleteFileBannerAsync(string UrlImg)
        {
            var task = new Task<IdentityResult>(() => {

                try
                {
                    if (UrlImg != null)
                    {
                        var path = Path.Combine(ImageExtend.PathRepresentWebRootPath(_webHostEnvironment, EmunFoderBanner), UrlImg);

                        if (File.Exists(path))
                        {
                            File.Delete(path);

                        }
                    }

                    return IdentityResult.Success;
                }
                catch (Exception)
                {

                    return IdentityResult.Failed(_appIdentityErrorDescriber.NotFindFileErorr());
                }

            });

            task.Start();

            return await task;
        }
    }
}
