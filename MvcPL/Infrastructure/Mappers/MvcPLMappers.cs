using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserViewModel ToUserView(this UserEntity userEntity)
        {
            return new UserViewModel()
            {
                Email = userEntity.Email,
                Role = (Role)userEntity.RoleId,
                EmailConfirmed = userEntity.EmailConfirmed
            };
        }

        public static UserEntity ToBllUser(this RegisterViewModel registerViewModel)
        {
            return new UserEntity()
            {
                Id = registerViewModel.Id,
                RoleId = (int)registerViewModel.Role,
                Email = registerViewModel.Email,
                EmailConfirmed = false,
                Password = registerViewModel.Password
            };
        }
    }
}