using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using DAL.Interfacies.Repository;

namespace BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork uow;
        private readonly IProfileRepository profileRepository;

        public ProfileService(IUnitOfWork uow, IProfileRepository profileRepository)
        {
            this.uow = uow;
            this.profileRepository = profileRepository;
        }

        public ProfileEntity GetProfileEntity(int id)
        {
            return profileRepository.GetById(id).ToBllProfile();
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

        public void CreateProfile(ProfileEntity profile)
        {
            throw new NotImplementedException();
        }

        public void DeleteProfile(ProfileEntity profile)
        {
            throw new NotImplementedException();
        }
    }
}
