using PluralsightLicense.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PluralsightLicense.Repo
{
    public interface ITeamRepo
    {
        bool Insert(Team team);
        
        List<Team> GetAll();
        bool Delete(int id);
        bool IsAvailable(int id);
        
    }
    public class TeamRepo : BaseRepo, ITeamRepo
    {
        public TeamRepo():base()
        {
        }

        /// <summary>
        /// Returns false if it can't create the item
        /// </summary>
        /// <returns>bool</returns>
        public bool Insert(Team Team)
        {
            using (SqlConnection sqlConnection = GetDbConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("Insert INTO Team (TeamName) VALUES('" + Team.TeamName + "')", sqlConnection);
                sqlConnection.Open();
                var v = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
        }

        /// <summary>
        /// Returns all if there is no team id given
        /// </summary>
        /// <returns>bool</returns>
        public List<Team> GetAll()
        {

            using (SqlConnection sqlConnection = GetDbConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Team", sqlConnection);
                sqlConnection.Open();
                DataTable dataTable = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dataTable);
                sqlConnection.Close();
                var result = new List<Team>();
                result = ConvertDataTableToList<Team>(dataTable);
                return result;
            }
        }





        /// <summary>
        /// Returns false if it can't delete the item
        /// </summary>
        /// <returns>bool</returns>
        public bool Delete(int id)
        {
            using (SqlConnection sqlConnection = GetDbConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM TeamDevlopers WHERE TeamId=" + id, sqlConnection);
                SqlCommand sqlCommand1 = new SqlCommand("DELETE FROM Team WHERE TeamId=" + id, sqlConnection);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand1.ExecuteNonQuery();
                sqlConnection.Close();
                return true;
            }
        }

        /// <summary>
        /// Returns false if it can't delete the item
        /// </summary>
        /// <returns>bool</returns>
        public bool IsAvailable(int id)
        {
            using (SqlConnection sqlConnection = GetDbConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("Select TeamID FROM Team WHERE TeamId=" + id, sqlConnection);
                sqlConnection.Open();
                if (Convert.ToInt32(sqlCommand.ExecuteScalar()) > 0)
                {
                    sqlConnection.Close();
                    return true;
                }
                else
                {
                    sqlConnection.Close();
                    return false;
                }
            }
        }


       
    }
}
