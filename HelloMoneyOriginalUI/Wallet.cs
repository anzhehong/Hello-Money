using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationMenuSample.Models
{
    public class Wallet
    {
        // 账户名称
        public string walletName { get; set; }
        // 账户描述
        public string walletDescription { get; set; }
        // 账户余额
        public double walletValue { get; set; }
        // 账户图片
        public string walletImg { get; set; }
        // TODO:未完待续
    }


    public class WalletHelper
    {
        private List<Wallet> _walletData;
        public double balance = 123; //{ get; set; }
        // load wallet.dat
        public async Task<bool> LoadFromFile()
        {
            this._walletData = await DataStorageHelper.ReadAsync<List<Wallet>>("Wallet.dat");
            return (this._walletData != null);
        }
        // save to wallet.dat
        public async void SaveToFile()
        {
            await DataStorageHelper.WriteAsync<List<Wallet>>(this._walletData, "Wallet.dat");
        }
        // func to return list
        public async Task<List<Wallet>> GetData()
        {
            if (this._walletData == null)
            {
                bool isExist = await LoadFromFile();
                if (!isExist)
                {
                    this._walletData = new List<Wallet>();
                }
            }
            return this._walletData;
        }

        internal static List<Wallet> testGet()
        {
            //           throw new NotImplementedException();
            List<Wallet> temp = new List<Wallet>();
            Wallet ttt = new Wallet();
            ttt.walletName = "zhifubao";
            ttt.walletDescription = "ali zhifubao";
            ttt.walletImg = "/Assets/zhifubao.png";
            ttt.walletValue = 1111;
            for (int i = 0; i < 5; i++)
            {
                temp.Add(ttt);
            }
            return temp;

        }

        // return balance
        internal static async Task<double> getPresentBalance()
        {
            //throw new NotImplementedException();
            WalletHelper helper = new WalletHelper();
           
            List<Wallet> collection = await helper.GetData();
            // 目前设置为123
            foreach (var item in collection)
            {
                helper.balance += item.walletValue;
            }
            return helper.balance;
            //return helper.balance;
        }

    }
}
