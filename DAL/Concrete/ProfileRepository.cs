using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interfacies.DTO;
using DAL.Mappers;
using DAL.Interface.Repository;
using ORM;

namespace DAL.Concrete
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DbContext _context;

        public ProfileRepository(DbContext context)
        {
            this._context = context;
        }
        public IEnumerable<DalProfile> GetAll()
        {
            return _context.Set<Profile>().ToDalProfiles();
        }

        public DalProfile GetById(int key)
        {
            return _context.Set<Profile>().FirstOrDefault(profile => profile.Id == key)?.ToDalProfile();
        }

        public DalProfile GetByPredicate(Expression<Func<DalProfile, bool>> f)
        {
            return _context.Set<Profile>().ToDalProfiles().SingleOrDefault(f);
        }

        public void Create(DalProfile e)
        {
            _context.Set<Profile>().Add(e.ToProfile());
        }

        public void Delete(DalProfile e)
        {
            var profile = _context.Set<Profile>().SingleOrDefault(p => p.Id == e.Id);
            if (profile != default(Profile))
                _context.Set<Profile>().Remove(profile);
        }

        public void Update(DalProfile entity)
        {
            var profile = entity.ToProfile();

            var localUser = _context.Set<Profile>().Local.FirstOrDefault(u => u.Id == profile.Id);
            if (localUser != null)
            {
                _context.Entry(localUser).CurrentValues.SetValues(profile);
            }
            else
            {
                _context.Set<Profile>().Attach(profile);
                _context.Entry(profile).State = EntityState.Modified;
            }
        }

        public void AddAreaToProfile(DalProfile dalProfile, DalArea dalArea)
        {
            var profile = dalProfile.ToProfile();
            var area = dalArea.ToArea();

            if (profile.Areas.Contains(area))
                return;
            profile = _context.Set<Profile>().Local.FirstOrDefault(p => p.Id == profile.Id) ?? profile;
            area = _context.Set<Area>().Local.FirstOrDefault(r => r.Id == area.Id) ?? area;

            _context.Set<Profile>().Attach(profile);
            _context.Set<Area>().Attach(area);

            _context.Entry(profile).Collection(x => x.Areas).Load();
            profile.Areas.Add(area);
        }

        public IEnumerable<DalProfile> GetDalProfilesByAreas(Expression<Func<DalProfile, bool>> func)
        {
            return _context.Set<Profile>().ToDalProfiles().Where(func).OrderBy(x => x.Id);
        }
    }
}
