using SQLite;
using System;

namespace MyPOS.Models
{
    [Table("Customer")]
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [Unique]
        public string Email { get; set; }
        public long  Number { get; set; }
        public string Note { get; set; }
    }
}