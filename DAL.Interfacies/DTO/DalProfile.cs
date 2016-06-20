using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interfacies.DTO
{
    public class DalProfile : IEntity
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Age { get; set; }
        public int UserId { get; set; }
    }
}
