using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyPOS.Models
{
    [Table("Sale")]
    public class Sale
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double TotalCost { get; set; }
        public DateTime Time { get; set; }
    }
}
