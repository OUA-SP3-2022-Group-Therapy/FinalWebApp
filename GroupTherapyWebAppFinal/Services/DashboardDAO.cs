using GroupTherapyWebAppFinal.Models;
using Microsoft.Data.SqlClient;

namespace GroupTherapyWebAppFinal.Services
{
    public class DashboardDAO
    {
        //Establishes connection to the database - Joshua Wagner
        string connectionString = @"Server=tcp:petpal-server.database.windows.net,1433;Initial Catalog=PetPalDatabase;Persist Security Info=False;User ID=wagnerj;Password=pet@pal1315;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
        public List<UserModel> FetchAllFam(int FamilyID)
        {
            List<UserModel> returnList = new List<UserModel>();

            string sqlQuery = "SELECT * FROM dbo.UserModels INNER JOIN dbo.Memberships ON dbo.UserModels.UsermodelID = dbo.Memberships.UsermodelID WHERE FamilyGroupID = @FamilyGroupID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {                
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@FamilyGroupID", System.Data.SqlDbType.Int).Value = FamilyID;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserModel user = new UserModel();
                        user.UserModelID = reader.GetInt32(0);
                        user.Email = reader.GetString(1);
                        user.Password = reader.GetString(2);
                        user.Name = reader.GetString(3);
                        user.UserType = reader.GetString(4);
                        user.Gender = reader.GetString(5);
                        user.DateCreated = reader.GetDateTime(6);

                        returnList.Add(user);
                    }
                }
            }

            return returnList;
        }

        public Membership FetchFamID(int UserID)
        {
            string sqlStatement = "SELECT * FROM dbo.Memberships WHERE UserModelID = @UserModelID";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, sqlConnection);
                command.Parameters.Add("@UserModelID", System.Data.SqlDbType.Int).Value = UserID;

                Membership member = new Membership();

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        member.UserModelID = reader.GetInt32(0);
                        member.FamilyGroupID = reader.GetInt32(1);
                        member.IsAdmin = reader.GetInt32(2);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return member;
            }
        }
    }
}
