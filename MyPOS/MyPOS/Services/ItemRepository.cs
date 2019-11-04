using MyPOS.Helpers;
using MyPOS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPOS.Services
{
    public class ItemRepository : IItemRepository
    {

        DatabaseHelper _databaseHelper;
        public ItemRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public void DeleteItem(int Id)
        {
            _databaseHelper.DeleteItem(Id);
        }

        public void DeleteAllItems()
        {
            _databaseHelper.DeleteAllItems();
        }

        public List<Item> GetAllItems()
        {
            return _databaseHelper.GetAllItems();
        }

        public Item GetItem(int Id)
        {
            return _databaseHelper.GetItem(Id);
        }

        public void InsertItem(Item item)
        {
            _databaseHelper.InsertItem(item);
        }

        public void UpdateItem(Item item)
        {
            _databaseHelper.UpdateItem(item);
        }
    }
}
