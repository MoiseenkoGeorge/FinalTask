using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interfacies.DTO
{
    public class DalArea : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<DalProfile> DalProfiles { get; set; }
    }
}