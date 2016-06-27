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
            return profileRepository.GetById(id)?.ToBllProfile();
        }

        public IEnumerable<ProfileEntity> GetAllProfileEntities()
        {
            return profileRepository.GetAll().Select(profile => new ProfileEntity()
            {
                Id = profile.Id,
                ImageUrl = profile.ImageUrl,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                UserId = profile.UserId
            });
        }

        public ProfileEntity GetProfileByUserId(int userId)
        {
            var profile = profileRepository.GetByPredicate(x => x.UserId == userId);
            var user = userRepository.GetByPredicate(x => x.Id == userId);
            var profileEntity = profile?.ToBllProfile();
            if (profileEntity == null) return null;
            profileEntity.Email = user.Email;
            profileEntity.Role = user.DalRoles.Select( r => r.Name).ToArray()[0];
            return profileEntity;
        }

        public void UpdateProfile(ProfileEntity profile)
        {
            userRepository.AddRoleToUser(roleRepository.GetByPredicate(r => r.Name == profile.Role), userRepository.GetById(profile.UserId));
            uow.Commit();
            
            var dalProfile = profileRepository.GetByPredicate(x => x.UserId == profile.UserId);
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
