using SQLite;
using System;

namespace MyPOS.Models
{
    [Table("Receipt")]
    public class Receipt
    {
        [PrimaryKey, AutoIncrement]
        public int ReceiptId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cashier { get; set; }
        public string POS { get; set; }
        public string ItemsList { get; set; }
        public double Total { get; set; }
        public double Cash { get; set; }
        public double Card { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Note { get; set; }
    }
}
