using System;
using System.Collections.Generic;
using System.Linq;

namespace Amdocs
{
    public class Game
    {
        private readonly int _minParticipants;
        private readonly int _maxParticipants;
        private readonly double _maxMargin;
        private readonly double _minMargin;

        private readonly List<Horse> _horses;

        private int HorseCount { get; set; }
        private double Margin { get; set; }


        public Game(int minParticipants, int maxParticipants, double minMargin, double maxMargin)
        {
            if (minParticipants < 1 || maxParticipants < 1 || minParticipants > maxParticipants || maxMargin < minMargin)
            {
                throw new ArgumentException("Please check game parameters");
            }

            _minParticipants = minParticipants;
            _maxParticipants = maxParticipants;
            _maxMargin = maxMargin;
            _minMargin = minMargin;

            _horses = new List<Horse>();
        }


        public void Run()
        {
            ConfigureHorsesCount();
            PrepareHorses(_horses);
            Margin = Helpers.CalculateMargin(_horses);

            if (Margin < _minMargin || Margin > _maxMargin)
            {
                Console.WriteLine($"Cannot play with this margin: {Margin}%");
                return;
            }

            CalculateChances(_horses, Margin);

            var winner = Helpers.CalculateWinner(_horses);

            Console.Clear();
            Console.WriteLine($"the winner is horse {winner.Name} with chances to win: {winner.ChancesToWin}%");
        }


        private void CalculateChances(List<Horse> horses, double margin)
        {
            foreach (var horse in horses)
            {
                horse.CalculateChancesToWin(margin);
            }
        }


        private void PrepareHorses(List<Horse> horses)
        {
            for (var horseIndex = 1; horseIndex <= HorseCount; horseIndex++)
            {
                var horse = new Horse();

                Console.Clear();
                Console.WriteLine($"Please give horse nr {horseIndex}. a name (max 18 symbols A-Z): ");

                while (!horse.TrySetName(Console.ReadLine()) || horses.Any(h => h.Name == horse.Name))
                {
                    Console.WriteLine("\nInvalid input or there is already a horse with this name. Please try again: ");
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


        private void ConfigureHorsesCount()
        {
            var inputCount = int.MinValue;
            Console.WriteLine($"Please enter participant horses count from {_minParticipants} to {_maxParticipants} and press ENTER");
            while (!int.TryParse(Console.ReadLine(), out inputCount) || inputCount > _maxParticipants || inputCount < _minParticipants)
            {
                Console.WriteLine("\nInvalid input. Please try again: ");
            }

            HorseCount = inputCount;
            Console.Clear();
        }
    }
}
