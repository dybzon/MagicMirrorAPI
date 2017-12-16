using System.Data.SqlClient;

namespace MagicMirrorApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }

        public static Item LoadFromReader(SqlDataReader reader)
        {
            var item = new Item();
            item.Id = (int)reader["Id"];
            item.Title = (string)reader["Title"];
            item.Description = (string)reader["Description"];
            item.Completed = (bool)reader["Completed"];
            return item;
        }
    }
}