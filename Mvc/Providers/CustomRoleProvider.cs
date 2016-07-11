using System;
using System.Linq;
using System.Web.Security;
using BLL.Interface.Services;
using System.Web.Mvc;
using BLL.Interfacies.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface.Repository;
using Mvc.Infrastructure;
using System.Web;

namespace Mvc.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private IUserService userService => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        public override bool IsUserInRole(string email, string roleName)
        {
            var user = userService.GetUserByEmail(email);
            return user != null && user.RoleEntities.Any(userRole => userRole.Name == roleName);
        }

        public override string[] GetRolesForUser(string email)
        {
            var user = userService.GetUserByEmail(email);
            return user?.RoleEntities.Select(entity => entity.Name).ToArray();
        }
        #region stabs

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
        #endregion
    }
}