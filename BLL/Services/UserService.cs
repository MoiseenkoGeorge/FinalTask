using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using System.Web.Helpers;
using System;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        public UserService(IUnitOfWork uow, IUserRepository repository,IRoleRepository roleRepository)
        {
            this.uow = uow;
            this.userRepository = repository;
            this.roleRepository = roleRepository;
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
            var roleEntity = roleRepository.GetById(user.RoleId).ToBllRole();
            user.RoleId = roleEntity.Id;
            user.Password = Crypto.HashPassword(user.Password);
            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        public void DeleteUser(UserEntity user)
        {
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public UserEntity GetUserByEmail(string email)
        {
            var ormUser = userRepository.GetByPredicate(x => x.Email == email);
            return ormUser?.ToBllUser();
        }
    }
}
