using System;

namespace Amdocs
{
    class Program
    {
        static void Main(string[] args)
        {
            const int minParticipants = 4;
            const int maxParticipants = 16;
            const double minMargin = 110;
            const double maxMargin = 140;

            Console.WriteLine("Horses game starts");
            var game = new Game(minParticipants, maxParticipants, minMargin, maxMargin);
            game.Run();
            Console.WriteLine("Please enter any key to exit the game");
            Console.ReadKey();
        }
    }
}
