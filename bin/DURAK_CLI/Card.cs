using System.Diagnostics;
using System;
using System.Collections;

// TODO Change class diagramm according to the classes

namespace DURAK_CLI
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

    public bool isTrump()
    {
      return Trump;
    }
    public string rank()
    {
      return $"{CardRank}";
    }
    public string suit()
    {
      return $"{CardSuit}";
    }
    public override string ToString()
    {
      return $"{CardSuit}_{CardRank}";
    }

    public static Card DemoCard()
    {
      Random random = new Random();
      Card DemoCard = new Card(Suit.suits[random.Next(0, 3)], Rank.ranks[random.Next(0, 8)]);
      return DemoCard;
    }

    public static bool CompareRanks(Card card1, Card card2)
    {
      // Check if one card is a trump and the other is not
      if (card1.suit() == Deck.getTrumpCard().suit() && card2.suit() != Deck.getTrumpCard().suit())
      {
        return true; // card1 is a trump and wins
      }
      else if (card1.suit() != Deck.getTrumpCard().suit() && card2.suit() == Deck.getTrumpCard().suit())
      {
        return false; // card2 is a trump and wins
      }              // Define a numerical value for each rank
                     // Check if both cards are trumps or neither is a trump
      else if (card1.suit() == card2.suit())
      {
        Dictionary<string, int> rankValues = new Dictionary<string, int>()
                {
                  { "Ace", 13 },
                  { "King", 12 },
                  { "Queen", 11 },
                  { "Jack", 10 },
                  { "10", 9 },
                  { "9", 8 },
                  { "8", 7 },
                  { "7", 6 },
                  { "6", 5 }
                  // Add more ranks as needed
                };
        // Get the numerical values of the ranks
        int rankValue1 = rankValues.ContainsKey(card1.CardRank) ? rankValues[card1.CardRank] : 0;
        int rankValue2 = rankValues.ContainsKey(card2.CardRank) ? rankValues[card2.CardRank] : 0;
        // Compare the ranks
        return rankValue1 > rankValue2;
      }
      else
      {
        return false;
      }
    }
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
  }


}