using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using Mvc.Infrastructure.Mappers;
using Mvc.Models;

namespace Mvc.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {

        private readonly IUserService userService;

        public CustomMembershipProvider(IUserService service)
        {
            this.userService = service;
        }
        public CustomMembershipProvider()
        {
                
        }
        public MembershipUser CreateUser(RegisterViewModel registerViewModel)
        {

            MembershipUser membershipUser = GetUser(registerViewModel.Email, false);

            if (membershipUser != null)
            {
                return null;
            }

            userService.CreateUser(registerViewModel.ToBllUser());
            membershipUser = GetUser(registerViewModel.Email, false);
            return membershipUser;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = userService.GetUserByEmail(email);

            if (user == null) return null;

            return new MembershipUser("CustomMembershipProvider", user.Id.ToString(),
                null, user.Email, null, null,
                false, false, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            return userService.GetAllUserEntities().Select(user => user.ToUserView());
        }
        public override bool ValidateUser(string email, string password)
        {
            var user = userService.GetUserByEmail(email);
            return user != null && Crypto.VerifyHashedPassword(user.Password, password);
        }

        public bool ConfirmEmail(int id, string email)
        {
            var user = userService.GetUserEntity(id);
            if (user != null)
            {
                if (user.Email == email)
                {
                    user.EmailConfirmed = true;
                    userService.UpdateUser(user);
                    return true;
                }
            }
            return false;
        }
        #region stabs
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }


        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval { get; }
        public override bool EnablePasswordReset { get; }
        public override bool RequiresQuestionAndAnswer { get; }
        public override string ApplicationName { get; set; }
        public override int MaxInvalidPasswordAttempts { get; }
        public override int PasswordAttemptWindow { get; }
        public override bool RequiresUniqueEmail { get; }
        public override MembershipPasswordFormat PasswordFormat { get; }
        public override int MinRequiredPasswordLength { get; }
        public override int MinRequiredNonAlphanumericCharacters { get; }
        public override string PasswordStrengthRegularExpression { get; }
        #endregion
    }
}