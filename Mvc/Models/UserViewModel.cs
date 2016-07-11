using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models
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
        public int Id { get; set; }
        [Display(Name = "User's e-mail")]
        public string Email { get; set; }
        [Display(Name = "User's confirmation of e-mail")]
        public bool EmailConfirmed { get; set; }
        [Display(Name = "User's Role")]
        public Role Role { get; set; }
    }
}