using System;
using System.Collections;
namespace DURAK_CLI
{
  public class AI : Player
  {

    public void Defend(Hand AiHand, GameTable table, int turn)
    {
      Card TheMove = null;
      List<Card> SuperCards = new List<Card>();
      // Loops through Ai's hand to find suitable cards for defense
      foreach (Card card in AiHand.cardS())
      {
        // Sup also Superior card is the card that can beat the opponents card
        bool isSup = Card.CompareRanks(card, table.UnderIndex(turn));
        if (isSup)
        {
          // adds to the list of superior cards
          SuperCards.Add(card);
        }
      }
      // if no suitable cards were found, scoop all cads on the table
      if (SuperCards.Count == 0)
      {
        int i = 0;
        while (i < table.Count())
        {
          table.TakeFromTable(table.UnderIndex(0), AiHand);
        }
      }
      else
      {
        // initiates random object
        Random random = new Random();
        // determines the card that will be the next Ai's move
        TheMove = SuperCards[random.Next(0, SuperCards.Count == 1 ? SuperCards.Count : SuperCards.Count - 1)];
        AiHand.RemoveCard(TheMove);
        table.addToTable(TheMove);
      }
    }
    bool defended;
    // !!! TODO not finnished just example!

    public void Attack(Hand AiHand, GameTable table, int turn)
    {
      // initiates random object
      Random random = new Random();
      if (table.Count() == 0)
      {
        table.addToTable(AiHand.UnderIndex(random.Next(0, AiHand.length())));
      }
      else
      {
        Card TheMove = null;
        List<Card> SuperCards = new List<Card>();
        // Loops through Ai's hand to find suitable cards for defense
        foreach (Card card in AiHand.cardS())
        {
          foreach (Card card1 in table.cardS())
          {
            if (card.rank() == card1.rank())
            {
              SuperCards.Add(card);
            }
            foreach (Card card2 in SuperCards)
            {

              Console.WriteLine(card2.ToString());
            }
          }
          // Sup also Superior card is the card that can beat the opponents card
          // bool isSup = Card.CompareRanks(card, table.UnderIndex(turn));
          // if (isSup)
          // {
          //   // adds to the list of superior cards
          //   SuperCards.Add(card);
          // }
        }
      }


    }


  }
}