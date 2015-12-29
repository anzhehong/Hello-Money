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
            listMonthReport.IsItemClickEnabled = true;
            listDayReport.IsItemClickEnabled = true;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
        // 当前记录的月份
        private int day;
        // 当前记录的月份
        private int month;
        // 当前记录的年份
        private int year;
        // 导航进入界面的事件处理程序
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            day = DateTime.Now.Day;
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;
            DisplayMonthData();
            DisplayDayData();

        }
        public int ApplicationBarIconButton_Sitch = 0;
        // 处理菜单栏单击事件
        private void ApplicationBarIconButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PivotIndex.SelectedIndex == 1)
                {
                    switch ((sender as AppBarButton).Label)
                    {
                        case "Last":
                            this.month--;
                            if (this.month <= 0)
                            {
                                this.year--;
                                this.month = 12;
                            }
                            break;
                        case "Next":
                            this.month++;
                            if (this.month > 12)
                            {
                                this.year++;
                                this.month = 1;
                            }
                            break;
                    }
                    DisplayMonthData();
                }
                else {
                    switch ((sender as AppBarButton).Label)
                    {
                        case "Last":
                            this.day--;
                            if (this.day <= 0)
                            {
                                if (this.month == 3 || this.month == 5 || this.month == 7
                                    || this.month == 8 || this.month == 10 || this.month == 12)
                                {
                                    this.month--;                                   
                                    this.day = 31;
                                }else if (this.month == 2)
                                {
                                    this.month--;
                                    if (this.year % 4 != 0)
                                    {
                                        this.day = 28;
                                    }
                                    else this.day = 29;
                                }else if(this.month == 1)
                                {
                                    this.year--;
                                    this.month = 12;
                                    this.day = 31;
                                }
                                else
                                {
                                    this.month--;
                                    this.day = 30;
                                }                             
                            }
                            break;
                        case "Next":
                            this.day++;
                            if ((this.month == 1||this.month == 3 || this.month == 5 || this.month == 7
                                    || this.month == 8 || this.month == 10 || this.month == 12) && this.day>31)
                            {
                                this.month++;
                                if (this.month > 12)
                                {
                                    this.year++;
                                    this.month = 1;
                                }
                                this.day = 1;
                            }else if (this.month == 2 && this.day > 28)
                            {
                                this.month++;
                                this.day = 1;
                            }else if ((this.month == 4 || this.month ==6 || this.month == 9
                                    || this.month == 11 ) && this.day > 30)
                            {
                                this.month++;
                                this.day = 1; }
                                break;
                    }
                    DisplayDayData();
                }
            }             
           
            catch
            {
            }
        }
        // 展现记账的数据
        private async void DisplayDayData()
        {
            //本天的收入
            double inSum = await LINQ.GetdaySummaryIncome(day,month, year);
            //本月的支出
            double exSum = await LINQ.GetdaySummaryExpenses(day,month, year);
            //显示本天收入
            dayInTB.Text = "Income:" + inSum;
            //显示本天支出
            dayExTB.Text = "Expend:" + exSum;
            //显示天月结余
            dayBalanceTB.Text = "Balance:" + (inSum - exSum);
            //绑定当前天份的记账记录
            listDayReport.ItemsSource = await LINQ.GetThisDayAllRecords(day, month, year);
            BillPageTitle.Text = year + "-" + month + "-" + day;
        }
        private async void DisplayMonthData()
        {
            //本月的收入
            double inSum = await LINQ.GetmonthSummaryIncome(month, year);
            //本月的支出
            double exSum = await LINQ.GetmonthSummaryExpenses(month, year);
            //显示本月收入
            inTB.Text = "Income:" + inSum;
            //显示本月支出
            exTB.Text = "Expend:" + exSum;
            //显示本月结余
            balanceTB.Text = "Balance:" + (inSum - exSum);
            //绑定当前月份的记账记录
            listMonthReport.ItemsSource = await LINQ.GetThisMonthAllRecords(month, year);
            BillPageTitle.Text = year + "-" + month;
        }

        // Redirect to details page
        private void NavToItemDetail(object sender , ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(DetailsPage),e.ClickedItem);
        }

       
    }
}

