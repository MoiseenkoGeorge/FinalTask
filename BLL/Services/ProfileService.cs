using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using DAL.Interfacies.DTO;

namespace BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork uow;
        private readonly IProfileRepository profileRepository;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IAreaRepository areaRepository;

        public ProfileService(IUnitOfWork uow, IProfileRepository profileRepository,IUserRepository userRepository,IRoleRepository roleRepository,IAreaRepository areaRepository)
        {
            this.uow = uow;
            this.profileRepository = profileRepository;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.areaRepository = areaRepository;
        }

        public ProfileEntity GetProfileEntity(int id)
        {
            var profileEntity = profileRepository.GetById(id)?.ToBllProfile();
            var user = userRepository.GetById(id);
            if (profileEntity == null) return null;
            profileEntity.Email = user.Email;
            profileEntity.Role = user.DalRoles.Select(r => r.Name).ToArray()[0];
            return profileEntity;
        }

        public IEnumerable<ProfileEntity> GetAllProfileEntities()
        {
            var profiles = profileRepository.GetAll()?.Select(profile => profile.ToBllProfile()).ToArray();
            if (profiles == null)
                return null;
            var users = userRepository.GetAll();
            for (int i = 0; i < profiles.Length; i++)
            {
                var user = users.SingleOrDefault(u => u.Id == profiles[i].Id);
                profiles[i].Email = user.Email;
                profiles[i].Role = user.DalRoles.Select(r => r.Name).ToArray()[0];
            }
            return profiles;
        }


        public IEnumerable<ProfileEntity> GetProfileEntitiesByAreas(string term)
        {
            var profiles = profileRepository.GetDalProfilesByAreas(p => p.DalAreas.Where(a => a.Name.StartsWith(term)).Count() != 0).ToProfileEntities().ToArray();
            for (int i = 0; i < profiles.Length; i++)
            {
                var user = userRepository.GetById(profiles[i].Id);
                profiles[i].Email = user.Email;
                profiles[i].Role = user.DalRoles.Select(r => r.Name).ToArray()[0];
            }
            return profiles;
        }

        public void UpdateProfile(ProfileEntity profile)
        {
            if (profile.Role != "0")
            {
                userRepository.AddRoleToUser(roleRepository.GetByPredicate(r => r.Name == profile.Role),
                    userRepository.GetById(profile.Id));
                uow.Commit();
            }
            var dalProfile = profileRepository.GetById(profile.Id);
            dalProfile.DalAreas = new HashSet<DalArea>();
            if (profile.Role != "Manager")
            {
                foreach (var area in profile.AreaEntities)
                {
                    var dalArea = areaRepository.GetByPredicate(x => x.Name == area);
                    if (dalArea == null)
                    {
                        areaRepository.Create(new DalArea() {Name = area});
                        uow.Commit();
                        profileRepository.AddAreaToProfile(dalProfile,
                            areaRepository.GetByPredicate(x => x.Name == area));
                    }
                    else profileRepository.AddAreaToProfile(dalProfile, dalArea);
                }
            }
            profile.Id = dalProfile.Id;
            profile.AreaEntities = new HashSet<string>();
            profileRepository.Update(profile.ToDalProfile());
            uow.Commit();
        }

        public void CreateProfile(ProfileEntity profile)
        {
            profileRepository.Create(profile.ToDalProfile());
            uow.Commit();
        }

        public void DeleteProfile(ProfileEntity profile)
        {
            profileRepository.Delete(profile?.ToDalProfile());
            uow.Commit();
        }
    }
}
