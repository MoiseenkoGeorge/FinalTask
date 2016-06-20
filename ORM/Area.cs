using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM
{
    public partial class Area
    {
        public Area()
        {
            ProfileAreas = new HashSet<ProfileArea>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ProfileArea> ProfileAreas { get; set; }
    }
}