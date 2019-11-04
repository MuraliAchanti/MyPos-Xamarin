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
    public class CategoryViewModel : BaseViewModel
    {
        public IDataStore<Category> DataStore => DependencyService.Get<IDataStore<Category>>() ?? new CategoryDataStore();
        public ObservableCollection<Category> Categories { get; set; }
        public Command LoadCategoriesCommand { get; set; }
        public CategoryViewModel()
        {
            Title = "Categories";
            Categories = new ObservableCollection<Category>();
            LoadCategoriesCommand = new Command(async () => await ExecuteLoadCategoriesCommand());

            MessagingCenter.Subscribe<NewCategoryPage, Category>(this, "AddCategory", async (obj, category) =>
            {
                var newCategory = category as Category;
                Categories.Add(newCategory);
                await DataStore.AddItemAsync(newCategory);
            });

            MessagingCenter.Subscribe<CategoryDetailPage, Category>(this, "UpdateCategory", async (obj, category) =>
            {
                var updatedCategory = category as Category;
                await DataStore.UpdateItemAsync(updatedCategory);
            });
        }
        public async Task<int> DeleteCategory(Category deletedCategory)
        {
            Categories.Remove(deletedCategory);
            return await DataStore.DeleteItemAsync(deletedCategory);
        }

        async Task ExecuteLoadCategoriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Categories.Clear();
                var categories = await DataStore.GetItemsAsync();
                foreach (var category in categories)
                {
                    Categories.Add(category);
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