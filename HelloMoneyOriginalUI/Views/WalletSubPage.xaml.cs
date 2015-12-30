using Windows.UI.Xaml.Controls;
using NavigationMenuSample.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace NavigationMenuSample.Views
{

    public class WalletSubDetail
    {

        public string name { get; set; }
        public double amount { get; set; }
        public DateTime time { get; set; }
        public string note { get; set; }
    }
    public sealed partial class WalletSubPage : Page
    {
        List<WalletSubDetail> detailsIn, detailsOut;
        public WalletSubPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            detailsIn = new List<WalletSubDetail>();
            detailsOut = new List<WalletSubDetail>();
            int walletIndex = (int)e.Parameter;
            List<Record> walletRecord = new List<Record>();
            walletRecord = (await WalletManager.getAllWalletRecordByName(walletIndex)).ToList();
            Debug.WriteLine("目前的size是" + walletRecord.Count());
            //List<string> ttlist = new List<string>();
            for (int i = 0; i < walletRecord.Count(); i++)
            {
                WalletSubDetail temp = new WalletSubDetail();
                temp.note = walletRecord[i].RecordNotes;
                temp.amount = walletRecord[i].Amount;
                temp.time = walletRecord[i].RecordTime;
                temp.name = walletRecord[i].RecordSource;
                //income 0
                if (walletRecord[i].Type == 0)
                {
                    detailsIn.Add(temp);
                }
                else
                {
                    detailsOut.Add(temp);
                }
            }
            //records.ItemsSource = ttlist;
            if (walletRecord.Count() > 0)
            {
                walletDetailIncome.ItemsSource = detailsIn;
                walletDetailOutgoing.ItemsSource = detailsOut;
            }
            walletName.Text = App.walletHelper._walletData[walletIndex].walletName;

        }
    }
}
