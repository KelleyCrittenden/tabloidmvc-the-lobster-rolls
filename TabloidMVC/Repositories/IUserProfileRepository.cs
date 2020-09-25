using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {

        UserProfile GetUserProfileById(int id);
        List<UserProfile> GetAllUserProfiles();
        UserProfile GetByEmail(string email);

        List<UserProfile> GetAllDeactivatedUserProfiles();
        List<UserProfile> GetAllAdminUserProfiles();

        void DeactivateProfile(int id);

        void ReactivateProfile(int id);

        void UpdateUserType(int id, int userTypeId);
        void Register(UserProfile userProfile);

    }
}