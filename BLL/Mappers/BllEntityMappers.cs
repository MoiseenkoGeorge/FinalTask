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
                RoleId = userEntity.RoleId,
                Email = userEntity.Email,
                EmailConfirmed = userEntity.EmailConfirmed,
                Password = userEntity.Password
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                Id = dalUser.Id,
                RoleId = dalUser.RoleId,
                Email = dalUser.Email,
                EmailConfirmed = dalUser.EmailConfirmed,
                Password = dalUser.Password,
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
                UserId = dalProfile.UserId
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
                UserId = profileEntity.UserId
            };
        }
    }
}
