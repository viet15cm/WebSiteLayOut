using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebSite.ModelValidations;
using WebSite.Models.Identity;
using WebSite.Services.MappingFileServices;
using WebSite.Services.MappingImageServices;

namespace WebSite.Areas.Identity.Pages.Account.Manager
{
    public class ProFileModel : PageModel
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly IimageServices _imageServices;

        private readonly IFileServices _fileServices;


        public ProFileModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IimageServices iimageServices,
            IFileServices fileServices
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _imageServices = iimageServices;
            _fileServices = fileServices;
        }

        [NonAction]
        private string GetUniqueFileName(string fileName)
        {

            return DateTime.Now.ToString("yymmssfff")
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 8)
                      + Path.GetExtension(fileName);
        }

        [BindProperty]
        public InputModel Input { get; set; }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            
            Input = new InputModel
            {
                UrlImage = user.UrlImage,
                LastName = user.LastName,
                FirstName = user.FirstName,
                BirthDate = user.BirthDate,
                Company = user.Company,
               
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                NativePlace = user.NativePlace
            };
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult>OnGetAsync()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToPage("~/");
            }
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Không tải được tài khoản ID = '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Không có tài khoản ID: '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Lỗi cập nhật số điện thoại.";
                    return RedirectToPage();
                }
            }

            // Cập nhật các trường bổ sung
          
            user.LastName = Input.LastName;
            user.FirstName = Input.FirstName;
            user.BirthDate = Input.BirthDate;
           
            user.PhoneNumber = Input.PhoneNumber;
            user.Company = Input.Company;
            user.NativePlace = Input.NativePlace;
            user.Address = Input.Address;
            await _userManager.UpdateAsync(user);

            // Đăng nhập lại để làm mới Cookie (không nhớ thông tin cũ)
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Hồ sơ của bạn đã cập nhật";
            
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostImageAsync()
        {

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToPage("~/");
            }
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Không tải được tài khoản ID = '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            if (FormFile == null)
            {
                 return RedirectToPage("ProFile");
            }

            if (ModelState.IsValid)
            {

                string olUrl = user.UrlImage;

                string Url = GetUniqueFileName(FormFile.FileName);


                var resultFile = await _fileServices.CreateFileAvatarAsync(FormFile, Url);


                if (resultFile.Succeeded)
                {
                    user.UrlImage = Url;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {

                        await _signInManager.RefreshSignInAsync(user);
                        StatusMessage = "Hình đại diện của bạn đã cập nhật";

                        if (olUrl != null)
                        {
                            await _fileServices.DeleteFileAvatarAsync(olUrl);
                        }

                        return RedirectToPage();
                    }

                    await _fileServices.DeleteFileAvatarAsync(Url);
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return Page();

                }

                foreach (var error in resultFile.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
            
        }


        [BindProperty]
        [FileImgValidations(new string[] { ".jpg", ".jpeg", ".png", ".jfif" })]
        [Display(Name ="Ảnh")]
        public IFormFile FormFile { get; set; }

        public class InputModel
        {
            
            public string UrlImage { get; set; }

            [Display(Name = "Họ")]
            [StringLength(20, ErrorMessage = "{0} dài từ {2} đến {1} ký tự.", MinimumLength = 3)]
            public string LastName { get; set; }

            [Display(Name = "Tên")]

            [StringLength(20, ErrorMessage = "{0} dài từ {2} đến {1} ký tự.", MinimumLength = 3)]
            public string FirstName { get; set; }

            [Display(Name = "Ngày sinh")]
            public DateTime? BirthDate { set; get; }

            [Display(Name = "Công ty")]
            [StringLength(20, ErrorMessage = "{0} dài từ {2} đến {1} ký tự.", MinimumLength = 5)]
            public string Company { get; set; }
         
            [Phone]
            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Quê quán")]
            [StringLength(100, ErrorMessage = "{0} dài từ {2} đến {1} ký tự.", MinimumLength = 5)]
            public string NativePlace { get; set; }

            [StringLength(100)]
            [Display(Name = "Địa chỉ")]
            public string Address { get; set; }




        }
    }


    

    

}
