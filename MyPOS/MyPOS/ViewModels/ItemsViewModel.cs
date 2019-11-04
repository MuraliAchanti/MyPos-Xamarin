using MyPOS.Models;
using MyPOS.Services;
using MyPOS.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPOS.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new ItemDataStore();
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ItemsViewModel()
        {
            Title = "Items";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "UpdateItem", async (obj, item) =>
            {
                var updatedItem = item as Item;
                await DataStore.UpdateItemAsync(updatedItem);
            });

            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "DeleteItem", async (obj, item) =>
            {
                var deletedItem = item as Item;
                Items.Remove(deletedItem);
                await DataStore.DeleteItemAsync(deletedItem);
            });
        }
        public async Task<int> DeleteItem(Item deletedItem)
        {
            Items.Remove(deletedItem);
            return await DataStore.DeleteItemAsync(deletedItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}