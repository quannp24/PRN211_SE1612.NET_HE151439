using BuilderPattern.AccountUser;
using System;

namespace BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new UserBuilder()
                .AddAccount(new Account("quannphe151439", "12345678"))
                .AddId(1)
                .AddName("Nguyen Phan Quan")
                .AddAddress("Bac Ninh")
                .AddDate("24/05/2001")
                .Build();
            Console.WriteLine(builder.ToString());
            Console.ReadLine();
        }
    }
}
