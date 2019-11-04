using MyPOS.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyPOS.Services
{
    public class ReceiptDataStore : IDataStore<Receipt>
    {
        readonly SQLiteAsyncConnection database;

        public ReceiptDataStore()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pos.db");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Receipt>().Wait();
        }

        public Task<int> AddItemAsync(Receipt receipt)
        {
            return database.InsertAsync(receipt);
        }

        public Task<int> UpdateItemAsync(Receipt receipt)
        {
            return database.UpdateAsync(receipt);
        }

        public Task<int> DeleteItemAsync(Receipt receipt)
        {
            return database.DeleteAsync(receipt);
        }

        public Task<Receipt> GetItemAsync(int id)
        {
            return database.Table<Receipt>().Where(i => i.ReceiptId == id).FirstOrDefaultAsync();
        }

        public Task<List<Receipt>> GetItemsAsync()
        {
            return database.Table<Receipt>().ToListAsync();
        }
    }
}
