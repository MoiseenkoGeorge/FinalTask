using System;
using System.Web.Security;
using BLL.Interface.Services;
using BLL.Interfacies.Services;

namespace Mvc.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public CustomRoleProvider(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        public override bool IsUserInRole(string email, string roleName)
        {
            // Получаем пользователя
            //var user = userService.GetUserByEmail(email);
            //if (user != null)
            //{
            //    // получаем роль
            //    var userRole = roleService.GetRoleEntity(user.RoleId);

            //    //сравниваем
            //    if (userRole != null && userRole.Name == roleName)
            //    {
            //        return true;
            //    }
            //}
            //return false;
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string email)
        {
            //string[] role = new string[] { };
            //// Получаем пользователя
            //var user = userService.GetUserByEmail(email);
            //if (user != null)
            //{
            //    // получаем роль
            //    var userRole = roleService.GetRoleEntity(user.RoleId);

            //    if (userRole != null)
            //    {
            //        role = new[] { userRole.Name };
            //    }
            //}
            //return role;
            throw new NotImplementedException();
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