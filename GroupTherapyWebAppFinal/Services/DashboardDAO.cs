using GroupTherapyWebAppFinal.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Text.RegularExpressions;

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

        // Used to fetch all family members other than the one being displayed - Joshua Wagner
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

        // Used to fetch the family IDs of all groups a member is a part of - Joshua Wagner
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

        //Used to fetch the family's details - Joshua Wagner
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

        //Used to fetch the full pet details from a family - Joshua Wagner
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

                    while (reader.Read())
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

        //Used to fetch the admin user details - Joshua Wagner
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

        //Fetch an individual pet's details - Joshua Wagner
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

        //Fetchs all the siblings of the pets - Joshua Wagner
        public List<Pet> FetchSiblings(int PetID, int FamilyID)
        {
            List<Pet> returnList = new List<Pet>();

            string sqlQuery = "SELECT * FROM dbo.Pets INNER JOIN dbo.FamilyGroups ON dbo.Pets.FamilyGroupID = dbo.FamilyGroups.FamilyGroupID WHERE dbo.FamilyGroups.FamilyGroupID=@FamilyGroupID AND dbo.Pets.PetID != @PetID";

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

        //Used to fetch all the pet trends for 1 pet - Joshua Wagner
        public List<Trend> FetchTrends(int PetID)
        {
            List<Trend> returnList = new List<Trend>();

            string sqlQuery = "SELECT * FROM dbo.Trends WHERE PetID = @PetID";

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

        //Fetch an individual schedule's details - Joshua Wagner
        public Schedule FetchSchedule(int ScheduleID)
        {
            string sqlStatement = "SELECT * FROM dbo.Schedules WHERE ScheduleID = @ScheduleID";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, sqlConnection);
                command.Parameters.Add("@ScheduleID", System.Data.SqlDbType.Int).Value = ScheduleID;

                Schedule schedule = new Schedule();

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        schedule.ScheduleID = reader.GetInt32(0);
                        schedule.ScheduleName = reader.GetString(1);
                        schedule.StartDateTime = reader.GetDateTime(2);
                        schedule.EndDateTime = reader.GetDateTime(3);
                        schedule.ScheduleType = reader.GetString(4);
                        schedule.Frequency = reader.GetString(5);
                        schedule.Dose = reader.GetString(6);
                        schedule.Description = reader.GetString(7);
                        schedule.FamilyGroupID = reader.GetInt32(8);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return schedule;
            }
        }

        //Used to fetch all the schedules for a specific family group - Joshua Wagner
        public List<Schedule> FetchSchedules(int FamilyID)
        {
            string sqlQuery = "SELECT * FROM dbo.Schedules WHERE FamilyGroupID = @FamilyGroupID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@FamilyGroupID", System.Data.SqlDbType.Int).Value = FamilyID;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Schedule> returnList = new List<Schedule>();

                while (reader.Read())
                {
                    Schedule schedule = new Schedule();
                    schedule.ScheduleID = reader.GetInt32(0);
                    schedule.ScheduleName = reader.GetString(1);
                    schedule.StartDateTime = reader.GetDateTime(2);
                    schedule.EndDateTime = reader.GetDateTime(3);
                    schedule.ScheduleType = reader.GetString(4);
                    schedule.Frequency = reader.GetString(5);
                    schedule.Dose = reader.GetString(6);
                    schedule.Description = reader.GetString(7);
                    schedule.FamilyGroupID = reader.GetInt32(8);
                    returnList.Add(schedule);
                }

                return returnList;

            }
        }

        //Used to fetch the details of a recently added family group
        public FamilyGroup FetchNewGroup()
        {
            string sqlQuery = "SELECT * FROM dbo.FamilyGroups WHERE FamilyGroupID = (SELECT MAX(FamilyGroupID) FROM FamilyGroups)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                FamilyGroup FGroup = new FamilyGroup();

                if (reader.Read())
                {
                    FGroup.FamilyGroupID = reader.GetInt32(0);
                    FGroup.FamilyName = reader.GetString(1);
                    FGroup.DateCreated = reader.GetDateTime(2);
                    FGroup.MemberStatus = reader.GetString(3);
                }

                return FGroup;
            }
        }
    }
}
