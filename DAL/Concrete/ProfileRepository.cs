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
        private readonly DbContext context;

        public ProfileRepository(DbContext context)
        {
            this.context = context;
        }
        public IEnumerable<DalProfile> GetAll()
        {
            return context.Set<Profile>().Select(profile => new DalProfile()
            {
                Id = profile.Id,
                Age = profile.Age,
                ImageUrl = profile.ImageUrl,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserId = profile.UserId
            });
        }

        public DalProfile GetById(int key)
        {
            return context.Set<Profile>().FirstOrDefault(profile => profile.Id == key).ToDalProfile();
        }

        public DalProfile GetByPredicate(Expression<Func<DalProfile, bool>> f)
        {
            return context.Set<Profile>().Select(profile => new DalProfile()
            {
                Id = profile.Id,
                Age = profile.Age,
                ImageUrl = profile.ImageUrl,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserId = profile.UserId
            }).SingleOrDefault(f);
        }

        public void Create(DalProfile e)
        {
            context.Set<Profile>().Add(e.ToProfile());
        }

        public void Delete(DalProfile e)
        {
            var profile = context.Set<Profile>().SingleOrDefault(p => p.Id == e.Id);
            if (profile != default(Profile))
                context.Set<Profile>().Remove(profile);
        }

        public void Update(DalProfile entity)
        {
            var profile = entity.ToProfile();

            var localUser = context.Set<Profile>().Local.FirstOrDefault(u => u.Id == profile.Id);
            if (localUser != null)
            {
                context.Entry(localUser).CurrentValues.SetValues(profile);
            }
            else
            {
                context.Set<Profile>().Attach(profile);
                context.Entry(profile).State = EntityState.Modified;
            }
        }
    }
}
