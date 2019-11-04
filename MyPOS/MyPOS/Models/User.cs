using SQLite;

namespace MyPOS.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Type { get; set; }
        [Unique]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
