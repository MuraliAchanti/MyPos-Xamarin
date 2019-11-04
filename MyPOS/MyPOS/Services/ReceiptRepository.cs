using MyPOS.Helpers;
using MyPOS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPOS.Services
{
    public class ReceiptRepository : IReceiptRepository
    {

        DatabaseHelper _databaseHelper;
        public ReceiptRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public void DeleteReceipt(int Id)
        {
            _databaseHelper.DeleteReceipt(Id);
        }

        public void DeleteAllReceipts()
        {
            _databaseHelper.DeleteAllReceipts();
        }

        public List<Receipt> GetAllReceipts()
        {
            return _databaseHelper.GetAllReceipts();
        }

        public Receipt GetReceipt(int Id)
        {
            return _databaseHelper.GetReceipt(Id);
        }

        public void InsertReceipt(Receipt receipt)
        {
            _databaseHelper.InsertReceipt(receipt);
        }

        public void UpdateReceipt(Receipt receipt)
        {
            _databaseHelper.UpdateReceipt(receipt);
        }
    }
}
