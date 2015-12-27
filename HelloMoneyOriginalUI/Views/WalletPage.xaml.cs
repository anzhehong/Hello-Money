﻿using NavigationMenuSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NavigationMenuSample.Views
{
    public sealed partial class WalletPage : Page
    {
        private List<Wallet> Wallets;
        public WalletPage()
        {
            this.InitializeComponent();
            Wallets = WalletHelper.testGet();
        }
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(
                typeof(BasicSubPage),
                e.ClickedItem,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
            var wallet = (Wallet)e.ClickedItem;

        }
    }
}
