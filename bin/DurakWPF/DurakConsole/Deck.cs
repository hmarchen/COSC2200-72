/**
 * Authors: Sheizah Jimenez, Hlib Marchenko, Raisa Nasara, Zhanibek Kapen
 * Date Updated: 04/14/2024
 * Description: Represents a deck of cards in the Durak game.
 **/
using System;
using System.Collections.Generic;
using System.Linq;

namespace DurakConsole
{
    public class Deck
    {
        // List to store the main deck of cards
        public static List<Card> mainDeck = new List<Card>();
        private static Card trumpCard; // Variable to store the trump card

        // Creates a standard deck of 52 cards and stores them in the main deck
        public virtual void makeDeck()
        {
            mainDeck.Clear(); // Clear the main deck

            // Generate cards for each suit and rank
            foreach (var suit in Suit.suits)
            {
                foreach (var rank in Rank.ranks)
                {
                    Card card = new Card(suit, rank); // Create a new card
                    mainDeck.Add(card); // Add the card to the main deck
                }
            }
        }

        // Shuffles the main deck of cards
        public static void shuffleDeck()
        {
            Random random = new Random();

            int n = mainDeck.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                // Swap list[i] and list[j]
                Card temp = mainDeck[i];
                mainDeck[i] = mainDeck[j];
                mainDeck[j] = temp;
            }
        }

        // Displays the cards in the main deck
        public static void DisplayDeck()
        {
            foreach (var card in mainDeck)
            {
                Console.WriteLine(card.ToString());
            }
        }

        // Draws the top card from the main deck
        public static Card Draw()
        {
            Card poppedCard = mainDeck[0]; // Retrieve the first card
            mainDeck.RemoveAt(0); // Remove the first card from the main deck
            return poppedCard; // Return the drawn card
        }

        // Draws a specified number of cards from the main deck
        public List<Card> DrawCards(int numberOfCardsToDraw)
        {
            List<Card> cardsDrawn = new List<Card>();

            // Ensure there are enough cards in the deck to draw
            if (numberOfCardsToDraw <= mainDeck.Count)
            {
                for (int i = 0; i < numberOfCardsToDraw; i++)
                {
                    // Draw the card and add it to cardsDrawn list
                    Card drawnCard = mainDeck[0];
                    cardsDrawn.Add(drawnCard);

                    // Remove the drawn card from the main deck
                    mainDeck.RemoveAt(0);
                }
            }
            else
            {
                // TODO: Handle insufficient cards in the deck. End Game logic
            }

            return cardsDrawn;
        }

        // Sets the top card from the main deck as the trump card
        public static void setTrumpCard(Deck mainDeck)
        {
            Card trumpCard;
            trumpCard = Deck.Draw(); // Draw the top card from the main deck
            Deck.trumpCard = trumpCard; // Set the drawn card as the trump card
        }

        // Gets the trump card
        public static Card getTrumpCard()
        {
            return trumpCard;
        }

        // Returns the number of cards in the main deck
        public int Size()
        {
            return mainDeck.Count();
        }

        // Returns a list of all cards in the main deck
        public List<Card> GetCards()
        {
            return mainDeck;
        }

        // Returns the card with the specified name from the main deck
        public Card GetCard(string name)
        {
            foreach (Card card in mainDeck)
            {
                if (card.ToString() == name) return card;
            }
            return null;
        }
    }
}
