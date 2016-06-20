using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().Select(user => new DalUser()
            {
                Id = user.Id,
                RoleId = user.RoleId,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Password = user.Password
            });
        }

        public DalUser GetById(int key)
        {
            return context.Set<User>().FirstOrDefault(user => user.Id == key).ToDalUser();
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            //Expression<Func<DalUser, bool>> -> Expression<Func<User, bool>> (!)
            return context.Set<User>().Select(user => new DalUser()
            {
                Id = user.Id,
                RoleId = user.RoleId,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Password = user.Password
            }).SingleOrDefault(f);
        }

        public void Create(DalUser e)
        {
            context.Set<User>().Add(e.ToUser());
        }

        public void Delete(DalUser e)
        {
            var user = context.Set<User>().SingleOrDefault(u => u.Id == e.Id);
            if(user != default(User))
                context.Set<User>().Remove(user);
        }

        public void Update(DalUser entity)
        {
            throw new NotImplementedException();
        }
    }
}