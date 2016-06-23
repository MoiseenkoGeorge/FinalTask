using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Profile;
using BLL.Interface.Services;
using BLL.Interfacies.Services;
using System.Configuration;
using BLL.Services;
using DAL.Concrete;

namespace MvcPL.Providers
{
    public class CustomProfileProvider : ProfileProvider
    {
        private readonly IUserService userService;
        private readonly IProfileService profileService;
        public CustomProfileProvider(IUserService userService, IProfileService profileService)
        {
            this.userService = userService;
            this.profileService = profileService;
        }

        public CustomProfileProvider()
        {
        }
        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            // коллекция, которая возвращает значения свойств профиля
            var result = new SettingsPropertyValueCollection();

            if (collection == null || collection.Count < 1 || context == null)
            {
                return result;
            }

            // получаем из контекста имя пользователя - логин в системе
            var username = (string)context["UserName"];
            if (string.IsNullOrEmpty(username)) return result;


            // получаем пользователя из таблицы Users по email
            var user = userService.GetUserByEmail(username);
            if (user != null)
            {
                var profile = profileService.GetProfileByUserId(user.Id);
                if (profile != null)
                {
                    foreach (SettingsProperty prop in collection)
                    {
                        var spv = new SettingsPropertyValue(prop)
                        {
                            PropertyValue = profile.GetType().GetProperty(prop.Name).GetValue(profile, null)
                        };
                        result.Add(spv);
                    }
                }
                else
                {
                    foreach (SettingsProperty prop in collection)
                    {
                        var svp = new SettingsPropertyValue(prop) { PropertyValue = null };
                        result.Add(svp);
                    }
                }
            }
            return result;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            // получаем логин пользователя
            var username = (string)context["UserName"];

            if (string.IsNullOrEmpty(username) || collection.Count < 1)
                return;

            // получаем пользователя из таблицы Users по email
            var user = userService.GetUserByEmail(username);
            if (user != null)
            {
                var profile = profileService.GetProfileByUserId(user.Id);
                // если такой профиль уже есть изменяем его
                if (profile != null)
                {
                    foreach (SettingsPropertyValue val in collection)
                    {
                        profile.GetType().GetProperty(val.Property.Name).SetValue(profile, val.PropertyValue);
                    }
                }
                else
                {
                    // если нет, то создаем новый профиль и добавляем его
                    profile = new BLL.Interfacies.Entities.ProfileEntity();
                    foreach (SettingsPropertyValue val in collection)
                    {
                        profile.GetType().GetProperty(val.Property.Name).SetValue(profile, val.PropertyValue);
                    }
                    profile.UserId = user.Id;
                    profileService.CreateProfile(profile);
                }
            }
        }

        #region stabs
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}