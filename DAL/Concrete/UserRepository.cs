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
        private readonly DbContext _context;

        public UserRepository(DbContext uow)
        {
            _context = uow;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return _context.Set<User>().ToDalUsers();
        }

        public DalUser GetById(int key)
        {
            return _context.Set<User>().FirstOrDefault(user => user.Id == key)?.ToDalUser();
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            //Expression<Func<DalUser, bool>> -> Expression<Func<User, bool>> (!)
            return _context.Set<User>().ToDalUsers().SingleOrDefault(f);
        }

        public void Create(DalUser e)
        {
            _context.Set<User>().Add(e.ToUser());
        }

        public void Delete(DalUser e)
        {
            var user = _context.Set<User>().SingleOrDefault(u => u.Id == e.Id);
            if (user != default(User))
            {
                _context.Set<User>().Remove(user);
            }
        }

        public void Update(DalUser entity)
        {
            User user = entity.ToUser();

            User localUser = _context.Set<User>().Local.FirstOrDefault(u => u.Id == user.Id);
            if (localUser != null)
            {
                _context.Entry(localUser).CurrentValues.SetValues(user);
            }
            else
            {
                _context.Set<User>().Attach(user);
                _context.Entry(user).State = EntityState.Modified;
            }
        }

        public void AddRoleToUser(DalRole dalRole, DalUser dalUser)
        {
            var user = dalUser.ToUser();
            var role = dalRole.ToRole();

            if (user.Roles.Contains(role))
                return;
            user = _context.Set<User>().Local.FirstOrDefault(u => u.Id == user.Id) ?? user;
            role = _context.Set<Role>().Local.FirstOrDefault(r => r.Id == role.Id) ?? role;

            _context.Set<User>().Attach(user);
            _context.Set<Role>().Attach(role);

            _context.Entry(user).Collection(x => x.Roles).Load();
            user.Roles.Add(role);
        }
    }
}