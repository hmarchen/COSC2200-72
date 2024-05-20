/**
 * Authors: Sheizah Jimenez, Hlib Marchenko, Raisa Nasara, Zhanibek Kapen
 * Date Updated: 04/14/2024
 * Description: Represents a player in the Durak game.
 **/
using System;
using System.Collections;
using System.Collections.Generic;

namespace DurakConsole
{
    public class Player : Hand
    {
        // Default constructor
        public Player() : base() { }

        public List<Card> GetPlayableCards(GameTable table, bool isAttacking)
        {
            List<Card> playableCards = new List<Card>();

            foreach(Card card in this.GetCards())
            {
                // if attacking
                if (isAttacking && table.CanAttack(card)) 
                    playableCards.Add(card);

                // if defending
                else if (!isAttacking && table.CanDefend(card)) 
                    playableCards.Add(card);
            }

            return playableCards;
        }
    }
}
