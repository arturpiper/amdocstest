using System;
using System.Collections.Generic;
using System.Linq;

namespace Amdocs
{
    public static class Helpers
    {
        public static Horse CalculateWinner(List<Horse> horses)
        {
            double rangeOfRandomStart = 0;

            foreach (var horse in horses)
            {
                horse.SetPossibilityRange(rangeOfRandomStart, horse.ChancesToWin);
                rangeOfRandomStart += horse.ChancesToWin;
            }

            var aimPercentage = (double)new Random().Next(0, 10000) / 100; // multiplied with 100% * 10^2 because of 2 decimal numbers

            return horses
                .First(h => h.PossibilityRangeMin <= aimPercentage && h.PossibilityRangeMax >= aimPercentage);
        }


        public static double CalculateMargin(List<Horse> horses)
        {
            double margin = 0;
            foreach (var horse in horses)
            {
                margin += Math.Round(100 / ((double)horse.OddsPriceX / horse.OddsPriceY + 1), 2);
            }

            return margin;
        }
    }
}
