using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public enum Role
    {
        Manager = 1,
        Programmist
    }

    public class UserViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailConfiremed { get; set; }
        public Role Role { get; set; }
    }
}