using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyPOS.Models
{
    [Table("Category")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string Name { get; set; }
    }
}
