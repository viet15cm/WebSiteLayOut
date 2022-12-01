using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Services.IdentityErrors
{
    public class AppIdentityErrorDescriber : IdentityErrorDescriber
    {
        
        public virtual IdentityError DuplicateCodeErorr()
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateCodeErorr),
                Description = "Mã đã tồn tại."
            };

        }

        public virtual IdentityError DuplicateIdAndParentIdErorr()
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateIdAndParentIdErorr),
                Description = "Lỗi Id không được trùng với parentId."
            };

        }

        public virtual IdentityError  ResourceError()
        {
            return new IdentityError()
            {
                Code = nameof(ResourceError),
                Description = "Chưa gọi tài nguyên để nạp."
            };

        }
        public virtual IdentityError DbUpdateConcurrencyException()
        {
            return new IdentityError()
            {
                Code = nameof(DbUpdateConcurrencyException),
                Description = "Lỗi không tạo được."
            };
        }

        public virtual IdentityError DuplicateUrlErorr()
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateCodeErorr),
                Description = "Url đã tồn tại."
            };

        }


        public virtual IdentityError DuplicateSlug(string Slug)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateCodeErorr),
                Description = $"Slug : {Slug} đã tồn tại."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return base.DuplicateEmail(email);
        }


        public virtual IdentityError NotFindFileErorr()
        {
            return new IdentityError()
            {
                Code = nameof(NotFindFileErorr),
                Description = "Không tìm thấy file"
            };

        }
        public virtual IdentityError FindNofoundIDErorr()
        {
            return new IdentityError()
            {
                Code = nameof(FindNofoundIDErorr),
                Description = "Không tìm thấy id."
            };

        }

        public virtual IdentityError DuplicateUrlFileError()
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateUrlFileError),
                Description = "File bị trùng."
            };
        }

        public virtual IdentityError DbUpdateErorr()
        {
            return new IdentityError()
            {
                Code = nameof(DbUpdateErorr),
                Description = "Không cập nhật dữ liệu."
            };
        }

        public virtual IdentityError DataNullErorr()
        {
            return new IdentityError()
            {
                Code = nameof(DataNullErorr),
                Description = "Không tìm thấy dữ liệu."
            };
        }

        public virtual IdentityError DatabaseAllErorr()
        {
            return new IdentityError()
            {
                Code = nameof(DatabaseAllErorr),
                Description = "Lỗi hệ thống liên hệ Admin."
            };
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateRoleName),
                Description = "Tên vai trò đã tồn tại"
            };
        }


        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override IdentityError DefaultError()
        {
            return base.DefaultError();
        }

        public override IdentityError ConcurrencyFailure()
        {
            return base.ConcurrencyFailure();
        }

        public override IdentityError PasswordMismatch()
        {
            return base.PasswordMismatch();
        }

        public override IdentityError InvalidToken()
        {
            return base.InvalidToken();
        }

        public override IdentityError RecoveryCodeRedemptionFailed()
        {
            return base.RecoveryCodeRedemptionFailed();
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return base.LoginAlreadyAssociated();
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return base.InvalidUserName(userName);
        }

        public override IdentityError InvalidEmail(string email)
        {
            return base.InvalidEmail(email);
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return base.DuplicateUserName(userName);
        }

        public override IdentityError InvalidRoleName(string role)
        {
            return base.InvalidRoleName(role);
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return base.UserAlreadyHasPassword();
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return base.UserLockoutNotEnabled();
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return base.UserAlreadyInRole(role);
        }

        public override IdentityError UserNotInRole(string role)
        {
            return base.UserNotInRole(role);
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return base.PasswordTooShort(length);
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return base.PasswordRequiresUniqueChars(uniqueChars);
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return base.PasswordRequiresNonAlphanumeric();
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return base.PasswordRequiresDigit();
        }

        public override IdentityError PasswordRequiresLower()
        {
            return base.PasswordRequiresLower();
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return base.PasswordRequiresUpper();
        }
    }
}
