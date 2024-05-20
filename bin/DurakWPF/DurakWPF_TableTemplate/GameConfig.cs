/**
 * Authors: Sheizah Jimenez, Hlib Marchenko, Raisa Nasara, Zhanibek Kapen
 * Date Updated: 04/14/2024
 * Description: Configuration settings for the GUI.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakWPF_Game
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
        public const int RowSpacing = 50;
        public const int CardRowLimit = 10;
        public const int TableLength = 3;
        public const int TableWidth = 2;
        public const int MaxTurns = 6;

        public const double LogHeight = 500;
        public const double LogWidth = 500;

        public const string AttackText = "ATTACKING";
        public const string DefendText = "DEFENDING";
        public const string RoundText = "ROUND";
        public const string PlayerText = "PLAYER";
        public const string AIText = "AI";

        // assets
        public const string CardBackPath = "../../Assets/Card Asset/Backgrounds/u_background.png";
    }
}
