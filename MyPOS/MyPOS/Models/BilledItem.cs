using SQLite;
using System;

namespace MyPOS.Models
{
    [Table("BilledItem")]
    public class BilledItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public String Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
    }
}
