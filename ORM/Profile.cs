using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM
{
    public partial class Profile
    {
        public Profile()
        {
            Areas = new HashSet<Area>();
        }
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime Age { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
    }
}