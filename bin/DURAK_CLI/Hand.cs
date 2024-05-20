using System;
using System.Collections;
using System.ComponentModel;

namespace DURAK_CLI
{

    public class Hand
    {
        List<Card> cards;

        public Hand()
        {
            cards = new List<Card>();
        }

        /**
        * Returns Card object by the specifide index from the list
        */
        public Card UnderIndex(int index)
        {
            return cards[index];
        }

        public void RemoveCard(Card card)
        {
            cards.Remove(card);
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public void DisplayHand()
        {
            int index = 0;
            foreach (Card card in cards)
            {
                index++;
                Console.WriteLine($"({index}) " + card.ToString());
            }
        }


        // Draw 6 cards
        public void drawHand(Deck deck)
        {
            int drawLimit = 6;

            // Draw 6 cards from the deck
            List<Card> drawnCards = deck.DrawCards(drawLimit);

            // Add the drawn cards to the player's hand
            cards.AddRange(drawnCards);
        }


        public int length()
        {
            return cards.Count;
        }

        public void Clear()
        {
            cards.Clear();
        }
        public List<Card> cardS()
        {
            return cards;
        }
    }
}