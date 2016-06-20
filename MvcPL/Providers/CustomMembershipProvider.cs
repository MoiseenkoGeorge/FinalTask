using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Providers.Entities;
using System.Web.Security;
using System.Web.Mvc;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using DependencyResolver;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {

        private readonly IUserService service;

        public CustomMembershipProvider(IUserService service)
        {
            this.service = service;
        }

        public MembershipUser CreateUser(RegisterViewModel registerViewModel)
        {

            MembershipUser membershipUser = GetUser(registerViewModel.Email, false);

            if (membershipUser != null)
            {
                return null;
            }

            service.CreateUser(registerViewModel.ToBllUser());
            membershipUser = GetUser(registerViewModel.Email, false);
            return membershipUser;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = service.GetUserByEmail(email);

            if (user == null) return null;

            return new MembershipUser("CustomMembershipProvider", user.Email,
                null, user.Email, null, null,
                false, false, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            return service.GetAllUserEntities().Select(user => user.ToUserView());
        }
        public override bool ValidateUser(string email, string password)
        {
            var user = service.GetUserByEmail(email);
            return user != null && Crypto.VerifyHashedPassword(user.Password, password);
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