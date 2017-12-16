using System.Data.SqlClient;
using System;

namespace MagicMirrorApi.Repository
{
    public class MirrorRepository
    {
        /// <summary>
        /// Update the shutdown time for the magic mirror. The time will be set equal to now.
        /// </summary>
        /// <returns>A value indicating whether the request was successful.</returns>
        public static bool ShutDownMirror()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConnector.getDbConnectionStringBuilder().ConnectionString))
            {
                try
                {
                    connection.Open();
                    var sql = @"EXEC Magic.SetShutdownTime";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return true;
        }

        /// <summary>
        /// Check whether the mirror should be shut down.
        /// </summary>
        /// <returns>A value indicating whether the mirror should shut down.</returns>
        public static bool ShouldShutDownMirror()
        {
            var result = false;
            using (SqlConnection connection = new SqlConnection(DatabaseConnector.getDbConnectionStringBuilder().ConnectionString))
            {
                try
                {
                    connection.Open();
                    var sql = "EXEC Magic.ShouldShutDown";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result = (bool)reader["ShouldShutDown"];
                            }
                        }
                    }
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return result;
        }
    }
}