using MyPOS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPOS.Services
{
    public interface IReceiptRepository
    {
        List<Receipt> GetAllReceipts();

        //Get Specific Receipt data
        Receipt GetReceipt(int Id);

        // Delete all Receipts Data
        void DeleteAllReceipts();

        // Delete Specific Receipt
        void DeleteReceipt(int Id);

        // Insert new Receipt to DB 
        void InsertReceipt(Receipt receipt);
    }
}
