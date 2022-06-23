using static System.Console;
using static MyLibary.MyClass;

namespace MySolution
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 50, b = 25;
            int result;
            WriteLine("******Demo*******");
            result = a.Add(b);
            WriteLine($"{a}-{b}={result}");
            ReadLine();
        }
    }
}
