using System.Collections.Generic;
using System.Linq;
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
                Role = (Role)userEntity.RoleEntities.ToArray()[0].Id,
                EmailConfirmed = userEntity.EmailConfirmed
            };
        }

        public static UserEntity ToBllUser(this RegisterViewModel registerViewModel)
        {
            var a = new UserEntity()
            {
                Id = registerViewModel.Id,
                Email = registerViewModel.Email,
                EmailConfirmed = false,
                Password = registerViewModel.Password,
                RoleEntities = new List<RoleEntity> { new RoleEntity() { Id = (int)registerViewModel.Role } }
            };
            return new UserEntity()
            {
                Id = registerViewModel.Id,
                Email = registerViewModel.Email,
                EmailConfirmed = false,
                Password = registerViewModel.Password,
                RoleEntities = new List<RoleEntity> { new RoleEntity() { Id = (int)registerViewModel.Role } }
        };
        }
    }
}