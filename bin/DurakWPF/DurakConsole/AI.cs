/**
 * Authors: Sheizah Jimenez, Hlib Marchenko, Raisa Nasara, Zhanibek Kapen
 * Date Updated: 04/14/2024
 * Description:  AI class representing the computer player in the Durak game.
 */
using System;
using System.Collections;
using System.Collections.Generic;

namespace DurakConsole
{
    public class AI : Hand
    {

        // Defend method to choose a card for defense
        public Card Defend(GameTable table)
        {
            Card cardPlayed = null;
            List<Card> SuperCards = new List<Card>();

            // Loops through Ai's hand to find suitable cards for defense
            foreach (Card card in this.GetCards())
            {
                // Sup also Superior card is the card that can beat the opponents card
                bool isSup = Card.CompareRanks(card, GameTable.Table[table.Size() - 1]);
                if (isSup)
                {
                    // adds to the list of superior cards
                    SuperCards.Add(card);
                }
            }

            // if no suitable cards were found, scoop all cads on the table
            if (SuperCards.Count > 0)
            {
                // initiates random object
                Random random = new Random();
                // determines the card that will be the next Ai's move
                cardPlayed = SuperCards[random.Next(0, SuperCards.Count == 1 ? SuperCards.Count : SuperCards.Count - 1)];
            }

            return cardPlayed;
        }

        // Attack method to choose a card for attacking
        public Card Attack(GameTable table)
        {
            Card cardPlayed = null;
            List<Card> playableCards = new List<Card>();

            // if table is empty, play first card
            if (table.Size() == 0)
                return GetCards()[0];

            // get cards in hand
            foreach (Card card in GetCards())
            {
                if (table.CanAttack(card))
                    playableCards.Add(card);
            }

            // play a random card to attack
            if (playableCards.Count > 0)
            {
                Random random = new Random();
                cardPlayed = playableCards[random.Next(0, playableCards.Count == 1 ? playableCards.Count : playableCards.Count - 1)];
            }

            return cardPlayed;
        }
    }
}
