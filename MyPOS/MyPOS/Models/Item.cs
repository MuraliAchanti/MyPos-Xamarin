using SQLite;
using System;

namespace MyPOS.Models
{
    [Table("Item")]
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool ByWeight { get; set; }
        public double Price { get; set; }
        public double Cost { get; set; }
        public string SKU { get; set; }
        [Unique]
        public string Barcode { get; set; }
        [Ignore]
        public double Quantity { get; set; }
    }
}