using System.Linq;

namespace DURAK_CLI
{
    public class GameTable
    {
        public static List<Card> Table = new List<Card>();

        public void addToTable(Card card)
        {
            Table.Add(card);
        }
        public void cleanTable()
        {
            Table.Clear();
        }

        public void DisplayTable()
        {
            foreach (Card card in Table)
            {

                Console.WriteLine(card.ToString());
            }
        }

        public static void TableClean()
        {
            Table.Clear();
        }

        public static void GameReset()
        {
            Table.Clear();
            Deck.mainDeck.Clear();
        }

        public int Count()
        {
            return Table.Count();
        }

        public Card UnderIndex(int index)
        {
            return Table[index];
        }
        // will it be better to remove before adding or add befor remove or no diff?
        public void TakeFromTable(Card card, Hand hand)
        {
            hand.AddCard(card);
            Table.Remove(card);
        }

        public List<Card> cardS()
        {
            return Table;
        }
    }
}

