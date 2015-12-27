using NavigationMenuSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace NavigationMenuSample.Views
{
    public sealed partial class WalletPage : Page
    {
        private List<Wallet> Wallets;
        private double Balance = 0;
        
        public  WalletPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            IEnumerable<Wallet> allWallets = await WalletManager.GetAllWallets();
            // read wallets
            Wallets =( await WalletManager.GetAllWallets()).ToList();
            // read balance according to wallets
            foreach (var item in Wallets)
            {
                Balance += item.walletValue;
            }
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
