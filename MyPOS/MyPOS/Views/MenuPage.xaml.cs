using MyPOS.Helpers;
using MyPOS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyPOS.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;

        public  MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>();
            menuItems.Add(new HomeMenuItem { Id = MenuItemType.Sales, Title = "Sales" });
            menuItems.Add(new HomeMenuItem { Id = MenuItemType.Receipts, Title = "Receipts" });
            if (Settings.UserType == 1) //manager
            {
                menuItems.Add(new HomeMenuItem { Id = MenuItemType.Items, Title = "Items" });
                menuItems.Add(new HomeMenuItem { Id = MenuItemType.Categories, Title = "Categories" });
                menuItems.Add(new HomeMenuItem { Id = MenuItemType.Reports, Title = "Reports" });
            }
            menuItems.Add(new HomeMenuItem { Id = MenuItemType.Settings, Title = "Settings" });

            ListViewMenu.ItemsSource = menuItems;
            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}