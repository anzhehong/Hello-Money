using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationMenuSample
{
    class LINQ
    {

        /// <summary>
        /// 获取所有的记账记录
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Record>> GetAllRecords()
        {
            return (from c in await App.recordHelper.GetData() select c);
        }
        /// <summary>
        /// 获取某月的所有记录
        /// </summary>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>记账记录枚举集合</returns>
        public static async Task<IEnumerable<Record>> GetThisMonthAllRecords(int month, int year)
        {
            return (from c in await App.recordHelper.GetData()
                    where (c.RecordTime.Month == month) && (c.RecordTime.Year == year)
                    select c);
        }
        /// <summary>
        /// Fetch all incomes for a specific month
        /// </summary>
        public static async Task<IEnumerable<Record>> GetThisMonthAllRecordsForType(int month , int year , int type)
        {
            return (from c in await GetThisMonthAllRecords(month, year)
                    where c.Type == type
                    select c);
        }
        
            
        /// <summary>
        /// 获取某日的所有记录
        /// </summary>
        /// <param name="day">日</param>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>记账记录枚举集合</returns>
        public static async Task<IEnumerable<Record>> GetThisDayAllRecords(int day, int month, int year)
        {
            return (from c in await App.recordHelper.GetData()
                    where (c.RecordTime.Day == day) && (c.RecordTime.Month == month) && (c.RecordTime.Year == year)
                    select c);
        }
        /// <summary>
        /// 获取本年的所有记录
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>记账记录枚举集合</returns>
        public static async Task<IEnumerable<Record>> GetThisYearAllRecords(int year)
        {
            return (from c in await App.recordHelper.GetData()
                    where c.RecordTime.Year == year
                    select c);
        }
        /// <summary>
        /// 获取本月的类别总金额
        /// </summary>
        /// <param name="ItemName">类别名称</param>
        /// <param name="type">类别类型</param>
        /// <returns>金额</returns>
        public static async Task<double> GetThisMonthSummaryOf(string ItemName, short type)
        {
            return ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                          where ((c.RecordTime.Month == DateTime.Now.Month) && (c.RecordTime.Year == DateTime.Now.Year)) && (c.Type == type) && (c.Category == ItemName)
                                          select c.Amount)).Sum();
        }

        ///// <summary>
        /////  获取本月的类别总金额
        ///// </summary>
        ///// <param name="category">类别实体</param>
        ///// <returns>金额</returns>
        //public static async Task<double> GetThisMonthSummaryOf(Category category)
        //{
        //    return ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
        //                                  where ((c.RecordTime.Month == DateTime.Now.Month) && (c.RecordTime.Year == DateTime.Now.Year)) && (c.Type == category.Type) && (c.Category == category.Name)
        //                                  select c.Amount)).Sum();
        //}

        /// <summary>
        /// 获取总支出
        /// </summary>
        /// <returns>金额</returns>
        public static async Task<double> GetSummaryExpenses()
        {
            return ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                          where c.Type == 1
                                          select c.Amount)).Sum();
        }
        /// <summary>
        /// 获取本年的支出
        /// </summary>
        /// <returns>金额</returns>
        public static async Task<double> GetThisYearSummaryExpenses()
        {
            return ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                          where ((c.RecordTime.Year == DateTime.Now.Year)) && (c.Type == 1)
                                          select c.Amount)).Sum();
        }
        /// <summary>
        /// 获取某年的支出
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static async Task<double> GetYearSummaryExpenses(int year)
        {
            return ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                          where ((c.RecordTime.Year == year)) && (c.Type == 1)
                                          select c.Amount)).Sum();
        }
        /// <summary>
        /// 获取本月的支出
        /// </summary>
        /// <returns></returns>
        public static async Task<double> GetThisMouthSummaryExpenses()
        {
            return ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                          where ((c.RecordTime.Year == DateTime.Now.Year)) && ((c.RecordTime.Month == DateTime.Now.Month)) && (c.Type == 1)
                                          select c.Amount)).Sum();
        }
        /// <summary>
        /// 获取某月的支出
        /// </summary>
        /// <param name="mouth">月份</param>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static async Task<double> GetMouthSummaryExpenses(int mouth, int year)
        {
            return ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                          where ((c.RecordTime.Year == year)) && ((c.RecordTime.Month == mouth)) && (c.Type == 1)
                                          select c.Amount)).Sum();
        }
        /// <summary>
        /// 获取总收入
        /// </summary>
        /// <returns>金额</returns>
        public static async Task<double> GetSummaryIncome()
        {
            return ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                          where c.Type == 0
                                          select c.Amount)).Sum();
        }
        /// <summary>
        /// 获取本年的收入
        /// </summary>
        /// <returns>金额</returns>
        public static async Task<double> GetThisYearSummaryIncome()
        {
            return ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                          where ((c.RecordTime.Year == DateTime.Now.Year)) && (c.Type == 0)
                                          select c.Amount)).Sum();
        }
        /// <summary>
        /// 获取某年的收入
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static async Task<double> GetYearSummaryIncome(int year)
        {
            return ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                          where ((c.RecordTime.Year == year)) && (c.Type == 0)
                                          select c.Amount)).Sum();
        }
        /// <summary>
        /// 获取本月的收入
        /// </summary>
        /// <returns>金额</returns>
        public static async Task<double> GetThisMouthSummaryIncome()
        {
            return ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                          where ((c.RecordTime.Year == DateTime.Now.Year)) && ((c.RecordTime.Month == DateTime.Now.Month)) && (c.Type == 0)
                                          select c.Amount)).Sum();
        }
        /// <summary>
        /// 获取某月的收入
        /// </summary>
        /// <param name="mouth">月份</param>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static async Task<double> GetMouthSummaryIncome(int mouth, int year)
        {
            return ((IEnumerable<double>)(from c in await App.recordHelper.GetData()
                                          where ((c.RecordTime.Year == year)) && ((c.RecordTime.Month == mouth)) && (c.Type == 0)
                                          select c.Amount)).Sum();
        }
        /// <summary>
        /// 查询记账记录
        /// </summary>
        /// <param name="begin">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="keyWords">关键字</param>
        /// <returns>记账记录</returns>
        //public static async Task<IEnumerable<Record>> Search(DateTime? begin, DateTime? end, string keyWords)
        //{
        //    if (keyWords == "")
        //    {
        //        return (from c in await App.recordHelper.GetData()
        //                where c.RecordTime >= begin && c.RecordTime <= end
        //                select c);
        //    }
        //    else
        //    {
        //        return (from c in await App.recordHelper.GetData()
        //                where c.RecordTime >= begin && c.RecordTime <= end && c.IndexOf(keyWords) >= 0
        //                select c);
        //    }
        //}
    }
}
