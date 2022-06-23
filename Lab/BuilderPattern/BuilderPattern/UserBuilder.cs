using BuilderPattern.AccountUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    class UserBuilder: IUserBuilder
    {
        public int id { get; set; }
        public string name { set; get; }
        public string date { get; set; }
        public string address { set; get; }
        public Account Account { get; set; }

        public UserBuilder AddAccount(Account account)
        {
            Account = account;
            return this;
        }
        public UserBuilder AddId(int Id)
        {
            id = Id;
            return this;
        }
        public UserBuilder AddName(string Name)
        {
            name = Name;
            return this;
        }
        public UserBuilder AddAddress(string Address)
        {
            address = Address;
            return this;
        }
        public UserBuilder AddDate(string Date)
        {
            date = Date;
            return this;
        }

        public User Build()
        {
            return new User(id, name, date, address, Account);
        }

    }
}
