namespace DURAK_CLI
{
    public class Deck
    {
        // Deck class made for storing cards
        public static List<Card> mainDeck = new List<Card>();
        private static Card trumpCard;

        // Creates 52 cards and stores them in main deck
        public virtual void makeDeck()
        {
            mainDeck.Clear();
            foreach (var suit in Suit.suits)
            {
                foreach (var rank in Rank.ranks)
                {
                    Card card = new Card(suit, rank);
                    mainDeck.Add(card);
                }
            }
        }
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
            // foreach (var card in mainDeck)
            // {
            //     Console.WriteLine(card.ToString());
            // }
        }
        public static void DisplayDeck()
        {
            foreach (var card in mainDeck)
            {
                Console.WriteLine(card.ToString());
            }
        }

        public static Card Draw()
        {
            Card poppedCard = mainDeck[0]; // Retrieve the first card
            mainDeck.RemoveAt(0);
            return poppedCard;
        }

        /**
        * Draw N number of cards and remove from mainDeck
        */
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

        /**
        * Draw the top card from the deck and set it as the trump card
        */
        public static void setTrumpCard(Deck mainDeck)
        {
            Card trumpCard;
            trumpCard = Deck.Draw();

            Deck.trumpCard = trumpCard;
        }

        /**
         * Gets the trump card
         */
        public static Card getTrumpCard()
        {
            return trumpCard;
        }

        public int length()
        {
            return mainDeck.Count();
        }
    }
}