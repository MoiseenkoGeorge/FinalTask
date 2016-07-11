using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext _context;

        public RoleRepository(DbContext context)
        {
            _context = context;
        }
        public IEnumerable<DalRole> GetAll()
        {
            return _context.Set<Role>().ToDalRoles();
        }

        public DalRole GetByPredicate(Expression<Func<DalRole, bool>> f)
        {
            return _context.Set<Role>().ToDalRoles().SingleOrDefault(f);
        }

        public void Create(DalRole role)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalRole e)
        {
            throw new NotImplementedException();
        }

        public void Update(DalRole entity)
        {
            throw new NotImplementedException();
        }


        public DalRole GetById(int roleId)
        {
            var ormRole = _context.Set<Role>().FirstOrDefault(role => role.Id == roleId);
            if (ormRole == null)
                throw new NullReferenceException();
            return new DalRole()
            { 
                Id = ormRole.Id,
                Name = ormRole.Name
            };
        }
    }
}
