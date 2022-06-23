using System;
using System.IO;
using System.Text;

namespace Slot12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****Demo*****\n");
            using FileStream f = File.Open("MyFile.txt", FileMode.Create);
            string msg = "ABCDEFGH";
            byte[] msgAsByte = Encoding.Default.GetBytes(msg);
            f.Write(msgAsByte, 0, msgAsByte.Length);
            f.Position = 0;
            Console.Write("Print message as an array of bytes: \n");
            byte[] bytesFrom = new byte[msgAsByte.Length];
            for (int i = 0; i < msgAsByte.Length; i++)
            {
                bytesFrom[i] = (byte)f.ReadByte();
                Console.Write($"{bytesFrom[i],5}");
            }
            Console.Write("\nDecoded Message: ");
            Console.WriteLine(Encoding.Default.GetString(bytesFrom));
            Console.ReadLine();
        }
    }

    
}
    
