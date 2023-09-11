using PluralsightLicense.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace PluralsightLicense.Repo
{

        public interface IDeveloperRepo
        {
            bool Insert(Developer developer);

            List<Developer> GetAll(int teamId);

            bool Delete(int id);
        bool IsAvailable(int developerId);
        List<Developer> GetAllUnlicensed();
    }

        public class DeveloperRepo : BaseRepo, IDeveloperRepo
        {
            
            public DeveloperRepo():base()
            {
            }

            /// <summary>
            /// Returns false if it can't create the item
            /// </summary>
            /// <returns>bool</returns>
            public bool Insert(Developer developer)
            {
                using (SqlConnection sqlConnection = GetDbConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand("Insert into Developer (DeveloperName,IsPluralsightLicenseAssigned) VALUES('" + developer.DeveloperName + "','" + developer.IsPluralsightLicenseAssigned + "')", sqlConnection);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    return true;
                }
                
            }

            /// <summary>
            /// Returns all if there is no team id given
            /// </summary>
            /// <returns>bool</returns>
            public List<Developer> GetAll(int teamId)
            {

                using (SqlConnection sqlConnection = GetDbConnection())
                {
                    if (teamId == 0)
                    {
                        SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Developer", sqlConnection);
                        sqlConnection.Open();
                        DataTable dataTable = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dataTable);
                        sqlConnection.Close();
                        var result = ConvertDataTableToList<Developer>(dataTable);
                        return result;
                    }
                    else
                    {
                        SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Developer", sqlConnection);
                        sqlConnection.Open();
                        DataTable dataTable = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                        da.Fill(dataTable);
                        sqlConnection.Close();
                        var result = ConvertDataTableToList<Developer>(dataTable);
                        return result;
                    }
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
                    SqlCommand sqlCommand = new SqlCommand("DELETE FROM TeamDevlopers WHERE DeveloperId=" + id, sqlConnection);
                    SqlCommand sqlCommand1 = new SqlCommand("DELETE FROM Developer WHERE DeveloperId=" + id, sqlConnection);
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
            public bool IsAvailable(int developerId)
            {
                using (SqlConnection sqlConnection = GetDbConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand("Select DeveloperId FROM Developer WHERE DeveloperId=" + developerId, sqlConnection);
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

        public List<Developer> GetAllUnlicensed()
        {

            using (SqlConnection sqlConnection = GetDbConnection())
            {

                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Developer Where [IsPluralsightLicenseAssigned]=0", sqlConnection);
                    sqlConnection.Open();
                    DataTable dataTable = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dataTable);
                    sqlConnection.Close();
                    var result = ConvertDataTableToList<Developer>(dataTable);
                    return result;

            }
        }
    }

}
