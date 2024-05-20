using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DurakWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // +--- PROJECT INITIALIZATION -----------------------------------------------------------------------------


        public MainWindow()
        {
            InitializeComponent();
        }


        // +--- PROGRAM FUNCTIONS ----------------------------------------------------------------------------------
        /**
         * Set form back to its original state (when starting application).
         */
        private void ResetForm()
        {
            // Set card found label to empty
            lblCardFound.Content = string.Empty;
            txtCard.Text = string.Empty;
            cardList.Items.Clear();
        }

        // +--- BUTTON CLICK EVENTS / FUNCTIONS --------------------------------------------------------------------
        /**
         * Click function that takes user input and finds the card in the deck.
         * Showcases a label that says if it was found or not.
         */
        private void ButtonGetCard_Click(object sender, RoutedEventArgs e)
        {


            // Locate card in deck
            //bool cardFound = FindCard(inputCard);
            try
            {
                // Get Card Name Input
                string inputCard = txtCard.Text.Trim();

                // Find a file picture of a card
                string cardName = inputCard.Replace(" ", "_") + ".png";
                //string imagePath = $"C:/Users/zkape/OneDrive/Desktop/durakOOP/COSC2200-72/COSC2200-72/Assets/playing_cards/PNG/white/{cardName}";
                string imagePath = $"../../../DurakWPF_TableTemplate/Assets/playing_cards/PNG/white/{cardName}";


                if (!File.Exists(imagePath))
                {
                    MessageBox.Show(cardName, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Create a BitmapImage from the specified imagePath
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();

                // Create an Image control to display the image
                Image imageControl = new Image();
                imageControl.Source = bitmap;

                // Create a ListBoxItem to hold the image
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = imageControl;

                imageControl.Height = 150; 
                imageControl.Width = 150; 

                // Add the ListBoxItem to the ListBox
                cardList.Items.Add(newItem);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while displaying the card image: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /**
         * Click function that gets all the cards in the deck and prints them
         * into the ListBox.
         */
        private void ButtonGetAllCards_Click(object sender, RoutedEventArgs e)
        {
            // Clear form
            ResetForm();

            lblCardFound.Content = "Get All Clicked!";
        }
            
        /**
         * Click function that resets form back to original state.
         */
        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Reset Form!");
            ResetForm();
        }
    }
}
