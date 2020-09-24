using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
<<<<<<< HEAD

=======
        List<UserProfile> GetAllUserProfiles();
>>>>>>> 2a8dbbab39e32c19744cb42f1122d454eadef585
    }
}