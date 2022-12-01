using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models.Identity;

namespace WebSite.Services.IdentityStoreServices
{
    public class UserNameEmailSignInManager : SignInManager<AppUser>
    {
        //https://stackoverflow.com/questions/55620595/i-need-to-save-the-user-name-and-email-address-separately-i-am-am-having-an-iss/55626039#55626039


        public UserNameEmailSignInManager(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<AppUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<AppUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<AppUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }
        public override async Task<SignInResult> PasswordSignInAsync(string userNameOrEmail, string password,
                bool isPersistent, bool lockoutOnFailure)
        {
            var isEmail = IsValidEmail(userNameOrEmail);

            if (isEmail)
            {
                var User = await UserManager.FindByEmailAsync(userNameOrEmail);

                if (User == null)
                {
                    return SignInResult.Failed;
                }

                return await PasswordSignInAsync(User, password, isPersistent, lockoutOnFailure);
            }


            var user = await UserManager.FindByNameAsync(userNameOrEmail);

            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        bool IsValidEmail(string value)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(value.ToString());
                return addr.Address == (string)value;
            }
            catch
            {
                return false;
            }

        }
    }

    

}
