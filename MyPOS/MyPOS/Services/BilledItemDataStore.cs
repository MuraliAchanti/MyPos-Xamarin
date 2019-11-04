using MyPOS.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyPOS.Services
{
    public class BilledItemDataStore: IDataStore<BilledItem>
    {
        readonly SQLiteAsyncConnection database;
        public BilledItemDataStore()
        {
            database = new SQLiteAsyncConnection(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pos.db"));
            database.CreateTableAsync<BilledItem>().Wait();
        }

        public Task<int> AddItemAsync(BilledItem item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(BilledItem item)
        {
            return database.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync(BilledItem item)
        {
            return database.DeleteAsync(item);
        }

        public Task<BilledItem> GetItemAsync(int id)
        {
            return database.Table<BilledItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<BilledItem>> GetItemsAsync()
        {
            return database.Table<BilledItem>().ToListAsync();
        }
        public Task<List<BilledItem>> GetItemsAsync(int receiptId)
        {
            return database.Table<BilledItem>().Where(i => i.ReceiptId == receiptId).ToListAsync();
        }
    }
}
