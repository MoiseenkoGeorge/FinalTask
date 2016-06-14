using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public enum Role
    {
        Administrator=1,
        User,
        Manager,
        Programmist
    }

    public class UserViewModel
    {
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public Role Role { get; set; }
    }
}