using MyPOS.Models;
using MyPOS.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyPOS.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public IDataStore<Item> ItemDataStore => DependencyService.Get<IDataStore<Item>>() ?? new ItemDataStore();
        public IDataStore<Category> CategoryDataStore => DependencyService.Get<IDataStore<Category>>() ?? new CategoryDataStore();
        public Item Item { get; set; }
        public Command LoadCategoriesCommand { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        public ItemDetailViewModel(Item item = null)
        {
            Item = item;
            Categories = new ObservableCollection<Category>();
            LoadCategoriesCommand = new Command(async () => await ExecuteLoadCategoriesCommand());
        }

        public async Task<Item> GetItemById(int itemId)
        {
            return await ItemDataStore.GetItemAsync(itemId);
        }

        async Task ExecuteLoadCategoriesCommand()
        {
            try
            {
                Categories.Clear();
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

        public int FindCategoryIndex()
        {
            int index = 0;
            foreach (Category category in Categories)
                if (category.Name.Equals(Item.Category))
                    break;
                else
                    index++;

            return index;
        }
    }
}