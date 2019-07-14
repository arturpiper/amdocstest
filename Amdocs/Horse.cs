using System;
using System.Text.RegularExpressions;

namespace Amdocs
{
    public class Horse
    {
        public string Name { get; private set; }
        public int OddsPriceX { get; private set; }
        public int OddsPriceY { get; private set; }
        public double ChancesToWin { get; private set; }

        public void CalculateChancesToWin(double margin)
        {
            ChancesToWin = Math.Round(100 / ((double) OddsPriceX / OddsPriceY + 1) / margin * 100, 2);
        } 

        public bool TrySetName(string name)
        {
            if (!Regex.IsMatch(name, "^[A-Z ]{1,18}$"))
            {
                return false;
            }

            Name = name;
            return true;
        }

        public bool TrySetOddsPrice(string oddsPrice)
        {
            if (!Regex.IsMatch(oddsPrice, "^[1-9][0-9]*[/][1-9][0-9]*$"))
            {
                return false;
            }
            var oddsPriceParts = Regex.Split(oddsPrice, "/");
            OddsPriceX = int.Parse(oddsPriceParts[0]);
            OddsPriceY = int.Parse(oddsPriceParts[1]);

            return true;
        }
    }
}