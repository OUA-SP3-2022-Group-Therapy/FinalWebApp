using GroupTherapyWebAppFinal.Models;
using Microsoft.Data.SqlClient;

namespace GroupTherapyWebAppFinal.Services
{
    public class UsersDAO
    {
        string connectionString = @"Server=tcp:petpal-server.database.windows.net,1433;Initial Catalog=PetPalDatabase;Persist Security Info=False;User ID=wagnerj;Password=pet@pal1315;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public bool FindUserByEmailAndPassword(UserModel user)
        {
            bool succeeded = false;
            string sqlStatement = "SELECT * FROM dbo.UserModels WHERE email = @Email AND password = @Password";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, sqlConnection);

                command.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 100).Value = user.Email;
                command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 30).Value = user.Password;

                try 
                {
                    sqlConnection.Open();
                    SqlDataReader reader= command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        succeeded = true;
                    }
                } catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return succeeded;
            }
        }
    }
}
