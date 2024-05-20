/**
 * Authors: Sheizah Jimenez, Hlib Marchenko, Raisa Nasara, Zhanibek Kapen
 * Date Updated: 04/14/2024
 * Description: Represents a playing card in the Durak game.
 */
using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DurakConsole
{
    public class Card
    {
        public string CardSuit { get; set; }
        public string CardRank { get; set; }
        public bool Trump { get; set; }

        // Parametrized constructor accepts suit and rank in order to create card object
        public Card(string suit, string rank)
        {
            CardSuit = suit;
            CardRank = rank;
        }

        // Returns whether the card is a trump card
        public bool isTrump()
        {
            return Trump;
        }

        // Returns the rank of the card
        public string rank()
        {
            return $"{CardRank}";
        }

        // Returns the suit of the card
        public string suit()
        {
            return $"{CardSuit}";
        }

        // Overrides ToString method to return a string representation of the card
        public override string ToString()
        {
            return $"{CardSuit}_{CardRank}";
        }

        // Compares ranks of two cards to determine superiority
        public static bool CompareRanks(Card card1, Card card2)
        {
            string trumpSuit = Deck.getTrumpCard().suit();

            // Check if one card is a trump and the other is not
            if (card1.suit() == trumpSuit && card2.suit() != trumpSuit)
            {
                return true; // card1 is a trump and wins
            }
            else if (card1.suit() != trumpSuit && card2.suit() == trumpSuit)
            {
                return false; // card2 is a trump and wins
            }

            // Define a numerical value for each rank
            // Check if both cards are trumps or neither is a trump
            else if (card1.suit() == card2.suit())
            {
                // Get the numerical values of the ranks
                int rankValue1 = Rank.values.ContainsKey(card1.CardRank) ? Rank.values[card1.CardRank] : 0;
                int rankValue2 = Rank.values.ContainsKey(card2.CardRank) ? Rank.values[card2.CardRank] : 0;
                // Compare the ranks
                return rankValue1 > rankValue2;
            }

            return false;
        }

        // --- FOR CLI ONLY -----------------------------------------------------------------------
        // Icon for the card (optional)
        public static string icon = @"
                                     ::::::                                 
                                       ::                                   
                                     ::::::                                 
            :::::                   ::::::::                     ::::       
            ::::::                 :::::-::::                   ::::::      
             :::::               :::::--==-::::            ::::::::-:       
               ::::::           :::-----====--::        -:------:           
               .:::::::        ::-------=======--     :---------            
                ::::::::::    :---------=========-  -----------:            
                :::::::::::::======================------------             
                :----------==========================----------             
                :--------==============================-------              
                 -------================================------              
                 -----====================================----              
                 ----======================================---              
                 --==========================================-              
                  -==========================================-              
                  -===========================================              
                  ===========================================-              
                  ============================================              
                   ==========================================               
                    ================== == ==================                
                     ================  ==  ================                 
                       -===========    ==    ===========-                   
                                      ====                                  
                                     ======                                 
                                    ========
        ";

        // Generates a demo card with random suit and rank
        public static Card DemoCard()
        {
            Random random = new Random();
            Card DemoCard = new Card(Suit.suits[random.Next(0, 3)], Rank.ranks[random.Next(0, 8)]);
            return DemoCard;
        }
    }
}
