using System;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            byte a = 16;
            byte b = 54;
            byte c = (byte)(a ^ b);
            Console.WriteLine(c);
        }
    }
}
