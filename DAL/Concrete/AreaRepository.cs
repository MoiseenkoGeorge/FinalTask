using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.Repository;
using DAL.Interfacies.DTO;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class AreaRepository : IAreaRepository
    {
        private readonly DbContext _context;

        public AreaRepository(DbContext context)
        {
            _context = context;
        }
        public IEnumerable<DalArea> GetAll()
        {
            return _context.Set<Area>().Select(area => new DalArea()
            {
                Id = area.Id,
                Name = area.Name 
            });
        }

        public DalArea GetById(int key)
        {
            return _context.Set<Area>().FirstOrDefault(area => area.Id == key).ToDalArea();
        }

        public DalArea GetByPredicate(Expression<Func<DalArea, bool>> f)
        {
            return _context.Set<Area>().Select(area => new DalArea()
            {
                Id = area.Id,
                Name = area.Name
            }).FirstOrDefault(f);
        }

        public void Create(DalArea e)
        {
            _context.Set<Area>().Add(e.ToArea());
        }

        public void Delete(DalArea e)
        {
            var area = _context.Set<Area>().SingleOrDefault(a => a.Id == e.Id);
            if (area != default(Area))
                _context.Set<Area>().Remove(area);
        }

        public void Update(DalArea entity)
        {
            throw new NotImplementedException();
        }
    }
}