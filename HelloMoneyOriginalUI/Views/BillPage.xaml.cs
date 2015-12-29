using NavigationMenuSample.Controls;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationMenuSample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BillPage : Page
    {
        public BillPage()
        {
            this.InitializeComponent();
            listMouthReport.IsItemClickEnabled = true;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
        // 当前记录的月份
        private int mouth;
        // 当前记录的年份
        private int year;
        // 导航进入界面的事件处理程序
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mouth = DateTime.Now.Month;
            year = DateTime.Now.Year;
            DisplayRecordData();
        }
        public int ApplicationBarIconButton_Sitch = 0;
        // 处理菜单栏单击事件
        private void ApplicationBarIconButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch ((sender as AppBarButton).Label)
                {
                    case "Last":
                        this.mouth--;
                        if (this.mouth <= 0)
                        {
                            this.year--;
                            this.mouth = 12;
                        }
                        break;
                    case "Next":
                        this.mouth++;
                        if (this.mouth > 12)
                        {
                            this.year++;
                            this.mouth = 1;
                        }
                        break;
                }
                DisplayRecordData();
            }
            catch
            {
            }
        }
        // 展现记账的数据
        private async void DisplayRecordData()
        {
            //本月的收入
            double inSum = await LINQ.GetMouthSummaryIncome(mouth, year);
            //本月的支出
            double exSum = await LINQ.GetMouthSummaryExpenses(mouth, year);
            //显示本月收入
            inTB.Text = "Income:" + inSum;
            //显示本月支出
            exTB.Text = "Expend:" + exSum;
            //显示本月结余
            balanceTB.Text = "Balance:" + (inSum - exSum);
            //绑定当前月份的记账记录
            listMouthReport.ItemsSource = await LINQ.GetThisMonthAllRecords(mouth, year);
            BillPageTitle.Text = year + "-" + mouth ;
        }

        // Redirect to details page
        private void NavToItemDetail(object sender , ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(DetailsPage),e.ClickedItem);
        }

    }
}

