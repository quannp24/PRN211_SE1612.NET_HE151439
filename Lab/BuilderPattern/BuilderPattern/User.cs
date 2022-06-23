using BuilderPattern.AccountUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    class User
    {
        public int id { get; set; }
        public string name { set; get; }
        public string date { get; set; }
        public string address { set; get; }
        public Account Account { get; set; }

        public User(int id, string name, string date, string address, Account account)
        {
            this.id = id;
            this.name = name;
            this.date = date;
            this.address = address;
            Account = account;
        }

        public override string ToString()
        {
            var content = "";
            content += $"Number ID:{id}\n";
            content += $"Name:{name}\n";
            content += $"Date of birth:{date}\n";
            content += $"Address of person:{address}\n";
            content += $"Account of person:{Account.Username}\n";
            return content;
        }
    }

}
