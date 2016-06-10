using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserViewModel()
            {
                Id = userEntity.Id,
                Role = (Role)userEntity.RoleId,
                Email = userEntity.Email,
                EmailConfiremed = userEntity.EmailConfirmed,
                Password = userEntity.Password
            };
        }

        public static UserEntity ToBllUser(this UserViewModel userViewModel)
        {
            return new UserEntity()
            {
                Id = userViewModel.Id,
                RoleId = (int)userViewModel.Role,
                Email = userViewModel.Email,
                EmailConfirmed = userViewModel.EmailConfiremed,
                Password = userViewModel.Password
            };
        }
    }
}