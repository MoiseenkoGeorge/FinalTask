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
                RoleId = user.RoleId,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Password = user.Password
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
                RoleId = dalUser.RoleId
            };
        }

        public static DalProfile ToDalProfile(this Profile profile)
        {
            return new DalProfile()
            {
                Id = profile.Id,
                Age = profile.Age,
                ImageUrl = profile.ImageUrl,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserId = profile.UserId
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
                UserId = dalProfile.UserId
            };
        }
    }
}
