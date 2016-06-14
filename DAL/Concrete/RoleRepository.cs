using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;

namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EntityModel _context = new EntityModel();
        public IEnumerable<DalRole> GetAllRoles()
        {
            return _context.Set<Role>().Select(role => new DalRole()
            {
                Id = role.Id,
                Name =  role.Name
            });
        }

        public bool CreateNewRole(DalRole role)
        {
            throw new NotImplementedException();
        }

        public DalRole GetById(int? roleId)
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
