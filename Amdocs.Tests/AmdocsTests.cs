using System.Collections.Generic;
using Xunit;

namespace Amdocs.Tests
{
    public class AmdocsTests
    {
        [Fact]
        public void AssertThatAllParticipantsWinTheirPercentage()
        {
            // Setup
            var gamePlays = 1000000;
            var horses = GetMockedHorses(true);
            var horseExpectedWins = new int[horses.Count];
            var horseWins = new int[horses.Count];

            for (int i = 0; i < horses.Count; i++)
            {
                horseExpectedWins[i] = (int)horses[i].ChancesToWin * gamePlays / 100;
                horseWins[i] = 0;
            }

            // Act

            for (var i = 0; i < gamePlays; i++)
            {
                var winner = Helpers.CalculateWinner(horses);
                var winnerId = 0;
                foreach (var horse in horses)
                {
                    if (horse == winner)
                    {
                        break;
                    }

                    winnerId++;
                }

                horseWins[winnerId]++;
            }

            // Assert
            for (var i = 0; i < horses.Count; i++)
            {
                Assert.InRange(horseWins[i], horseExpectedWins[i] - gamePlays * 0.02, horseExpectedWins[i] + gamePlays * 0.02);
            }
        }


        private List<Horse> GetMockedHorses(bool calculateMargin)
        {
            var horses = new List<Horse>();
            for (var i = 0; i < 4; i++)
            {
                horses.Add(new Horse());
            }


            horses[0].TrySetOddsPrice("2/1");
            horses[1].TrySetOddsPrice("1/2");
            horses[2].TrySetOddsPrice("3/1");
            horses[3].TrySetOddsPrice("8/1");

            horses[0].TrySetName("A");
            horses[1].TrySetName("AB");
            horses[2].TrySetName("AC");
            horses[3].TrySetName("AD");

            var margin = calculateMargin ? Helpers.CalculateMargin(horses) : 136.11;

            foreach (var horse in horses)
            {
                horse.CalculateChancesToWin(margin);
            }

            return horses;
        }
    }
}
