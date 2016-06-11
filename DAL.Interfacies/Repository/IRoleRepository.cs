using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IRoleRepository
    {
        IEnumerable<DalRole> GetAllRoles();
        bool CreateNewRole(DalRole role);
        DalRole GetById(int? roleId);
    }
}