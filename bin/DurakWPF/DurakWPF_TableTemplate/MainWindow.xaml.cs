/**
 * Authors: Sheizah Jimenez, Hlib Marchenko, Raisa Nasara, Zhanibek Kapen
 * Date Updated: 04/14/2024
 * Description: Main application for Durak Game
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using DurakConsole;
using System.Windows.Documents;
using System.Net.NetworkInformation;

namespace DurakWPF_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class
        MainWindow : Window
    {
        // +--- PROJECT INITIALIZATION -----------------------------------------------------------------------------
        // game variables
        Deck deck = new Deck();
        GameTable table = new GameTable();
        Player player = new Player();
        AI ai = new AI();
        bool gameRunning = false;
        bool playerAttacking = true;
        bool playerPlayed = false;
        bool roundOngoing = false;
        private bool skipTurnClicked = false;
        int currentTurn = 1;
        int currentRound = 1;
        int currentGame = 1;
        int aiWins = 0;
        int playerWins = 0;

        // ui variables
        string cardStylePath;
        string trumpCardName;
        Dictionary<string, double> cardRows = new Dictionary<string, double>();
        Dictionary<string, double> rowCount = new Dictionary<string, double>();
        int tableRow = 0;
        int tableColumn = 0;

        // logging
        GameLog gameLog = new GameLog(true);
        Window logWindow;
        GameInfo durakGameInfo = new GameInfo();
        TextBox logTextBox = new TextBox();

        // main initialization
        public MainWindow()
        {
            InitializeComponent();
            ResetApplication();
        }

        // +--- PROGRAM FUNCTIONS ----------------------------------------------------------------------------------
        /**
         * Resets to Default.
         */
        private void ResetApplication()
        {
            // Reset GUI
            GameMenu.Visibility = Visibility.Visible;
            PlayerHand.Children.Clear();
            AIHand.Children.Clear();
            GameTable.Children.Clear();

            ChangeGameTheme(Themes.Blue, Themes.Tables["Blue"]);
            ChangeCardStyle(Styles.White);

            // Reset Game
            cardRows[PlayerHand.Name] = 0;
            rowCount[PlayerHand.Name] = 0;
            cardRows[AIHand.Name] = 0;
            rowCount[AIHand.Name] = 0;
            tableRow = 0;
            tableColumn = 0;

            gameRunning = false;
            playerAttacking = true;
            playerPlayed = false;
            roundOngoing = false;
            currentTurn = 1;
            currentRound = 1;

            deck.makeDeck();
            table.ClearTable();
            player.Clear();
            ai.Clear();

            // Set log
            CreateLog();
        }

        /**
         * Closes application
         */
        private void CloseApplication()
        {
            if (logWindow != null) logWindow.Close();
            Application.Current.Shutdown();
        }

        /**
         * Intializes the log menu for game logging
         */
        private void InitializeLogMenu()
        {
            // initialize game log window
            logWindow = new Window();
            logWindow.Name = "LogMenu";
            logWindow.Height = GameConfig.LogHeight;
            logWindow.Width = GameConfig.LogWidth;
            logWindow.ResizeMode = ResizeMode.NoResize;
            logWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // initialize game log txt block
            logTextBox.Name = "txtLog";
            logWindow.Content = logTextBox;
            logTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            logTextBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            logTextBox.Text = gameLog.Read();

            // read log, auto update log file
            FileSystemWatcher fileWatcher = new FileSystemWatcher(gameLog.GetPath());
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Filter = "*.txt";
            fileWatcher.EnableRaisingEvents = true;
            fileWatcher.Changed += new FileSystemEventHandler(UpdateLog);
        }

        /**
        * Update log menu
        */
        private void UpdateLog(object s, FileSystemEventArgs file)
        {
            this.Dispatcher.Invoke(() =>
            {
                string log = gameLog.Read();
                if (string.IsNullOrEmpty(log))
                    logTextBox.Text = "Empty File...";
                else
                    logTextBox.Text = log;
            });
        }

        /**
         * Creates a new log file
         */
        private void CreateLog()
        {
            gameLog = new GameLog();

            string logHeader = $"-----===Durak - Game Log | Current Game: {currentGame}===-----\n";

            gameLog.Write(logHeader);
        }

        /**
         * Changes the theme of the game
         */
        private void ChangeGameTheme(Dictionary<string, Color> theme, Uri table)
        {
            // set game theme settings
            GradientBack.Color = theme["Background"];
            GradientMid.Color = theme["Midground"];
            GradientFront.Color = theme["Foreground"];
            imgTable.Source = new BitmapImage(table);
        }

        /**
         * Resets the style of a specified collection
         */
        private void SetStyle(UIElementCollection collection)
        {
            foreach (Button card in collection)
            {
                Image cardImage = (Image)card.Content;
                string cardTitle = cardImage.Source.ToString().Split('/').Last();
                Uri newImage = new Uri(cardStylePath + "/" + cardTitle, UriKind.Relative);
                cardImage.Source = new BitmapImage(newImage);
            }
        }

        /**
         * Changes the card style of the game
         */
        private void ChangeCardStyle(string style)
        {
            // set game card style
            cardStylePath = style;

            // reset player hand and game table
            SetStyle(PlayerHand.Children);
            SetStyle(GameTable.Children);
        }

        /**
         * Updates the game's dynamic GUIs
         */
        private void UpdateInterface()
        {
            // check who is attacking
            if (playerAttacking)
            {
                lblAttack.Text = $"{GameConfig.AttackText}: {GameConfig.PlayerText}";
                lblDefend.Text = $"{GameConfig.DefendText}: {GameConfig.AIText}";
                lblAttack.Foreground = Brushes.LimeGreen;
                lblDefend.Foreground = Brushes.LimeGreen;
            }
            else
            {
                lblAttack.Text = $"{GameConfig.AttackText}: {GameConfig.AIText}";
                lblDefend.Text = $"{GameConfig.DefendText}: {GameConfig.PlayerText}";
                lblAttack.Foreground = Brushes.Red;
                lblDefend.Foreground = Brushes.Red;
            }

            // update current round
            lblRound.Text = $"{GameConfig.RoundText} {currentRound}";
        }

        // +--- CARD FUNCTIONS --------------------------------------------------------------------------------------
        /**
         * Creates a new card gui.
         */
        private Button CreateCard(string cardName = "", bool hidden = false, bool disabled = false)
        {
            // if hidden
            string path;
            if (hidden)
                path = GameConfig.CardBackPath;
            else
                path = cardStylePath + "/" + $"{cardName}" + ".png";

            // create the card
            Button card = new Button()
            {
                Name = cardName,
                Width = GameConfig.CardWidth,
                Height = GameConfig.CardHeight,
                Background = null,
                BorderBrush = null,
                Content = new Image
                {
                    Source = new BitmapImage(new Uri(path, UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = Stretch.Fill
                }
            };

            // set hidden
            if (hidden || disabled) card.IsEnabled = false;

            // card click function
            card.Click += btnCard_Click;

            return card;
        }

        /**
         * Updates hand if cards go over the hand row limit
         */
        private void CheckHandLimit(Grid hand)
        {
            // get rows and excess
            double handRows = cardRows[hand.Name];

            double currentRows = (double)Math.Floor((decimal)hand.Children.Count / GameConfig.CardRowLimit);
            int cardAmount = hand.Children.Count % GameConfig.CardRowLimit;
            rowCount[hand.Name] = cardAmount;

            //Console.WriteLine($"{hand.Name} | {cardRows[hand.Name]} : {rowCount[hand.Name]}");

            // ignore if row is the same
            if (currentRows == handRows) return;

            // move cards upwards
            if (currentRows > handRows)
            {
                foreach (Button card in hand.Children)
                {
                    Thickness cardMargin = card.Margin;
                    if (hand.Name == AIHand.Name) cardMargin.Top = card.Margin.Top + GameConfig.RowSpacing;
                    else cardMargin.Top = card.Margin.Top - GameConfig.RowSpacing;
                    card.Margin = cardMargin;

                    cardRows[hand.Name] = currentRows;
                    rowCount[hand.Name] = 0;
                }
            }

            // move cards downwards
            else if (currentRows < handRows) cardRows[hand.Name] = currentRows;
        }

        /**
         * Updates the player's hand to show what cards they can play
         */
        private void ShowPlayableCards()
        {
            // ignore if no cards were found in the hand
            if (player.Size() == 0) return;

            // show a green border around the playable cards
            List<Card> playableCards = player.GetPlayableCards(table, playerAttacking);
            foreach (Card card in playableCards)
            {
                string cardName = card.ToString();
                foreach(Button uiCard in PlayerHand.Children)
                {
                    if (uiCard.Name == cardName)
                    {
                        uiCard.BorderThickness = new Thickness(2);
                        uiCard.Background = Brushes.LimeGreen;
                        uiCard.BorderBrush = Brushes.LimeGreen;
                    }
                }
            }
        }

        /**
         * Resets player card display
         */
        private void ResetPlayerCards()
        {
            // ignore if no cards were found in the hand
            if (player.Size() == 0) return;

            foreach (Button uiCard in PlayerHand.Children)
            {
                uiCard.Background = Brushes.Transparent;
                uiCard.BorderBrush = Brushes.Transparent;
            }
        }

        /**
         * Overloaded function that deals a certain card to the hand
         */
        private void DealCard(Grid uiHand, string cardName = "", bool hidden = false, int amount = 1)
        {
            // check if hand is over the limit
            CheckHandLimit(uiHand);

            // deal cards
            // Changed logic so spacing is dynamic for each card
            double colSpacing = -uiHand.ActualWidth + rowCount[uiHand.Name] * GameConfig.CardSpacing + GameConfig.CardWidth;
            double rowSpacing = uiHand.ActualHeight / 3;

            if (uiHand.Name == AIHand.Name) rowSpacing *= -1;

            for (int i = 0; i < amount; i++)
            {
                Button card = CreateCard(cardName, hidden);
                card.Margin = new Thickness(colSpacing, rowSpacing, 0, 0);
                colSpacing += GameConfig.CardSpacing;                

                uiHand.Children.Add(card);
                lblDeckSize.Text = deck.Size().ToString();
            }
        }

        /**
         * Overloaded function that takes cards drawn instead of card's name
         */
        private void DealCard(Grid uiHand, Hand hand, List<Card> cardsDrawn, bool hidden = false)
        {
            // check if hand is over the limit
            CheckHandLimit(uiHand);

            // getting data
            int amount = cardsDrawn.Count;

            // deal cards
            // Changed logic so spacing is dynamic for each card
            double colSpacing = -uiHand.ActualWidth + rowCount[uiHand.Name] * GameConfig.CardSpacing + GameConfig.CardWidth;
            double rowSpacing = uiHand.ActualHeight/3;

            if (uiHand.Name == AIHand.Name) rowSpacing *= -1;

            for (int i = 0; i < amount; i++)
            {
                string cardName = cardsDrawn[i].ToString();
                Button card = CreateCard(cardName, hidden);
                card.Margin = new Thickness(colSpacing, rowSpacing, 0, 0);
                colSpacing += GameConfig.CardSpacing;

                uiHand.Children.Add(card);
                lblDeckSize.Text = deck.Size().ToString();
            }
        }

        /**
         * Fills gaps in the given hand
         */
        private void FillHandGaps(Grid uiHand)
        {
            // get data
            List<Button> cardsInHand = new List<Button>();
            foreach (Button handCard in uiHand.Children)
            {
                cardsInHand.Add(handCard);
            }

            // clear hand
            uiHand.Children.Clear();

            // readd cards
            foreach (Button handCard in cardsInHand)
            {
                if (handCard.IsEnabled) 
                    DealCard(uiHand, handCard.Name);
                else 
                    DealCard(uiHand, handCard.Name, true);
            }
        }

        /**
         * Gives minimum cards to the hand
         */
        private void FillHand(Grid uiHand, Hand hand, bool hidden = false)
        {
            int cardsInHand = uiHand.Children.OfType<Button>().Count();

            if (cardsInHand < GameConfig.MinimumCards)
            {
                int cardsNeeded = GameConfig.MinimumCards - cardsInHand;

                // If deck has less cards than a player needs, then draw all cards that deck has to the player's hand
                // for example, deck has only 3 cards left, but player needs 6 cards. It will only give last 3 cards to the player
                if (deck.Size() < cardsNeeded)
                {
                    List<Card> cardsDrawn = hand.DrawCards(deck, deck.Size());
                    DealCard(uiHand, hand, cardsDrawn, hidden);
                }
                else
                {
                    List<Card> cardsDrawn = hand.DrawCards(deck, cardsNeeded);
                    DealCard(uiHand, hand, cardsDrawn, hidden);
                }

            }
        }

        /**
         * Takes the cards from the table and puts it into the hand
         */
        private void TakeCards(Grid uiHand, Hand hand, bool hidden = false)
        {
            // add cards to hand
            List<Card> takenCards = hand.TakeAllCards(table);
            DealCard(uiHand, hand, takenCards, hidden);
            GameTable.Children.Clear();

            // Reset Table
            tableRow = 0;
            tableColumn = 0;
        }

        /**
         * Clears the table of all cards (discard them)
         */
        private void ClearTable()
        {
            table.ClearTable();
            GameTable.Children.Clear();

            // Reset Table
            tableRow = 0;
            tableColumn = 0;
        }

        /**
         * Delivers the card to the table
         */
        private void PlayCard(Grid uiHand, Hand hand, Button card)
        {
            // reset table
            bool tableReset = tableColumn == 0 && tableRow == 0 && GameTable.Children.Count % 2 == 0;
            if (tableReset) GameTable.Children.Clear();

            // remove from hand
            uiHand.Children.Remove(card);
            hand.PlayCard(table, hand.GetCard(card.Name));

            // add to table
            Button tableCard = CreateCard(card.Name, false, true);
            GameTable.Children.Add(tableCard);

            tableCard.Margin = new Thickness(10);
            tableCard.SetValue(Grid.RowProperty, tableRow);
            tableCard.SetValue(Grid.ColumnProperty, tableColumn);

            // move cards over
            FillHandGaps(uiHand);

            // card table alignment
            if (GameTable.Children.Count % 2 == 0)
                tableCard.HorizontalAlignment = HorizontalAlignment.Right;
            else
            {
                tableCard.HorizontalAlignment = HorizontalAlignment.Left;
                return;
            }

            // row/column logic
            if (tableColumn < GameConfig.TableLength - 1)
                tableColumn++;
            else
            {
                tableColumn = 0;
                tableRow++;
            }

            if (tableRow > GameConfig.TableWidth - 1)
            {
                tableColumn = 0;
                tableRow = 0;
            }
        }

        /**
         * Displays Trump Card 
         */
        private void DisplayTrumpCard(string cardName)
        {
            BitmapImage trumpCardSource = new BitmapImage(new Uri($"{cardStylePath}/{cardName}.png", UriKind.Relative));

            imgTrumpCard.Source = trumpCardSource;
        }

        /**
         * Initializing the start of the game
         */
        private void GameStart()
        {
            // Initiate deck 
            deck = new Deck();
            deck.makeDeck();
            Deck.shuffleDeck();

            // Set Trump Card
            Deck.setTrumpCard(deck);
            trumpCardName = Deck.getTrumpCard().ToString();
            DisplayTrumpCard(trumpCardName);

            // Deal cards to both hands
            List<Card> playerCardsDrawn = player.DrawCards(deck, GameConfig.MinimumCards);
            DealCard(PlayerHand, player, playerCardsDrawn);
            List<Card> aiCardsDrawn = ai.DrawCards(deck, GameConfig.MinimumCards);
            DealCard(AIHand, ai, aiCardsDrawn, true);

            // set variables
            gameRunning = true;
            playerAttacking = true;
        }

        /**
         * Checks for a winner
         */
        private bool WinnerFound()
        {
            bool winnerFound = false;
            this.Dispatcher.Invoke(() =>
            {
                // ignore if deck still has cards
                if (deck.Size() > 0) return;

                // if winner was found
                if (player.Size() == 0 && ai.Size() == 0)
                {
                    lblGameTitle.Content = "TIE GAME!";
                    gameLog.Write($"GAME WINNER: Tie Game\n");

                    aiWins++;
                    playerWins++;
                    winnerFound = true;
                }
                else if (player.Size() == 0)
                {
                    lblGameTitle.Content = "PLAYER WINS!";
                    gameLog.Write($"GAME WINNER: Player\n");

                    playerWins++;
                    winnerFound = true;

                }
                else if (ai.Size() == 0)
                {
                    lblGameTitle.Content = "AI WINS!";
                    gameLog.Write($"GAME WINNER: AI\n");

                    aiWins++;
                    winnerFound = true;
                }

                if (winnerFound)
                {
                    btnGameStart.Content = "Play Again";

                    // update score board
                    lblAIWins.Text = aiWins.ToString();
                    lblPlayerWins.Text = playerWins.ToString();

                    gameLog.Write($"___Current Score Board___" +
                        $"AI Wins: {aiWins}" +
                        $"Player Wins: {playerWins}\n\n");

                    // stop game
                    gameRunning = false;
                    ResetApplication();
                }
            });
            return winnerFound;
        }

        /**
         * The main logic of the game
         */
        private async Task GameRound()
        {
            currentTurn = 1;

            do {
                gameLog.Write($"___Round {currentRound} | Turn {currentTurn}___\n" +
                    $"Player Hand: {player}\n" +
                    $"AI Hand: {ai}\n"
                );

                // Add to current turn
                currentTurn++;
                roundOngoing = true;

                if (playerAttacking)
                {
                    // Player Attacking Logic
                    lblTurn.Text = "YOUR TURN";
                    ShowPlayableCards();

                    // Wait until player either attacks, or skips his turn
                    while (!playerPlayed && !skipTurnClicked) { await Task.Delay(100); } 
                    if (skipTurnClicked || WinnerFound()) { roundOngoing = false; goto EndRound; }

                    
                    // AI Defending Logic
                    lblTurn.Text = "AI'S TURN";

                    await Task.Delay(1000);
                    this.Dispatcher.Invoke(() =>
                    {
                        Card cardPlayed = ai.Defend(table);

                        // make ai take cards
                        if (cardPlayed == null)
                        { 
                            TakeCards(AIHand, ai, true);
                            roundOngoing = false;
                            gameLog.Write("AI has skipped...");
                        }

                        // if ai plays card, continue
                        else
                        {
                            string cardName = cardPlayed.ToString();
                            Button uiCard = null;

                            foreach(Button card in AIHand.Children)
                                if (card.Name == cardName) { uiCard = card; } 

                            PlayCard(AIHand, ai, uiCard);

                            gameLog.Write($"AI Defence: {cardName}\n");
                        }
                    });
                    if (!roundOngoing || WinnerFound()) { roundOngoing = false; goto EndRound; }
                }
                else
                {
                    // AI Attacking Logic
                    lblTurn.Text = "AI'S TURN";

                    await Task.Delay(1000);
                    this.Dispatcher.Invoke(() =>
                    {
                        Card cardPlayed = ai.Attack(table);

                        // make ai take cards
                        if (cardPlayed == null)
                        {
                            ClearTable();
                            roundOngoing = false;
                            gameLog.Write($"AI has skipped...");
                        }

                        // if ai plays card, continue
                        else
                        {
                            string cardName = cardPlayed.ToString();
                            Button uiCard = null;

                            foreach (Button card in AIHand.Children)
                                if (card.Name == cardName) { uiCard = card; }

                            PlayCard(AIHand, ai, uiCard);

                            gameLog.Write($"AI Attack: {cardName}");
                        }
                    });
                    if (!roundOngoing || WinnerFound()) { roundOngoing = false; goto EndRound; }


                    // Player Defending Logic
                    lblTurn.Text = "YOUR TURN";
                    ShowPlayableCards();

                    // Wait until player either attacks, or skips his turn
                    while (!playerPlayed && !skipTurnClicked) { await Task.Delay(100); }
                    if (skipTurnClicked || WinnerFound()) { roundOngoing = false; goto EndRound; }
                }
                // Max cards on table w/ no winner
                if (currentTurn > GameConfig.MaxTurns) { roundOngoing = false; goto EndRound; }

            // End round
            EndRound:
                if (!roundOngoing)
                {
                    await Task.Delay(500);
                    GameTable.Children.Clear();
                    skipTurnClicked = false;
                }
                ResetPlayerCards();
                playerPlayed = false;
            } while (roundOngoing && gameRunning);
        }


        // +--- CONTROL EVENTS -------------------------------------------------------------------------------------
        /**
         * Exit program
        */
        private void btnGameExit_Click(object sender, RoutedEventArgs e)
        {
            CloseApplication();
        }

        /**
         * Resets the application
         */
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetApplication();
        }

        // --- GAME LOGIC ----------------------------
        /** 
         * Starts/Initializes the game.
        */
        private async void btnGameStart_Click(object sender, RoutedEventArgs e)
        {
            // Initialize GUI
            GameArea.Visibility = Visibility.Visible;
            GameMenu.Visibility = Visibility.Collapsed;

            // Initialize Game
            GameStart();
            gameLog.Write($"Trump Card: {trumpCardName}\n");

            while (gameRunning)
            {
                UpdateInterface();
                gameLog.Write(
                    $"_______________Round {currentRound}_______________\n" +
                    $"Attacking: {(playerAttacking ? "Player" : "AI")}\n" +
                    $"Defending: {(playerAttacking ? "AI" : "Player")}\n"
                );

                await GameRound();

                // The player has attacked gets the cards first
                if (playerAttacking) {
                    FillHand(PlayerHand, player);
                    FillHand(AIHand, ai, true);
                }
                else
                {
                    FillHand(AIHand, ai, true);
                    FillHand(PlayerHand, player);
                }

                // max cards played on table
                currentRound++;
                playerAttacking = !playerAttacking; // switches who is attacking
            }
        }


        /**
         * Routed handler for when a card is clicked.
         */
        private void btnCard_Click(object sender, RoutedEventArgs e)
        {
            Button cardUI = (Button)sender;
            string cardName = cardUI.Name;

            // ignore if player has already played a card. await ai response.
            if (playerPlayed) return;

            Card card = player.GetCard(cardName);
            Console.WriteLine($"Player: {cardName} : {card}");

            // check if card is able to be played
            if (playerAttacking)
            {
                // if player attacking
                // ignore if cannot attack
                if (!table.CanAttack(card)) return;

                // play card
                PlayCard(PlayerHand, player, cardUI);
                playerPlayed = true;

                gameLog.Write($"Player Attack: {cardName}");
            }
            else
            {
                // if player defending
                // ignore if cannot defend
                if (!table.CanDefend(card)) return;

                // play card
                PlayCard(PlayerHand, player, cardUI);
                playerPlayed = true;

                gameLog.Write($"Player Defence: {cardName}\n");
            }
            
        }

        /**
         * Allows the player to skip their turn and pick up all cards from the table.
         */
        private void btnSkipTurn_Click(object sender, RoutedEventArgs e)
        {
            // ignore if game is not running
            if (!gameRunning) return;

            // skip turn
            roundOngoing = false;
            skipTurnClicked = true;

            gameLog.Write($"Player has skipped...\n");

            // if attacking, clear the table
            if (playerAttacking) ClearTable();

            // if defending, take cards
            else TakeCards(PlayerHand, player);
        }

        /**
         * When button is pressed, deals cards to playersas
         */
        private void dealButton_Click(object sender, RoutedEventArgs e)
        {
            // ignore if game is not running
            if (!gameRunning) return;

            // Card for PlayerHand
            List<Card> playerCardsDrawn = player.DrawCards(deck);
            DealCard(PlayerHand, player, playerCardsDrawn);

            // Card for AIHand
            List<Card> aiCardsDrawn = ai.DrawCards(deck);
            DealCard(AIHand, ai, aiCardsDrawn, true);
            /*DealCard(AIHand, "", 1, true);*/
        }

        /**
         * Add a card to the table
         */
        private void cardToTable_Click(object sender, RoutedEventArgs e)
        {
            // ignore if game is not running
            if (!gameRunning) return;

            // create cards
            string plrPath = cardStylePath + "/Hearts_King.png";
            string aiPath = cardStylePath + "/Clovers_6.png";

            Button playerCardImage = CreateCard(plrPath);
            Button aiCard = CreateCard(aiPath);

            PlayCard(PlayerHand, player, playerCardImage);
            PlayCard(AIHand, ai, aiCard);
        }

        // --- TOOLBAR PROGRAM EVENTS ----------------

        // -- HELP INFO ---
        /**
         * Describe what the game is
         */
        private void GameInfo_Click(object sender, RoutedEventArgs e)
        {
            GameInfo durakGameInfo = new GameInfo();
            durakGameInfo.ShowDialog();
        }

        /** 
         * Describe the game rules
         */
        private void GameRules_Click(object sender, RoutedEventArgs e)
        {
            GameRules durakGameRules = new GameRules();
            durakGameRules.ShowDialog();
        }


        // ++ Game Theme (Background)
        /**
         * Sets theme to BLUE.
         */
        private void ThemeBlue_Click(object sender, RoutedEventArgs e)
        {
            ChangeGameTheme(Themes.Blue, Themes.Tables["Blue"]);
        }

        /**
         * Sets theme to RED.
         */
        private void ThemeRed_Click(object sender, RoutedEventArgs e)
        {
            ChangeGameTheme(Themes.Red, Themes.Tables["Red"]);
        }

        /**
         * Sets theme to GREEN.
         */
        private void ThemeGreen_Click(object sender, RoutedEventArgs e)
        {
            ChangeGameTheme(Themes.Green, Themes.Tables["Green"]);
        }

        // ++ Game Style (Cards)
        /**
         * Sets card style to WHITE
         */
        private void StyleWhite_Click(object sender, RoutedEventArgs e)
        {
            ChangeCardStyle(Styles.White);
        }

        /**
         * Sets card style to BLACK
         */
        private void StyleBlack_Click(object sender, RoutedEventArgs e)
        {
            ChangeCardStyle(Styles.Black);
        }

        /**
         * View game logs
         */
        private void LogMenuOption_Click(object sender, RoutedEventArgs e)
        {
            // ignore if log menu is already open
            if (logWindow != null && logWindow.IsActive) return;

            // open log menu
            InitializeLogMenu();
            logWindow.Show();
        }

        /**
         * Closes all windows when main window exits
         */
        private void Window_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (logWindow != null) logWindow.Close();
            durakGameInfo.Close();
        }
    }
}
