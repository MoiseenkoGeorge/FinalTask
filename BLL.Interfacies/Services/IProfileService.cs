
using System.Collections.Generic;
using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface IProfileService
    {
        ProfileEntity GetProfileEntity(int id);
        IEnumerable<ProfileEntity> GetAllProfileEntities();
        void CreateProfile(ProfileEntity profile);
        void DeleteProfile(ProfileEntity profile);
    }
}