using System;
using System.Collections;
namespace DURAK_CLI
{
    public class Player : Hand
    {
        static List<Card> PlayerHand = new List<Card>();


        // Draw 6 cards
        public void drawHand(Deck deck)
        {
            int drawLimit = 6;

            // Draw 6 cards from the deck
            List<Card> drawnCards = deck.DrawCards(drawLimit);

            // Add the drawn cards to the player's hand
            PlayerHand.AddRange(drawnCards);
        }

        // Attack method, gets the card from N hand and puts Card on the GameTable
        public void Attack(Card card, GameTable table, Hand PlayerHand) {
            table.addToTable(card);
            PlayerHand.RemoveCard(card);
        }


    }
}
