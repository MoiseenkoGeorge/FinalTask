﻿using System;
using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        public IEnumerable<RoleEntity> RoleEntities { get; set; } 
    }
}