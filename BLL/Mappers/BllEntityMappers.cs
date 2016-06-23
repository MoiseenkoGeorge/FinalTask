using System.Linq;
using BLL.Interface.Entities;
using BLL.Interfacies.Entities;
using DAL.Interface.DTO;
using DAL.Interfacies.DTO;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
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

        public static ProfileEntity ToBllProfile(this DalProfile dalProfile)
        {
            return new ProfileEntity()
            {
                Id = dalProfile.Id,
                ImageUrl = dalProfile.ImageUrl,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                Age = dalProfile.Age,
                UserId = dalProfile.UserId,
                Description = dalProfile.Description,
                AreaEntities = dalProfile.DalAreas.Select( dalArea => dalArea.ToBllArea())
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
                UserId = profileEntity.UserId,
                Description = profileEntity.Description,
                DalAreas = profileEntity.AreaEntities.Select(areaEntity => areaEntity.ToDalArea())
            };
        }

        public static AreaEntity ToBllArea(this DalArea dalArea)
        {
            return new AreaEntity()
            {
                Id = dalArea.Id,
                Name = dalArea.Name,
                ProfileEntities = dalArea.DalProfiles.Select( dalProfile => dalProfile.ToBllProfile())
            };
        }

        public static DalArea ToDalArea(this AreaEntity areaEntity)
        {
            return new DalArea()
            {
                Id = areaEntity.Id,
                Name = areaEntity.Name,
                DalProfiles = areaEntity.ProfileEntities.Select( profileEntity => profileEntity.ToDalProfile())
            };
        }
    }
}
