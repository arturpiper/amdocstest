using System;

namespace Amdocs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Horses game starts");
            new Game().Run(4, 16);
            Console.ReadKey();
        }
    }
}
