using MyPOS.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace MyPOS.Views
{
    [DesignTimeVisible(false)]
    public partial class ItemQuantityPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemQuantityPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;

            double quantity = 0;
            double.TryParse(QtyNeeded.Text, out quantity);
            if (!this.viewModel.Item.ByWeight)
                quantity = Math.Floor(quantity);
            Total.Text = (quantity * this.viewModel.Item.Price).ToString();
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            double quantity = 0;
            double.TryParse(QtyNeeded.Text, out quantity);
            if (quantity >= 0)
            {
                quantity = quantity + 1;
                if (!this.viewModel.Item.ByWeight)
                    quantity = Math.Floor(quantity);
            }
            QtyNeeded.Text = quantity.ToString();
            Total.Text = (double.Parse(QtyNeeded.Text) * this.viewModel.Item.Price).ToString();
        }
        private void Sub_Clicked(object sender, EventArgs e)
        {
            double quantity = 0;
            double.TryParse(QtyNeeded.Text, out quantity);
            if (quantity > 0)
            {
                quantity = quantity - 1;
                if (!this.viewModel.Item.ByWeight)
                    quantity = Math.Floor(quantity);
            }
            QtyNeeded.Text = quantity.ToString();
            Total.Text = (double.Parse(QtyNeeded.Text) * this.viewModel.Item.Price).ToString();
        }

        private void QtyNeeded_TextChanged(object sender, TextChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isValid = this.viewModel.Item.ByWeight
                    ? args.NewTextValue.ToCharArray().All(x => char.IsDigit(x) || char.IsPunctuation(x)) //Make sure all characters are numbers or punctuation
                    : args.NewTextValue.ToCharArray().All(x => char.IsDigit(x)); //Make sure all characters are numbers
                ((Entry)sender).Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
        }

        private void QtyNeeded_Completed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(QtyNeeded.Text))
                QtyNeeded.Text = "0";

            double quantity = 0;
            if (double.Parse(QtyNeeded.Text) > 0)
            {
                quantity = double.Parse(QtyNeeded.Text);
                if (!this.viewModel.Item.ByWeight)
                    quantity = Math.Floor(quantity);
            }
            QtyNeeded.Text = quantity.ToString();
            Total.Text = (quantity * this.viewModel.Item.Price).ToString();
        }

        private async void Done_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}