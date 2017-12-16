using System.Collections.Generic;
using MagicMirrorApi.Models;
using System.Data.SqlClient;
using System;
using System.Web;

namespace MagicMirrorApi.Repository
{
    public class ItemRepository
    {
        public static IEnumerable<Item> GetAllItems()
        {
            var items = new List<Item>();
            using (SqlConnection connection = new SqlConnection(DatabaseConnector.getDbConnectionStringBuilder().ConnectionString))
            {
                try
                {
                    connection.Open();
                    var sql = @"SELECT Id, Title, [Description], Completed FROM Magic.Items";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(Item.LoadFromReader(reader));
                            }
                        }
                    }
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return items;
        }

        public static int CreateItem(Item item)
        {
            var newItemId = 0;
            using (SqlConnection connection = new SqlConnection(DatabaseConnector.getDbConnectionStringBuilder().ConnectionString))
            {
                connection.Open();
                var sql = $"EXEC Magic.CreateItem '{item.Title}', '{item.Description}', {item.Completed}";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            newItemId = reader.GetInt32(0); // We expect that only one column and one row is returned. This is the Id of the new item.
                        }
                    }
                }
            }

            return newItemId;
        }

        public static bool CompleteItem(int id)
        {
            var success = true;
            using (SqlConnection connection = new SqlConnection(DatabaseConnector.getDbConnectionStringBuilder().ConnectionString))
            {
                connection.Open();
                var sql = string.Format(@"UPDATE Magic.Items SET Completed = 1 WHERE Id = {0}", id);

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        command.ExecuteReader();
                    }
                    catch
                    {
                        success = false;
                    }
                }
            }

            return success;
        }
    }
}