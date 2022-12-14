using GroupTherapyWebAppFinal.Controllers;
using GroupTherapyWebAppFinal.Models;
using Microsoft.Data.SqlClient;

namespace GroupTherapyWebAppFinal.Services
{
    public class UsersFullDAO
    {
        //Establishes connection to the database - Joshua Wagner
        string connectionString = @"Server=tcp:petpal-server.database.windows.net,1433;Initial Catalog=PetPalDatabase;Persist Security Info=False;User ID=wagnerj;Password=pet@pal1315;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public UserModel FetchOne(int UserID)
        {
            //Create the query - Joshua Wagner
            string sqlStatement = "SELECT * FROM dbo.UserModels WHERE UserID = @UserID";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                //Create and execute command - Joshua Wagner
                SqlCommand command = new SqlCommand(sqlStatement, sqlConnection);

                command.Parameters.Add("@UserID", System.Data.SqlDbType.Int).Value = UserID;

                UserModel userdetails = new UserModel();
                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {                        
                        while (reader.Read())
                        {                            
                            userdetails.UserModelID = reader.GetInt32(0);
                            userdetails.Email = reader.GetString(1);
                            userdetails.Password = reader.GetString(2);
                            userdetails.Name = reader.GetString(3);
                            userdetails.UserType= reader.GetString(4);
                            userdetails.Gender = reader.GetString(5);
                            userdetails.DateCreated = reader.GetDateTime(6);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return userdetails;
            }
        }
    }
}
