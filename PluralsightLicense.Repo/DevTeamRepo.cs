using PluralsightLicense.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace PluralsightLicense.Repo
{
        public interface IDevTeamRepo
        {
            bool AssignDeveloperstoTeam(DevTeam devTeam);
            List<TeamDevelopersVM> GetTeamDevelopers();
        bool RemoveDevelopersFromTeam(DevTeam devTeam);
    }

    public class DevTeamRepo : BaseRepo, IDevTeamRepo
    {

        public DevTeamRepo() : base()
        {
        }

        public bool AssignDeveloperstoTeam(DevTeam devTeam)
        {
            using (SqlConnection sqlConnection = GetDbConnection())
            {
                sqlConnection.Open();
                foreach (var devId in devTeam.DeveloperIds.Split(","))
                {
                    
                    SqlCommand sqlCommand1 = new SqlCommand("Select count(Id) from DevTeam Where DeveloperId=" + devId + " AND TeamId=" + devTeam.TeamId, sqlConnection);
                    var existingCount = Convert.ToInt32(sqlCommand1.ExecuteScalar());
                    if (existingCount <= 0)
                    {
                        SqlCommand sqlCommand = new SqlCommand("Insert INTO DevTeam (TeamId,DeveloperId) VALUES(" + devTeam.TeamId + "," + devId + ")", sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        
                    }
                }
                sqlConnection.Close();
                return true;
            }
        }

        public bool RemoveDevelopersFromTeam(DevTeam devTeam)
        {
            using (SqlConnection sqlConnection = GetDbConnection())
            {
                sqlConnection.Open();
                foreach (var devId in devTeam.DeveloperIds.Split(","))
                {

                        SqlCommand sqlCommand = new SqlCommand("Delete FROM DevTeam Where DeveloperId=" + devId + " AND TeamId=" + devTeam.TeamId, sqlConnection);
                        sqlCommand.ExecuteNonQuery();


                }
                sqlConnection.Close();
                return true;
            }
        }

        /// <summary>
        /// Returns all if there is no team id given
        /// </summary>
        /// <returns>bool</returns>
        public List<TeamDevelopersVM> GetTeamDevelopers()
        {

            using (SqlConnection sqlConnection = GetDbConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Id,t.TeamName,d.DeveloperName FROM DevTeam Td LEFT JOIN Developer d ON td.DeveloperId=d.DeveloperId LEFT JOIN Team t on Td.TeamId=t.TeamId", sqlConnection);
                sqlConnection.Open();
                DataTable dataTable = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dataTable);
                sqlConnection.Close();
                var result = new List<TeamDevelopersVM>();
                result = ConvertDataTableToList<TeamDevelopersVM>(dataTable);
                return result;
            }
        }
    }
}
