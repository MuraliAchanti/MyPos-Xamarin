using MyPOS.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyPOS.Services
{
    public class ItemDataStore : IDataStore<Item>
    {
        readonly SQLiteAsyncConnection database;

        public ItemDataStore()
        {
            database = new SQLiteAsyncConnection(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pos.db"));
            database.CreateTableAsync<Item>().Wait();
        }

        public Task<int> AddItemAsync(Item item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(Item item)
        {
            return database.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync(Item item)
        {
            return database.DeleteAsync(item);
        }

        public Task<Item> GetItemAsync(int id)
        {
            return database.Table<Item>().Where(i => i.ItemId == id).FirstOrDefaultAsync();
        }

        public Task<List<Item>> GetItemsAsync()
        {
            return database.Table<Item>().ToListAsync();
        }
    }
}
