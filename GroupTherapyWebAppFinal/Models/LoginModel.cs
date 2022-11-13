namespace GroupTherapyWebAppFinal.Models
{
    public class LoginModel
    {
        private List<UserModel> listUsers = new List<UserModel>();

        public UserModel find(string Email)
        {
            return listUsers.FirstOrDefault(acc => acc.Email.Equals(Email));
        }

        public UserModel login(string Email, string Password) 
        {
            return listUsers.FirstOrDefault(acc => acc.Email.Equals(Email) && acc.Password.Equals(Password));
        }
    }
}
