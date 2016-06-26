using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork uow;
        private readonly IProfileRepository profileRepository;
        private readonly IUserRepository userRepository;

        public ProfileService(IUnitOfWork uow, IProfileRepository profileRepository,IUserRepository userRepository)
        {
            this.uow = uow;
            this.profileRepository = profileRepository;
            this.userRepository = userRepository;
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
            return profileEntity;
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
