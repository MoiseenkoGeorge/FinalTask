using BLL.Interface.Entities;
using DAL.Interface.DTO;

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
                Password = userEntity.Password,
                ProfileId = userEntity.ProfileId
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
                ProfileId = dalUser.ProfileId
            };
        }
    }
}
