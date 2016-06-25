using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using System.Web.Helpers;
using System;
using DAL.Interfacies.DTO;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IProfileRepository profileRepository;
        public UserService(IUnitOfWork uow, IUserRepository repository,IRoleRepository roleRepository,IProfileRepository profileRepository)
        {
            this.uow = uow;
            this.userRepository = repository;
            this.roleRepository = roleRepository;
            this.profileRepository = profileRepository;
        }

        public UserEntity GetUserEntity(int id)
        {
            return userRepository.GetById(id).ToBllUser();
        }
        
        public IEnumerable<UserEntity> GetAllUserEntities()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }

        public void CreateUser(UserEntity user)
        {
            var role = roleRepository.GetById(user.RoleEntities.ToList()[0].Id).ToBllRole();
            user.RoleEntities = new HashSet<RoleEntity>();
            user.Password = Crypto.HashPassword(user.Password);
            userRepository.Create(user.ToDalUser());
            uow.Commit();
            var dalUser = userRepository.GetByPredicate(x => x.Email == user.Email);
            userRepository.AddRoleToUser(role.ToDalRole(), dalUser);
            profileRepository.Create(new DalProfile() {UserId = dalUser.Id,ImageUrl = "http://res.cloudinary.com/djb7hr8nk/image/upload/v1466780959/empty_zeehdh.png" });
            uow.Commit();
        }

        public void DeleteUser(UserEntity user)
        {
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public void UpdateUser(UserEntity user)
        {
            userRepository.Update(user.ToDalUser());
            uow.Commit();
        }

        public UserEntity GetUserByEmail(string email)
        {
            var ormUser = userRepository.GetByPredicate(x => x.Email == email);
            return ormUser?.ToBllUser();
        }
    }
}
