using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ChartPage : Page
    {
        //private RecordHelper rh;
        public ChartPage()
        {
            this.InitializeComponent();
           // rh = new RecordHelper();
        }

        // 导航进入界面的事件处理程序
        protected async override void OnNavigatedTo (NavigationEventArgs e)
        {
            this.RequestedTheme = await App.walletHelper.GetTheme();

            // 创建图表的数据源对象
            ObservableCollection<ChartData> collection = new ObservableCollection<ChartData>();
            ObservableCollection<ChartData> collectionForExpend = new ObservableCollection<ChartData>();
            ObservableCollection<ChartDataForBar> collectionForBar = new ObservableCollection<ChartDataForBar>();
            ObservableCollection<ChartDataForLine> collectionForLine = new ObservableCollection<ChartDataForLine>();
            // 获取所有的记账记录
            IEnumerable<Record> allRecords = await LINQ.GetAllRecords();
            IEnumerable<Record> allIncomeRecords = await LINQ.GetThisMonthAllRecordsForType(DateTime.Now.Month, DateTime.Now.Year, 0);
            IEnumerable<Record> allExpendRecords = await LINQ.GetThisMonthAllRecordsForType(DateTime.Now.Month, DateTime.Now.Year, 1);
            // 获取所有记账记录里面的类别
            IEnumerable<string> categories = (from c in allRecords select c.Category).Distinct<string>();
            // 按照类别来统计记账的数目
            foreach (var item in categories)
            {
                // 获取该类别下的钱的枚举集合
                IEnumerable<double> amounts = from c in allIncomeRecords.Where<Record>(c => c.Category == item) select c.Amount;
                IEnumerable<double> amountsForExpend = from c in allExpendRecords.Where<Record>(c => c.Category == item) select c.Amount;
                // 添加一条图表的数据
                ChartData data = new ChartData
                {
                    Account = amounts.Sum(),
                    Type = item
                };
                ChartData dataForExpend = new ChartData
                {
                    Account = amountsForExpend.Sum(),
                    Type = item
                };             
                collection.Add(data);
                collectionForExpend.Add(dataForExpend);
            }
            //ba chartr 对比总的 income expend
            double incomeAll = ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                                      where c.Type == 0
                                                      select c.Amount)).Sum();
            double expendAll =  ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                          where c.Type == 1
                                          select c.Amount)).Sum();
           
            ChartDataForBar incomeForBar = new ChartDataForBar
            {
                Account = incomeAll
            };
            ChartDataForBar expendForBar = new ChartDataForBar
            {
                Account = expendAll
            };
            collectionForBar.Add(incomeForBar);
            collectionForBar.Add(expendForBar);

            //line chart 显示每日余额趋势
            // 获取所有记账记录里面的时间
            IEnumerable<int> dayTime = (from c in allRecords select c.RecordTime.Day).Distinct<int>();
            foreach (var item in dayTime)
            {
              
                IEnumerable<double> amounts = from c in allIncomeRecords.Where<Record>(c => c.RecordTime.Day == item) select c.Amount;
                IEnumerable<double> amountsForExpend = from c in allExpendRecords.Where<Record>(c => c.RecordTime.Day == item) select c.Amount;
                // 添加一条图表的数据
                ChartDataForLine data = new ChartDataForLine
                {
                    Account = amounts.Sum() - amountsForExpend.Sum()               
                };
                collectionForLine.Add(data);

            }

            // 设置图形的数据源
            pieChart.DataSource = collection;
            expandPieChart.DataSource = collectionForExpend;

            barChart.DataSource = collectionForBar;
            //barChartForExpend.DataSource = collectionForExpend;
 
            lineChart.DataSource = collectionForLine;
           // lineChartForExpend.DataSource = collectionForExpend;
            //chart3.DataSource = from c in collectionForExpend.Where<ChartData>(c => c.Account != 0) select c;

        }
    }
}
