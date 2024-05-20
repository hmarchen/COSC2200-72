using System;
using System.Collections;
namespace DURAK_CLI
{
  class Program
  {
    // Initiating the start of the game




    // FUNCTIONS
    public static void GamePlay()
    {
      Deck deck = new Deck();
      Hand playerHand = new Hand();
      Hand AiHand = new Hand();
      GameTable.GameReset();

      // instantienting game esential objects
      Player player = new Player();
      AI ai = new AI();

      GameTable table = new GameTable();

      // Creates 36 card deck
      deck.makeDeck();

      // Shuffles deck
      Deck.shuffleDeck();

      // Set Trump Card
      Deck.setTrumpCard(deck);
      Console.WriteLine("\n===============================");
      Console.WriteLine(Deck.getTrumpCard().suit());

      // Draws 6 cards as first hand and remove from main deck
      playerHand.drawHand(deck);
      AiHand.drawHand(deck);

      Console.Clear();

      Console.WriteLine("Remaining cards on Deck: " + deck.length());
      Deck.DisplayDeck();
      Console.WriteLine("\n========================================");

      Console.WriteLine("Trump card is: " + Deck.getTrumpCard());
      Console.WriteLine("\n========================================");
      Console.WriteLine("Player Hand Count: " + playerHand.length());
      playerHand.DisplayHand();
      Console.WriteLine("\n========================================");
      Console.WriteLine("AI Hand Count: " + AiHand.length());
      AiHand.DisplayHand();

      Console.WriteLine("\n========================================");
      Console.WriteLine("\n Attack started: Player1");
      Console.WriteLine("\n========================================");
      Console.WriteLine("\n+=> Select the card from Player1 hand <=+");

      // used in the function to track the card that needs to defended
      int turn = 0;
      // loops until one hand is empty {win condition}
      while (playerHand.length() != 0 && AiHand.length() != 0)
      {
        Console.WriteLine("\n=============AI_Hand====================");
        AiHand.DisplayHand();
        Console.WriteLine("\n=============PLayer_Hand================");
        playerHand.DisplayHand();
        int cardIndex;
        // reads input from user and validates it
        while (!int.TryParse(Console.ReadLine(), out cardIndex) || cardIndex <= 0 || cardIndex > playerHand.length())
        {
          Console.WriteLine("Invalid input. Please enter a valid card index.");
          playerHand.DisplayHand();
        }

        if (cardIndex == 90)
        {
          Console.WriteLine("Player stopped attacking!");

        }
        else
        {
          // changes human readable index to array readable
          cardIndex = cardIndex - 1;
          // player's attack function {chosen card, table, player's hand}
          player.Attack(playerHand.UnderIndex(cardIndex), table, playerHand);
          Console.WriteLine("\n========================================");
          Console.WriteLine("The Game Table\n");
          table.DisplayTable();
          Console.WriteLine("\n===========AI_MOVES=====================");
          // Ai's defend function {Ai's hand, table, index of card that ai is trying to defend}
          ai.Defend(AiHand, table, turn);
          turn = turn + 2;
          // ai.Defend(AiHand.UnderIndex(cardIndex), table);
          Console.WriteLine("\n========================================");
          Console.WriteLine("The Game Table\n");
          table.DisplayTable();
          Console.WriteLine("\n==Trump SUIT:" + Deck.getTrumpCard().suit() + "==");
        }

      }
      Console.WriteLine("player won!!!");

    }


    static void Main(string[] args)
    {
      string menuChoice;
      string exitChoice = "";


      Console.Clear();
      do
      {
        // Display initial menu
        Console.WriteLine(Card.icon);
        Console.WriteLine("\n========================================");
        Console.WriteLine("1 - Card.");
        Console.WriteLine("2 - List Deck.");
        Console.WriteLine("3 - Pull the Card from the Deck.");
        Console.WriteLine("4 - Deck shuffle.");
        Console.WriteLine("5 - Trump Card.");
        Console.WriteLine("6 - GamePlay.");
        Console.WriteLine("7 - GameRiver<>.");
        Console.WriteLine("8 - AIHand.");
        Console.WriteLine("9 - PlayerHand.");
        Console.WriteLine("11 - Exit Program.");
        Console.WriteLine("========================================");

        // Get user choice
        Console.Write("\nPlease enter your choice: ");
        menuChoice = Console.ReadLine();

        switch (menuChoice)
        {
          case "1":
            Console.WriteLine(Card.DemoCard());
            Console.ReadLine();
            break;

          case "2":
            Deck.DisplayDeck();
            Console.ReadLine();
            break;

          case "3":
            Console.WriteLine(Deck.Draw());
            Console.ReadLine();
            break;

          case "4":
            Deck.shuffleDeck();
            Console.ReadLine();
            break;

          // case "5":
          //     Deck.setTrumpCard(deck);
          //     Console.WriteLine("The trump card is " + Deck.getTrumpCard());
          //     Console.ReadLine();
          //     break;

          case "6":
            GamePlay();
            Console.ReadLine();
            break;

          // case "7":                           
          //     GameRiver();
          //     break;

          // case "8":
          //     AiHand.DisplayHand();
          //     Console.ReadLine();
          //     break;

          // case "9":
          //     playerHand.DisplayHand();
          //     Console.ReadLine();
          //     break;

          case "11":
            exitChoice = "exit";
            break;

          default: // If user choice is not one of the menu options,
            menuChoice = "retry"; // set menu choice to "retry"
            break;
        }

      } while (menuChoice == "retry" | exitChoice != "exit");
    }
  }
}