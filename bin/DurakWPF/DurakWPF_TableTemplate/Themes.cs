using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace DurakWPF_Game
{
    /**
     * Themes within the game (Tool Bar Settings)
     */
    internal class Themes
    {
        // Game Themes (Background)
        const string TablePath = "/Assets/Card Asset/Tables";

        public static Dictionary<string, Color> Blue = new Dictionary<string, Color>() { 
            { "Background", (Color)ColorConverter.ConvertFromString("#FF4F8AB8") },
            { "Midground", (Color)ColorConverter.ConvertFromString("#FF18639E") },
            { "Foreground", (Color)ColorConverter.ConvertFromString("#FF2C76B1") }
        };
        
        public static Dictionary<string, Color> Red = new Dictionary<string, Color>() {
            { "Background", (Color)ColorConverter.ConvertFromString("#FFB84F4F") },
            { "Midground", (Color)ColorConverter.ConvertFromString("#FF9E1818") },
            { "Foreground", (Color)ColorConverter.ConvertFromString("#FFB12C2C") },
        };

        public static Dictionary<string, Color> Green = new Dictionary<string, Color>() {
            { "Background", (Color)ColorConverter.ConvertFromString("#FF50B84F") },
            { "Midground", (Color)ColorConverter.ConvertFromString("#FF139211") },
            { "Foreground", (Color)ColorConverter.ConvertFromString("#FF21A61F") },
        };

        public static Dictionary<string, Uri> Tables = new Dictionary<string, Uri>()
        {
            { "Blue", new Uri(TablePath + "/table_blue.png", UriKind.Relative) },
            { "Red", new Uri(TablePath + "/table_red.png", UriKind.Relative) },
            { "Green", new Uri(TablePath + "/table_green.png", UriKind.Relative) },
        };    
    }
}
