using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.dbItem
{
    internal class dbItem
    {
        public string shopName;
        public string shopAddress;
        public string clientName;
        public string clientEmail;
        public string clientPhone;
        public string categotyName;
        public string distrName;
        public int itemId;
        public string itemName;
        public int amount;
        public decimal price;
        public DateTime purchDate;

        public dbItem(string shopName, string shopAddress, string clientName, string clientEmail,
                    string clientPhone, string categotyName, string distrName, int itemId, string itemName,
                    int amount, decimal price, DateTime purchDate)
        {
            this.shopName = shopName;
            this.shopAddress = shopAddress;
            this.clientName = clientName;
            this.clientEmail = clientEmail;
            this.clientPhone = clientPhone;
            this.categotyName = categotyName;
            this.distrName = distrName;
            this.itemId = itemId;
            this.itemName = itemName;
            this.amount = amount;
            this.price = price;
            this.purchDate = purchDate;
        }

        public override string ToString()
        {
            return shopName + " " + shopAddress + " " + clientName + " " + clientEmail + " " + clientPhone + " " + categotyName + " " + distrName + " " + itemId + " " + itemName + " " + amount + " " + price + " " + purchDate;
        }
    }
}
