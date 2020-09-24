using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
        List<UserProfile> GetAllUserProfiles();
        UserProfile GetUserProfileById(int id);

        List<UserProfile> GetAllDeactivatedUserProfiles();

        void DeactivateProfile(int id);

        void ReactivateProfile(int id);
    }
}