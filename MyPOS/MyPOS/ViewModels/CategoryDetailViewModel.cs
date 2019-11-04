using MyPOS.Models;
using MyPOS.Services;

namespace MyPOS.ViewModels
{
    public class CategoryDetailViewModel : BaseViewModel
    {
        public Category Category { get; set; }

        public CategoryDetailViewModel(Category category = null)
        {
            Title = category?.Name;
            Category = category;
        }
    }
}
