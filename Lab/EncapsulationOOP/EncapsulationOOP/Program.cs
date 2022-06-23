using System;

namespace EncapsulationOOP
{
    class Customer
    {
        private int Id;
        public int CustomerID
        {
            get
            {
                return Id;
            }
            set
            {
                Id = value;
            }
        }
        public string CustomerName { get; set; } = "New customer";
        public void Print()
        {
            Console.WriteLine($"ID:{CustomerID}, Name:{CustomerName}");
        }
    }

    public class MyClass
    {
        public int x { get; init; }
        public int y { get; }
        public MyClass()
        {
            x = 10;
            y = 20;
        }
        public MyClass(int a, int b)
        {
            x = a;
            y = b;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Customer c = new Customer();
            //c.CustomerID = 1000;
            //Console.WriteLine($"ID:{c.CustomerID}, Name:{c.CustomerName}");
            //c.CustomerID = 2000;
            //c.CustomerName = "Jack";
            //c.Print();
            //Console.ReadLine();

            MyClass m = new MyClass { x = 1 };
            Console.WriteLine($"x:{m.x}, y:{m.y}");
            MyClass m2 = new MyClass();
            Console.WriteLine($"x:{m2.x}, y:{m2.y}");
            MyClass m3 = new MyClass(30,50);
            Console.WriteLine($"x:{m2.x}, y:{m2.y}");
            Console.ReadLine();

        }
    }
}
