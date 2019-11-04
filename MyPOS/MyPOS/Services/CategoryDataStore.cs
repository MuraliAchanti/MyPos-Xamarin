using MyPOS.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyPOS.Services
{
    class CategoryDataStore : IDataStore<Category>
    {
        readonly SQLiteAsyncConnection database;

        public CategoryDataStore()
        {
            database = new SQLiteAsyncConnection(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pos.db"));
            database.CreateTableAsync<Category>().Wait();
        }

        public Task<int> AddItemAsync(Category category)
        {
            return database.InsertAsync(category);
        }

        public Task<int> UpdateItemAsync(Category category)
        {
            return database.UpdateAsync(category);
        }

        public Task<int> DeleteItemAsync(Category category)
        {
            return database.DeleteAsync(category);
        }

        public Task<Category> GetItemAsync(int id)
        {
            return database.Table<Category>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Category>> GetItemsAsync()
        {
            return database.Table<Category>().ToListAsync();
        }
    }
}