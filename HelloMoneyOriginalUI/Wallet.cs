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
        public List<Wallet> _walletData;
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

        // data initial func
        internal static List<Wallet> getWallets()
        {
            List<Wallet> list = new List<Wallet>();
            list.Add(new Wallet { walletName = "支付宝", walletDescription = "阿里公司支付宝", walletImg = "/Assets/zhifubao.png", walletValue = 111 });
            list.Add(new Wallet { walletName = "现金", walletDescription = "口袋money", walletImg = "/Assets/cash.png", walletValue = 0 });
            list.Add(new Wallet { walletName = "银行卡", walletDescription = "各种银行卡", walletImg = "/Assets/creditcard.png", walletValue = 0 });
            list.Add(new Wallet { walletName = "其它", walletDescription = "除了上面的所有money", walletImg = "/Assets/others.png", walletValue = 0 });
            return list;
        }

        // func to return list
        public async Task<List<Wallet>> GetData()
        {
            if (this._walletData == null)
            {
                bool isExist = await LoadFromFile();
                if (!isExist)
                {
                    this._walletData = getWallets();
                }
            }
            return this._walletData;
        }
        
    }

    public class WalletManager
    {
        // read all wallet record
        public static async Task<IEnumerable<Wallet>> GetAllWallets()
        {
            return (from c in await App.walletHelper.GetData() select c);
        }

        // set value according to wallet name
        public async Task<string> setValueByName(string walletName, double newValue)
        {
            IEnumerable<Wallet> wallets = (from c in await App.walletHelper.GetData() select c);
           
            List<Wallet> tempList = wallets.ToList();
            for (int i = 0; i < tempList.Count(); i++)
            {
                if (tempList[i].walletName.Equals(walletName))
                {
                    tempList[i].walletValue = newValue;
                    App.walletHelper._walletData = tempList;
                    App.walletHelper.SaveToFile();
                    return "Set Value Successfully!";
                }
            }
            return "Something Wrong!";
        }

        // add wallet type
        public async Task<string> addNewWallet(string name, double value, string description)
        {
            IEnumerable<Wallet> wallets = (from c in await App.walletHelper.GetData() select c);

            List<Wallet> tempList = wallets.ToList();
            tempList.Add(new Wallet
            {
                walletDescription = description,
                walletName = name,
                walletValue = value,
                walletImg = "/Assets/others"
            });
            App.walletHelper._walletData = tempList;
            App.walletHelper.SaveToFile();

            return "Add Successfully";
        }

    }
}
