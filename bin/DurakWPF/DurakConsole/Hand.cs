/**
 * Authors: Sheizah Jimenez, Hlib Marchenko, Raisa Nasara, Zhanibek Kapen
 * Date Updated: 04/14/2024
 * Description: Represents a player's hand in the Durak game.
 **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DurakConsole
{
    public class Hand
    {
        List<Card> cards; // List to store the cards in the hand

        // Constructor to initialize the hand with an empty list of cards
        public Hand()
        {
            cards = new List<Card>();
        }

        /**
         * Sends the given card to the provided table
         */
        public void PlayCard(GameTable table, Card card)
        {
            table.addToTable(card); // Add the card to the table
            this.RemoveCard(card); // Remove the card from the hand
        }

        /**
         * Takes all cards from table and adds into the hand
         */
        public List<Card> TakeAllCards(GameTable table)
        {
            List<Card> cardsTaken = new List<Card>();
            Console.WriteLine($"Table: {table}");

            foreach(Card card in GameTable.Table.ToList())
            {
                table.TakeFromTable(card, this);
                cardsTaken.Add(card);
            }

            Console.WriteLine($"Cards Taken Count: {cardsTaken.Count}");

            return cardsTaken;
        }

        /**
        * Returns Card object by the specified index from the list
        */
        public Card GetCard(int index)
        {
            return cards[index];
        }

        // Removes the specified card from the hand
        public void RemoveCard(Card card)
        {
            cards.Remove(card);
        }

        // Adds the specified card to the hand
        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        // Displays the cards in the hand
        public void DisplayHand()
        {
            int index = 0;
            foreach (Card card in cards)
            {
                index++;
                Console.WriteLine($"({index}) " + card.ToString());
            }
        }

        // Draw cards from the deck and add them to the hand
        public List<Card> DrawCards(Deck deck, int amount = 1)
        {
            List<Card> drawnCards = deck.DrawCards(amount);

            foreach (Card card in drawnCards)
                cards.Add(card);

            return drawnCards;
        }

        // Returns the number of cards in the hand
        public int Size()
        {
            return cards.Count;
        }

        // Clears the hand by removing all cards
        public void Clear()
        {
            cards.Clear();
        }

        // Returns the list of cards in the hand
        public List<Card> GetCards()
        {
            return cards;
        }

        // Returns the card with the specified name
        public Card GetCard(string name)
        {
            foreach (Card card in cards)
            {
                if (card.ToString() == name) return card;
            }
            return null;
        }

        // Method placeholder for drawing a complete hand from the deck (not implemented yet)
        internal void drawHand(Deck deck)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            List<string> cardNames = new List<string>();
            foreach (Card card in cards)
            {
                cardNames.Add(card.ToString());
            }

            return String.Join(", ", cardNames.ToArray());
        }
    }
}
