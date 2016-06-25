using System;
using System.Collections.Generic;

namespace Mvc.Models
{
    public class ProfileViewModel
    {
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime? Birthday { get; set; }
        public IEnumerable<string> Areas { get; set; }
    }
}