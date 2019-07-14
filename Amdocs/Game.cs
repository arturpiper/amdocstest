using System;
using System.Collections.Generic;
using Amdocs.Helpers;

namespace Amdocs
{
    public class Game
    {
        private readonly List<Horse> _horses;
        private int HorseCount { get; set; }
        private double Margin { get; set; }

        public Game()
        {
            _horses = new List<Horse>();
        }

        public void Run(int minParticipants, int maxParticipants)
        {
            ConfigureHorsesCount(minParticipants, maxParticipants);
            PrepareHorses(_horses);
            Margin = GameHelpers.CalculateMargin(_horses);
            
            if (Margin < 110 || Margin > 140)
            {
                Console.WriteLine(string.Format("Cannot play with this margin: {0}%", Margin));
                return;
            }

            CalculateChances(_horses, Margin);

            var winner = GameHelpers.CalculateWinner(_horses);

            Console.Clear();
            Console.WriteLine(string.Format("the winner is horse {0} with chances to win: {1}", winner.Name, winner.ChancesToWin));
        }


        private void CalculateChances(List<Horse> horses, double margin)
        {
            foreach (var horse in horses)
            {
                horse.CalculateChancesToWin(margin);
            }
        }


        private void PrepareHorses(ICollection<Horse> horses)
        {
            for (var horseIndex = 1; horseIndex <= HorseCount; horseIndex++)
            {
                var horse = new Horse();

                Console.Clear();
                Console.WriteLine(string.Format("Please give horse nr {0}. a name (max 18 symbols A-Z): ", horseIndex));

                while (!horse.TrySetName(Console.ReadLine()))
                {
                    Console.WriteLine("\nInvalid input. Please try again: ");
                }

                Console.Clear();
                Console.WriteLine("Give the horse fractional odds price. Format : x/y, where x and y are numbers > 1 ");

                while (!horse.TrySetOddsPrice(Console.ReadLine()))
                {
                    Console.WriteLine("\nInvalid input. Please try again: ");
                }
                
                horses.Add(horse);
            }
        }

        private void ConfigureHorsesCount(int minParticipants, int maxParticipants)
        {
            if (minParticipants < 1 || maxParticipants < 1)
            {
                throw new ArgumentException("There should be at least 1 participant");
            }

            var inputCount = int.MinValue;
            Console.WriteLine(string.Format("Please enter participant horses count from {0} to {1} and press ENTER", minParticipants, maxParticipants));
            while (!int.TryParse(Console.ReadLine(), out inputCount) || inputCount > maxParticipants || inputCount < minParticipants)
            {
                Console.WriteLine("\nInvalid input. Please try again: ");
            }

            HorseCount = inputCount;
            Console.Clear();
        }
    }
}
