/**
 * Authors: Sheizah Jimenez, Hlib Marchenko, Raisa Nasara, Zhanibek Kapen
 * Date Updated: 04/14/2024
 * Description: Represents the game table in the Durak game.
 **/
using System;
using System.Collections.Generic;
using System.Linq;

namespace DurakConsole
{
    public class GameTable
    {
        public static List<Card> Table = new List<Card>(); // List to store cards played on the table

        /**
         * Get all possible card suits played from the table
         */
        public List<string> GetAllowedSuits()
        {
            List<string> allowedSuits = new List<string> { };
            foreach (Card card in Table)
            {
                string cardSuit = card.suit();
                if (!allowedSuits.Contains(cardSuit))
                    allowedSuits.Add(card.suit());
            }

            return allowedSuits;
        }

        /**
         * Get all possible card suits played from the table
         */
        public List<string> GetAllowedRanks()
        {
            List<string> allowedRanks = new List<string> { };
            foreach (Card card in Table.ToList())
            {
                if (card != null)
                {
                    string cardRank = card.rank();
                    if (!allowedRanks.Contains(cardRank))
                        allowedRanks.Add(card.rank());
                }
            }

            return allowedRanks;
        }

        /**
         * Get all possible card ranks played from the table
         */
        public List<int> GetTableValues()
        {
            List<string> ranks = this.GetAllowedRanks();
            List<int> rankValues = new List<int> { };

            // get rank values
            foreach (string rank in ranks)
            {
                int value = Rank.GetRankValue(rank);

                if (!rankValues.Contains(value))
                    rankValues.Add(value);
            }

            // sort list
            rankValues.Sort();

            return rankValues;
        }

        /**
         * Checks if a card is able to be played for attacking
         */
        public bool CanAttack(Card card)
        {
            List<string> tableRanks = this.GetAllowedRanks();
            string cardRank = card.rank();

            string trumpSuit = Deck.getTrumpCard().suit();

            // allow if table is empty, or card is a rank that has already been played
            if (Table.Count == 0 || tableRanks.Contains(cardRank))
                return true;
            return false;
        }

        /**
         * Checks if a card is able to be played for attacking
         */
        public bool CanDefend(Card card)
        {
            // ignore if no cards are in the table
            if (Table.Count < 1) return false;

            Card attackingCard = Table[Table.Count - 1];
            return Card.CompareRanks(card, attackingCard);
        }

        // Adds a card to the table
        public void addToTable(Card card)
        {
            Table.Add(card);
        }

        // Clears the table of all cards
        public void ClearTable()
        {
            Table.Clear();
        }

        // Displays the cards currently on the table
        public void DisplayTable()
        {
            foreach (Card card in Table)
            {
                Console.WriteLine(card.ToString());
            }
        }

        // Cleans the table (same as cleanTable method)
        public void TableClean()
        {
            Table.Clear();
        }

        // Resets the game by clearing the table and the deck
        public static void GameReset()
        {
            Table.Clear();
            Deck.mainDeck.Clear();
        }

        // Returns the number of cards currently on the table
        public int Size()
        {
            return Table.Count();
        }

        // Returns the card at the specified index on the table
        public Card UnderIndex(int index)
        {
            return Table[index];
        }

        // Moves a card from the table to a player's hand
        public void TakeFromTable(Card card, Hand hand)
        {
            hand.AddCard(card); // Add the card to the hand
            Table.Remove(card); // Remove the card from the table
        }

        // Overrides the ToString method to return a string representation of the table
        public override string ToString()
        {
            string allCards = "";

            if (Table == null) return null;

            foreach (Card card in Table)
            {
                if (card != null)
                    allCards += card.ToString() + ", ";
                else
                    allCards += "NULL CARD, ";
            }
            return allCards;
        }
    }
}
