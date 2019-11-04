using MyPOS.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyPOS.Services
{
    public class UserDataStore : IDataStore<User>
    {
        readonly SQLiteAsyncConnection database;

        public UserDataStore()
        {
            database = new SQLiteAsyncConnection(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pos.db"));
            database.CreateTableAsync<User>().Wait();

            User user1 = new User();
            user1.UserName = "raj";
            user1.Type = 1; //storemanager
            user1.Email = "sanganabatlaraj297@gmail.com";
            user1.Password = "123";
            database.InsertAsync(user1);
            User user2 = new User();
            user2.UserName = "divisha";
            user2.Type = 2;
            user2.Email = "divishabyreddy@gmail.com";
            user2.Password = "123";
            database.InsertAsync(user2);
            User user3 = new User();
            user3.UserName = "pallavi";
            user3.Type = 2;
            user3.Email = "vangaripallavi27@gmail.com";
            user3.Password = "123";
            database.InsertAsync(user3);
            User user4 = new User();
            user4.UserName = "rithisha";
            user4.Type = 2;
            user4.Email = "guntupallirithisha@gmail.com";
            user4.Password = "123";
            database.InsertAsync(user4);
            User user5 = new User();
            user5.UserName = "santhosh";
            user5.Type = 2;
            user5.Email = "santhoshsai3@gmail.com";
            user5.Password = "123";
            database.InsertAsync(user5);
            User user6 = new User();
            user6.UserName = "murali";
            user6.Type = 2;
            user6.Email = "muralivenkata4@gmail.com";
            user6.Password = "123";
            database.InsertAsync(user5);
        }

        public Task<int> AddItemAsync(User user)
        {
            return database.InsertAsync(user);
        }

        public Task<int> UpdateItemAsync(User user)
        {
            return database.UpdateAsync(user);
        }

        public Task<int> DeleteItemAsync(User user)
        {
            return database.DeleteAsync(user);
        }

        public Task<User> GetItemAsync(int id)
        {
            return database.Table<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<User>> GetItemsAsync()
        {
            return database.Table<User>().ToListAsync();
        }
    }
}
