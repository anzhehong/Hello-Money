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
        private RecordHelper rh;
        public ChartPage()
        {
            this.InitializeComponent();
            rh = new RecordHelper();
        }

        // 导航进入界面的事件处理程序
        protected async override void OnNavigatedTo (NavigationEventArgs e)
        {
            // 创建图表的数据源对象
            ObservableCollection<ChartData> collection = new ObservableCollection<ChartData>();
            ObservableCollection<ChartData> collectionForExpend = new ObservableCollection<ChartData>();
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
            // 设置图形的数据源
            pieChart.DataSource = collection;
            expandPieChart.DataSource = collectionForExpend;

            barChart.DataSource = collection;
            barChartForExpend.DataSource = collectionForExpend;

            //chart3.DataSource = from c in collectionForExpend.Where<ChartData>(c => c.Account != 0) select c;

        }
    }
}
