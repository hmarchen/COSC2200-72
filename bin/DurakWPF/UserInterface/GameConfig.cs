using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceProperties
{
    /**
     * The game's configuration settings.
     */
    internal class GameConfig
    {
        // constants
        public const int MinimumCards = 6;
        public const int CardHeight = 100;
        public const int CardWidth = 70;
        public const int CardSpacing = 150;
        public const int CardRowLimit = 10;
        public const int TableLength = 5;
        public const int TableWidth = 2;

        // assets
        public const string CardBackPath = "../../Assets/Card Asset/Backgrounds/u_background.png";
    }
}
