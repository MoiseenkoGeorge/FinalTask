using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interfacies.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class DalMappers
    {
        public static DalUser ToDalUser(this User user)
        {
            return new DalUser()
            {
                Id = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Password = user.Password,
                DalRoles = user.Roles.Select(role => role.ToDalRole())
            };
        }
        public static User ToUser(this DalUser dalUser)
        {
            return new User()
            {
                Id = dalUser.Id,
                Email = dalUser.Email,
                EmailConfirmed = dalUser.EmailConfirmed,
                Password = dalUser.Password,
                Roles = dalUser.DalRoles?.Select(dalRole => dalRole.ToRole()).ToList()
            };
        }
        #region Role
        public static DalRole ToDalRole(this Role role)
        {
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name,
            };
        }

        public static Role ToRole(this DalRole dalRole)
        {
            return new Role()
            {
                Id = dalRole.Id,
                Name = dalRole.Name,
            };
        }
        #endregion
        #region Profile
        public static DalProfile ToDalProfile(this Profile profile)
        {
            return new DalProfile()
            {
                Id = profile.Id,
                Age = profile.Age,
                ImageUrl = profile.ImageUrl,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserId = profile.UserId,
                DalAreas = profile.Areas.Select(area => area.ToDalArea()),
                Description = profile.Description
            };
        }

        public static Profile ToProfile(this DalProfile dalProfile)
        {
            return new Profile()
            {
                Id = dalProfile.Id,
                Age = dalProfile.Age,
                ImageUrl = dalProfile.ImageUrl,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                UserId = dalProfile.UserId,
                Areas = dalProfile.DalAreas?.Select(dalArea => dalArea.ToArea()).ToList(),
                Description = dalProfile.Description
            };
        }
        #endregion
        #region Area
        public static DalArea ToDalArea(this Area area)
        {
            return new DalArea()
            {
                Id = area.Id,
                Name = area.Name,
                DalProfiles = area.Profiles.Select(profile => profile.ToDalProfile())
            };
        }

        public static Area ToArea(this DalArea dalArea)
        {
            return new Area()
            {
                Id = dalArea.Id,
                Name = dalArea.Name,
                Profiles = (ICollection<Profile>) dalArea.DalProfiles.Select(dalProfile => dalProfile.ToProfile())
            };
        }
        #endregion
    }
}
