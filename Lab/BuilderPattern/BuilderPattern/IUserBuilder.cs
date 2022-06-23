using BuilderPattern.AccountUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    interface IUserBuilder
    {
        UserBuilder AddId(int id);
        UserBuilder AddName(string name);
        UserBuilder AddDate(string date);
        UserBuilder AddAddress(string address);
        UserBuilder AddAccount(Account account);
        User Build();
    }
}
