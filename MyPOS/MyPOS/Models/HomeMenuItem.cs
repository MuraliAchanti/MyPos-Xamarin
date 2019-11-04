using System;
using System.Collections.Generic;
using System.Text;

namespace MyPOS.Models
{
    public enum MenuItemType
    {
        Sales,
        Receipts,
        Items,
        Categories,
        Reports,
        Settings
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
