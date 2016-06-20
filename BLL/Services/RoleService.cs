using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork uow;
        private readonly IRoleRepository roleRepository;

        public RoleService(IUnitOfWork uow, IRoleRepository roleRepository)
        {
            this.uow = uow;
            this.roleRepository = roleRepository;
        }
        public RoleEntity GetRoleEntity(int id)
        {
            return roleRepository.GetById(id).ToBllRole();
        }

        public IEnumerable<RoleEntity> GetAllRoleEntities()
        {
            return roleRepository.GetAllRoles().Select(dalRole => dalRole.ToBllRole());
        }

        public void CreateRole(RoleEntity role)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(RoleEntity role)
        {
            throw new NotImplementedException();
        }
    }
}
