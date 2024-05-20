/**
 * Authors: Sheizah Jimenez, Hlib Marchenko, Raisa Nasara, Zhanibek Kapen
 * Date Updated: 04/14/2024
 * Description: Represents the ranks of cards in a standard deck.
 **/
using System.Collections.Generic;

namespace DurakConsole
{
    public static class Rank
    {
        // Array of rank names
        public static string[] ranks =
        {
            "Ace",
            "King",
            "Queen",
            "Jack",
            "10",
            "9",
            "8",
            "7",
            "6"
        };

        // Dictionary to map rank names to numerical values
        public static Dictionary<string, int> values = new Dictionary<string, int>()
        {
            { "Ace", 13 },     // Ace has the highest value
            { "King", 12 },    // King
            { "Queen", 11 },   // Queen
            { "Jack", 10 },    // Jack
            { "10", 9 },       // Numerical ranks
            { "9", 8 },
            { "8", 7 },
            { "7", 6 },
            { "6", 5 }
            // Add more ranks as needed
        };

        // Method to get the numerical value of a rank
        public static int GetRankValue(string rank)
        {
            if (values.ContainsKey(rank))
                return values[rank];
            else
                return 0;
        }
    }
}
