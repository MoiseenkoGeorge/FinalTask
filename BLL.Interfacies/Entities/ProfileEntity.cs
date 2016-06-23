using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfacies.Entities
{
    public class ProfileEntity
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime? Age { get; set; }
        public IEnumerable<AreaEntity> AreaEntities { get; set; } 
    }
}
