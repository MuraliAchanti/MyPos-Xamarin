using MyPOS.Models;
using MyPOS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPOS.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new ItemDataStore();
        public IDataStore<Category> CategoryDataStore => DependencyService.Get<IDataStore<Category>>() ?? new CategoryDataStore();
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command ReloadItemsCommand { get; set; }
        public Command LoadCategoriesCommand { get; set; }
                   
        private double _totalCost;
        public double TotalCost
        {
            get
            {
                return _totalCost;
            }
            set
            {
                SetProperty(ref _totalCost, value);
            }
        }

        public SalesViewModel()
        {
            Items = new ObservableCollection<Item>();
            Categories = new ObservableCollection<Category>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ReloadItemsCommand = new Command(async () => await ExecuteReloadItemsCommand());
            LoadCategoriesCommand = new Command(async () => await ExecuteLoadCategoriesCommand());
        }

        public void UpdateTotalCost()
        {
            TotalCost = 0;
            foreach (Item item in Items)
                if (item.Quantity > 0)
                    TotalCost += item.Quantity * item.Price;
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

        async Task ExecuteReloadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // take a backup of the previous list of items
                List<Item> oldItems = new List<Item>();
                foreach (Item oldItem in Items)
                    oldItems.Add(oldItem);

                Items.Clear();
                TotalCost = 0;
                var items = await DataStore.GetItemsAsync();
                foreach (var item in items)
                {
                    // updating the quantity from the previous list of items
                    var matchedItem = oldItems.Find(i => i.ItemId == item.ItemId);
                    if (matchedItem != null)
                    {
                        item.Quantity = matchedItem.Quantity;
                        if (item.Quantity > 0)
                            TotalCost += item.Quantity * item.Price;
                    }
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
        async Task ExecuteLoadCategoriesCommand()
        {
            try
            {
                Categories.Clear();
                Categories.Add(new Category() { Id = -1, Name = "All Items" } );
                var categories = await CategoryDataStore.GetItemsAsync();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
