using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interfacies.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class DalMappers
    {
        #region QueryMappers
        #region User
        public static IQueryable<DalUser> ToDalUsers(this IQueryable<User> user)
        {
            return user?.Select(u => new DalUser()
            {
                Id = u.Id,
                Email = u.Email,
                EmailConfirmed = u.EmailConfirmed,
                Password = u.Password,
                DalRoles = u.Roles.Select(r => new DalRole()
                {
                    Id = r.Id,
                    Name = r.Name
                })
            });
        }

        public static IQueryable<User> ToUsers(this IQueryable<DalUser> dalUser)
        {
            return dalUser?.Select(d => new User()
            {
                Id = d.Id,
                Email = d.Email,
                EmailConfirmed = d.EmailConfirmed,
                Password = d.Password,
                Roles = d.DalRoles.Select(r => new Role()
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList()
            });
        }
        #endregion
        #region Profile

        public static IQueryable<DalProfile> ToDalProfiles(this IQueryable<Profile> profiles)
        {
            return profiles?.Select(p => new DalProfile()
            {
                Age = p.Age,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Id = p.Id,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                DalAreas = p.Areas.Select(area => new DalArea()
                {
                    Id = area.Id,
                    Name = area.Name
                })
            });
        }

        public static IQueryable<Profile> ToProfiles(this IQueryable<DalProfile> dalProfiles)
        {
            return dalProfiles?.Select(p => new Profile()
            {
                Age = p.Age,
                Description = p.Description,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Areas = p.DalAreas.Select(a => new Area()
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList()
            });
        }
        #endregion
        #region Area

        public static IQueryable<DalArea> ToDalAreas(this IQueryable<Area> areas)
        {
            return areas?.Select(a => new DalArea()
            {
                Id = a.Id,
                Name = a.Name
            });
        }

        public static IQueryable<Area> ToAreas(this IQueryable<DalArea> dalAreas)
        {
            return dalAreas?.Select(a => new Area()
            {
                Id = a.Id,
                Name = a.Name
            });
        }
        #endregion
        #region Role

        public static IQueryable<DalRole> ToDalRoles(this IQueryable<Role> roles)
        {
            return roles?.Select(r => new DalRole()
            {
                Id = r.Id,
                Name = r.Name
            });
        }

        public static IQueryable<Role> ToRoles(this IQueryable<DalRole> dalRoles)
        {
            return dalRoles?.Select(r => new Role()
            {
                Id = r.Id,
                Name = r.Name
            });
        } 
        #endregion
        #endregion
        #region Mappers
        #region User
        public static DalUser ToDalUser(this User user)
        {
            return new DalUser()
            {
                Id = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Password = user.Password,
                DalRoles = user.Roles.ToDalRoles()
            };
        }
        public static User ToUser(this DalUser dalUser)
        {
            return new User()
            {
                Id = dalUser.Id,
                Email = dalUser.Email,
                EmailConfirmed = dalUser.EmailConfirmed,
                Password = dalUser.Password,
                Roles = dalUser.DalRoles.ToRoles().ToList()
            };
        }

        public static IEnumerable<DalUser> ToDalUsers(this IEnumerable<User> users)
        {
            return users?.Select(x => x.ToDalUser());
        }

        public static IEnumerable<User> ToUsers(this IEnumerable<DalUser> dalUsers)
        {
            return dalUsers?.Select(x => x.ToUser());
        } 
        #endregion
        #region Role
        public static DalRole ToDalRole(this Role role)
        {
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name,
            };
        }

        public static Role ToRole(this DalRole dalRole)
        {
            return new Role()
            {
                Id = dalRole.Id,
                Name = dalRole.Name,
            };
        }

        public static IEnumerable<DalRole> ToDalRoles(this IEnumerable<Role> roles)
        {
            return roles?.Select(x => x.ToDalRole());
        }

        public static IEnumerable<Role> ToRoles(this IEnumerable<DalRole> dalRoles)
        {
            return dalRoles?.Select(x => x.ToRole());
        }
        #endregion
        #region Profile
        public static DalProfile ToDalProfile(this Profile profile)
        {
            return new DalProfile()
            {
                Id = profile.Id,
                Age = profile.Age,
                ImageUrl = profile.ImageUrl,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                DalAreas = profile.Areas.ToDalAreas(),
                Description = profile.Description
            };
        }

        public static Profile ToProfile(this DalProfile dalProfile)
        {
            return new Profile()
            {
                Id = dalProfile.Id,
                Age = dalProfile.Age,
                ImageUrl = dalProfile.ImageUrl,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                Areas = dalProfile.DalAreas.ToAreas()?.ToList(),
                Description = dalProfile.Description
            };
        }

        public static IEnumerable<DalProfile> ToDalProfiles(this IEnumerable<Profile> profiles)
        {
            return profiles?.Select(x => x.ToDalProfile());
        }

        public static IEnumerable<Profile> ToProfiles(this IEnumerable<DalProfile> dalProfiles)
        {
            return dalProfiles?.Select(x => x.ToProfile());
        } 
        #endregion
        #region Area
        public static DalArea ToDalArea(this Area area)
        {
            return new DalArea()
            {
                Id = area.Id,
                Name = area.Name
            };
        }

        public static Area ToArea(this DalArea dalArea)
        {
            return new Area()
            {
                Id = dalArea.Id,
                Name = dalArea.Name
            };
        }

        public static IEnumerable<DalArea> ToDalAreas(this IEnumerable<Area> areas)
        {
            return areas?.Select(x => x.ToDalArea());
        }

        public static IEnumerable<Area> ToAreas(this IEnumerable<DalArea> dalAreas)
        {
            return dalAreas?.Select(x => x.ToArea());
        }
        #endregion
        #endregion
    }
}
