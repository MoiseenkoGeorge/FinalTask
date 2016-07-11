using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfacies.DTO;

namespace DAL.Interface.Repository
{
    public interface IProfileRepository : IRepository<DalProfile>
    {
        void AddAreaToProfile(DalProfile dalProfile, DalArea dalArea);
        IEnumerable<DalProfile> GetDalProfilesByAreas(Expression<Func<DalProfile, bool>> func);
    }
}
