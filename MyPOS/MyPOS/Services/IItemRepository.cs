using MyPOS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPOS.Services
{
    public interface IItemRepository
    {
        List<Item> GetAllItems();

        //Get Specific Item data
        Item GetItem(int Id);

        // Delete all Items Data
        void DeleteAllItems();

        // Delete Specific Item
        void DeleteItem(int Id);

        // Insert new Item to DB 
        void InsertItem(Item item);

        // Update Item Data
        void UpdateItem(Item item);
    }
}
