using GroupTherapyWebAppFinal.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace GroupTherapyWebAppFinal.Services
{
    public class DashboardDAO
    {
        //Establishes connection to the database - Joshua Wagner
        string connectionString = @"Server=tcp:petpal-server.database.windows.net,1433;Initial Catalog=PetPalDatabase;Persist Security Info=False;User ID=wagnerj;Password=pet@pal1315;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public List<UserModel> FetchAllFam(int FamilyID)
        {
            List<UserModel> returnList = new List<UserModel>();

            string sqlQuery = "SELECT * FROM dbo.UserModels INNER JOIN dbo.Memberships ON dbo.UserModels.UsermodelID = dbo.Memberships.UsermodelID WHERE FamilyGroupID=@FamilyGroupID";

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

        public List<UserModel> FetchOtherFam(int UserID, int FamilyID)
        {
            List<UserModel> returnList = new List<UserModel>();

            string sqlQuery = "SELECT * FROM dbo.UserModels INNER JOIN dbo.Memberships ON dbo.UserModels.UsermodelID = dbo.Memberships.UsermodelID WHERE dbo.Memberships.FamilyGroupID=@FamilyGroupID AND dbo.UserModels.UserModelID != @UserModelID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@FamilyGroupID", System.Data.SqlDbType.Int).Value = FamilyID;
                command.Parameters.Add("@UserModelID", System.Data.SqlDbType.Int).Value = UserID;

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

        public List<Membership> FetchFamID(int UserID)
        {
            string sqlStatement = "SELECT * FROM dbo.Memberships WHERE UserModelID = @UserModelID";
            List<Membership> returnList = new List<Membership>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, sqlConnection);
                command.Parameters.Add("@UserModelID", System.Data.SqlDbType.Int).Value = UserID;
                
                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Membership member = new Membership();
                        member.UserModelID = reader.GetInt32(0);
                        member.FamilyGroupID = reader.GetInt32(1);
                        member.IsAdmin = reader.GetInt32(2);
                        returnList.Add(member);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return returnList;
            }
        }

        public List<FamilyGroup> FetchFamDetails(int FamilyID)
        {
            string sqlStatement = "SELECT * FROM dbo.FamilyGroups WHERE FamilyGroupID = @FamilyGroupID";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, sqlConnection);
                command.Parameters.Add("@FamilyGroupID", System.Data.SqlDbType.Int).Value = FamilyID;

                List<FamilyGroup> returnList = new List<FamilyGroup>();

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        FamilyGroup FGroup = new FamilyGroup();
                        FGroup.FamilyGroupID = reader.GetInt32(0);
                        FGroup.FamilyName = reader.GetString(1);
                        FGroup.DateCreated = reader.GetDateTime(2);
                        FGroup.MemberStatus = reader.GetString(3);
                        returnList.Add(FGroup);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return returnList;
            }
        }

        public List<Pet> FetchPetDetails(int FamilyID)
        {
            string sqlStatement = "SELECT * FROM dbo.Pets WHERE FamilyGroupID = @FamilyGroupID";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, sqlConnection);
                command.Parameters.Add("@FamilyGroupID", System.Data.SqlDbType.Int).Value = FamilyID;

                List<Pet> returnList = new List<Pet>();

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Pet pets = new Pet();
                        pets.PetID = reader.GetInt32(0);
                        pets.Name = reader.GetString(1);
                        pets.Species = reader.GetString(2);
                        pets.Breed = reader.GetString(3);
                        pets.DOB = reader.GetDateTime(4);
                        pets.Allergies = reader.GetString(5);
                        pets.FamilyGroupID = reader.GetInt32(6);
                        returnList.Add(pets);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return returnList;
            }
        }

        public List<UserModel> FetchAdmin(int FamilyID)
        {
            List<UserModel> returnList = new List<UserModel>();

            string sqlQuery = "SELECT * FROM dbo.UserModels INNER JOIN dbo.Memberships ON dbo.UserModels.UsermodelID = dbo.Memberships.UsermodelID WHERE dbo.Memberships.FamilyGroupID=@FamilyGroupID AND dbo.Memberships.IsAdmin = 1";

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

        public Pet FetchPet(int PetID)
        {
            string sqlStatement = "SELECT * FROM dbo.Pets WHERE PetID = @PetID";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, sqlConnection);
                command.Parameters.Add("@PetID", System.Data.SqlDbType.Int).Value = PetID;

                Pet pet = new Pet();

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        pet.PetID = reader.GetInt32(0);
                        pet.Name = reader.GetString(1);
                        pet.Species = reader.GetString(2);
                        pet.Breed = reader.GetString(3);
                        pet.DOB = reader.GetDateTime(4);
                        pet.Allergies = reader.GetString(5);
                        pet.FamilyGroupID = reader.GetInt32(6);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return pet;
            }
        }

        public List<Pet> FetchSiblings(int PetID, int FamilyID)
        {
            List<Pet> returnList = new List<Pet>();

            string sqlQuery = "SELECT * FROM dbo.Pets INNER JOIN dbo.FamilyGroups ON dbo.Pets.FamilyGroupID = dbo.FamilyGroups.FamilyGroupID WHERE dbo.FamilyGroup.FamilyGroupID=@FamilyGroupID AND dbo.Pets.PetID != @PetID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@FamilyGroupID", System.Data.SqlDbType.Int).Value = FamilyID;
                command.Parameters.Add("@PetID", System.Data.SqlDbType.Int).Value = PetID;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Pet pet = new Pet();
                        pet.PetID = reader.GetInt32(0);
                        pet.Name = reader.GetString(1);
                        pet.Species = reader.GetString(2);
                        pet.Breed = reader.GetString(3);
                        pet.DOB = reader.GetDateTime(4);
                        pet.Allergies = reader.GetString(5);
                        pet.FamilyGroupID = reader.GetInt32(6);

                        returnList.Add(pet);
                    }
                }
            }

            return returnList;
        }

        public List<Trend> FetchTrends(int PetID)
        {
            List<Trend> returnList = new List<Trend>();

            string sqlQuery = "SELECT * FROM dbo.Trends WHERE PetID = @PetID GROUP BY Date";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@PetID", System.Data.SqlDbType.Int).Value = PetID;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Trend trend = new Trend();
                        trend.PetID = reader.GetInt32(0);
                        trend.Date = reader.GetDateTime(1);
                        trend.Height = reader.GetInt32(2);
                        trend.Weight = reader.GetInt32(3);
                        returnList.Add(trend);
                    }
                }
            }

            return returnList;

        }
    }
}
