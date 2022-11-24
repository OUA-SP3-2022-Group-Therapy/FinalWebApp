using GroupTherapyWebAppFinal.Models;
using Microsoft.Data.SqlClient;

namespace GroupTherapyWebAppFinal.Services
{
    public class UsersDAO
    {
        //Establishes connection to the database - Joshua Wagner
        string connectionString = @"Server=tcp:petpal-server.database.windows.net,1433;Initial Catalog=PetPalDatabase;Persist Security Info=False;User ID=wagnerj;Password=pet@pal1315;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public bool FindUserByEmailAndPassword(UserModel user)
        {
            //Create the command and reset sucess value - Joshua Wagner
            bool succeeded = false;
            string sqlStatement = "SELECT * FROM dbo.UserModels WHERE email = @Email AND password = @Password";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                //Execute command - Joshua Wagner
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

        public UserModel FetchID(UserModel user)
        {
            //Set the command and return ID value - Joshua Wagner
            string sqlStatement = "SELECT * FROM dbo.UserModels WHERE email = @Email AND password = @Password";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                //Execute command - Joshua Wagner
                SqlCommand command = new SqlCommand(sqlStatement, sqlConnection);

                command.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 100).Value = user.Email;
                command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 30).Value = user.Password;

                UserModel userdetails = new UserModel();
                                
                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        userdetails.UserModelID = reader.GetInt32(0);
                        userdetails.Email = reader.GetString(1);
                        userdetails.Password = reader.GetString(2);
                        userdetails.Name = reader.GetString(3);
                        userdetails.UserType = reader.GetString(4);
                        userdetails.Gender = reader.GetString(5);
                        userdetails.DateCreated = reader.GetDateTime(6);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return userdetails;
            }
        }

        public UserModel FetchOne(int UserID)
        {
            //Create the query - Joshua Wagner
            string sqlStatement = "SELECT * FROM dbo.UserModels WHERE UserModelID = @UserModelID";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                //Create and execute command - Joshua Wagner
                SqlCommand command = new SqlCommand(sqlStatement, sqlConnection);
                command.Parameters.Add("@UserModelID", System.Data.SqlDbType.Int).Value = UserID;

                UserModel userdetails = new UserModel();

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {                        
                        userdetails.UserModelID = reader.GetInt32(0);
                        userdetails.Email = reader.GetString(1);
                        userdetails.Password = reader.GetString(2);
                        userdetails.Name = reader.GetString(3);
                        userdetails.UserType = reader.GetString(4);
                        userdetails.Gender = reader.GetString(5);
                        userdetails.DateCreated = reader.GetDateTime(6);
                        
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
