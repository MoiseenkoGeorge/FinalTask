﻿using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IUserRepository : IRepository<DalUser>//Add user repository methods!
    {
        void AddRoleToUser(DalRole dalRole, DalUser dalUser); 
    }
}