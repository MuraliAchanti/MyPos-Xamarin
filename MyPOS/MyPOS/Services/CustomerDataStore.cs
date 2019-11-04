using MyPOS.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyPOS.Services
{
    public class CustomerDataStore : IDataStore<Customer>
    {
        readonly SQLiteAsyncConnection database;

        public CustomerDataStore()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pos.db");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Customer>().Wait();
        }

        public Task<int> AddItemAsync(Customer customer)
        {
            return database.InsertAsync(customer);
        }

        public Task<int> UpdateItemAsync(Customer customer)
        {
            return database.UpdateAsync(customer);
        }

        public Task<int> DeleteItemAsync(Customer customer)
        {
            return database.DeleteAsync(customer);
        }

        public Task<Customer> GetItemAsync(int id)
        {
            return database.Table<Customer>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Customer>> GetItemsAsync()
        {
            return database.Table<Customer>().ToListAsync();
        }
    }
}
