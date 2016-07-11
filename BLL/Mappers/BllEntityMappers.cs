using System.Linq;
using BLL.Interface.Entities;
using BLL.Interfacies.Entities;
using DAL.Interface.DTO;
using DAL.Interfacies.DTO;

using System.Collections.Generic;
namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        #region User
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                EmailConfirmed = userEntity.EmailConfirmed,
                Password = userEntity.Password,
                DalRoles = userEntity.RoleEntities?.Select(roleEntity => roleEntity.ToDalRole())
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                Id = dalUser.Id,
                Email = dalUser.Email,
                EmailConfirmed = dalUser.EmailConfirmed,
                Password = dalUser.Password,
                RoleEntities = dalUser.DalRoles.Select( dalRole => dalRole.ToBllRole())
            };
        }
        public static IEnumerable<DalUser> ToDalUsers(this IEnumerable<UserEntity> userEntities)
        {
            return userEntities.Select(x => x.ToDalUser());
        }
        public static IEnumerable<UserEntity> ToUserEntities(this IEnumerable<DalUser> dalUsers)
        {
            return dalUsers.Select(x => x.ToBllUser());
        }
        #endregion
        #region Role
        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            return new RoleEntity()
            {
                Id = dalRole.Id,
                Name = dalRole.Name
            };
        }

        public static DalRole ToDalRole(this RoleEntity roleEntity)
        {
            return new DalRole()
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name
            };
        }

        public static IEnumerable<DalRole> ToDalRoles(this IEnumerable<RoleEntity> roleEntities)
        {
            return roleEntities.Select(x => x.ToDalRole());
        }
        public static IEnumerable<RoleEntity> ToRoleEntities(this IEnumerable<DalRole> dalRoles)
        {
            return dalRoles.Select(x => x.ToBllRole());
        }
        #endregion
        #region Profile
        public static ProfileEntity ToBllProfile(this DalProfile dalProfile)
        {
            return new ProfileEntity()
            {
                Id = dalProfile.Id,
                ImageUrl = dalProfile.ImageUrl,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                Age = dalProfile.Age,
                Description = dalProfile.Description,
                AreaEntities = dalProfile.DalAreas?.Select( dalArea => dalArea.Name)
            };
        }

        public static DalProfile ToDalProfile(this ProfileEntity profileEntity)
        {
            return new DalProfile()
            {
                Id = profileEntity.Id,
                ImageUrl = profileEntity.ImageUrl,
                FirstName = profileEntity.FirstName,
                LastName = profileEntity.LastName,
                Age = profileEntity.Age,
                Description = profileEntity.Description,
                DalAreas = profileEntity.AreaEntities.Select(areaEntity => new DalArea() {Name = areaEntity})
            };
        }
        public static IEnumerable<DalProfile> ToDalProfiles(this IEnumerable<ProfileEntity> profileEntities)
        {
            return profileEntities?.Select(x => x.ToDalProfile());
        }
        public static IEnumerable<ProfileEntity> ToProfileEntities(this IEnumerable<DalProfile> dalProfiles)
        {
            return dalProfiles?.Select(x => x.ToBllProfile());
        }
        #endregion
        #region Area
        public static AreaEntity ToBllArea(this DalArea dalArea)
        {
            return new AreaEntity()
            {
                Id = dalArea.Id,
                Name = dalArea.Name
            };
        }

        public static DalArea ToDalArea(this AreaEntity areaEntity)
        {
            return new DalArea()
            {
                Id = areaEntity.Id,
                Name = areaEntity.Name
            };
        }
        public static IEnumerable<DalArea> ToDalAreas(this IEnumerable<AreaEntity> areaEntities)
        {
            return areaEntities.Select(x => x.ToDalArea());
        }
        public static IEnumerable<AreaEntity> ToAreaEntities(this IEnumerable<DalArea> dalAreas)
        {
            return dalAreas.Select(x => x.ToBllArea());
        }
        #endregion
    }
}
