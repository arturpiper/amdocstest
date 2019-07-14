using System;
using System.Collections.Generic;
using System.Linq;

namespace Amdocs.Helpers
{
    public static class GameHelpers
    {
        public static Horse CalculateWinner(List<Horse> horses)
        {
            var aimPercentage = (double)new Random().Next(50, 100);

            return horses
                .OrderBy(h => Math.Abs(Math.Round(h.ChancesToWin - aimPercentage, 2)))
                .First();
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
