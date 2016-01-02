using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NavigationMenuSample.Models;
using System.Diagnostics;

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BasicSubPage : Page
    {
        public BasicSubPage()
        {

            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RequestedTheme = await App.walletHelper.GetTheme();

            int walletIndex = (int)e.Parameter;
            List<Record> walletRecord = new List<Record>();
            walletRecord = (await WalletManager.getAllWalletRecordByName(walletIndex)).ToList();
            Debug.WriteLine("目前的size是" + walletRecord.Count());
            List<string> ttlist = new List<string>();
            foreach (var item in walletRecord)
            {
                ttlist.Add(item.Amount.ToString());
            }
            records.ItemsSource = ttlist;

            //if (e.Parameter is string)
            //{
            //    this.Message = String.Format("Clicked on {0}", e.Parameter);
            //}
            //else
            //{
            //    //this.Message = "Hi!";
            //    string temp = (string)e.Parameter;
            //    this.Message = temp;
            //}
            //walletName.Text = (string)e.Parameter;
            walletName.Text = "hahhh";
            base.OnNavigatedTo(e);
        }

        public string Message { get; set; }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
