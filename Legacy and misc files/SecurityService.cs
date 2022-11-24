using GroupTherapyWebAppFinal.Models;

namespace GroupTherapyWebAppFinal.Services
{
    public class SecurityService
    {
        //Creates list and pulls in service - Joshua Wagner
        List<UserModel> knownUsers = new List<UserModel>();
        UsersDAO usersDAO = new UsersDAO();

        //Used for adding static values for testing - Joshua Wagner
        public SecurityService() 
        {

        }

        public bool IsValid(UserModel user)
        {
            //Returns True if the user exists - Joshua Wagner
            return usersDAO.FindUserByEmailAndPassword(user);
        }
    }
}
