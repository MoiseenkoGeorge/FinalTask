using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BLL.Interface.Entities;
using BLL.Interfacies.Entities;
using Mvc.Models;

namespace Mvc.Infrastructure.Mappers
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
            return new UserEntity()
            {
                Id = registerViewModel.Id,
                Email = registerViewModel.Email,
                EmailConfirmed = false,
                Password = registerViewModel.Password,
                RoleEntities = new List<RoleEntity> { new RoleEntity() { Id = (int)registerViewModel.Role } }
            };
        }

        public static ProfileViewModel ToProfileViewModel(this ProfileEntity profileEntity)
        {
            return new ProfileViewModel()
            {
                FirstName = profileEntity.FirstName,
                LastName = profileEntity.LastName,
                Birthday = profileEntity.Age,
                ImageUrl = profileEntity.ImageUrl,
                UserId = profileEntity.UserId,
                Description = profileEntity.Description,
                Email = profileEntity.Email,
                Areas = profileEntity.AreaEntities,
                Id = profileEntity.Id
            };
        }

        public static ProfileEntity ToProfileEntity(this ProfileViewModel profile)
        {
            return new ProfileEntity()
            {
                ImageUrl = profile.ImageUrl,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Birthday,
                Description = profile.Description,
                UserId = profile.UserId,
                AreaEntities = profile.Areas,
                Email = profile.Email,
                Id = profile.Id
            };
        }
    }
}