/**
 * Authors: Sheizah Jimenez, Hlib Marchenko, Raisa Nasara, Zhanibek Kapen
 * Date Updated: 04/14/2024
 * Description: Handles logging of game events and information.
 */
using System;
using System.IO;
using System.Text;

namespace DurakWPF_Game
{
    /**
     * Class to log game logic.
     */
    internal class GameLog
    {
        private static string GameLogFolder = new Uri("../../GameLog/", UriKind.Relative).ToString();
        private static string FileType = ".txt";

        private string logFile = "";

        /**
         * Initializes a new instance of the GameLog class.
         */
        public GameLog(bool delete = false)
        {
            // log name
            string now = DateTime.Today.ToString("ddMMyyyy");
            string fileName = $"Durak-Game-Log-{now}";
            this.logFile = GameLogFolder + fileName + FileType;

            // check if file already exists
            if (File.Exists(logFile))
            {
                // delete any previous log files on this path
                if (delete) File.Delete(logFile);

                // continue logging on file
                else
                {
                    this.logFile = GameLogFolder + fileName + FileType;
                    return;
                }
            }

            // create new file
            File.Create(logFile);
        }

        /**
         * Writes into the game log.
         */
        public void Write(string log)
        {
            string newLog = this.Read() + log;
            FileStream file = File.OpenWrite(logFile);
            byte[] byteLog = new UTF8Encoding(true).GetBytes(newLog);
            file.Write(byteLog, 0, byteLog.Length);
            file.Close();
        }

        /**
         * Reads the current game log.
         */
        public string Read()
        {
            string log = "";
            using (FileStream file = File.OpenRead(logFile))
            {
                byte[] b = new byte[4096];
                UTF8Encoding encoder = new UTF8Encoding(true);
                int readLen;
                while ((readLen = file.Read(b, 0, b.Length)) > 0)
                {
                    log += encoder.GetString(b, 0, readLen) + "\n";
                }

                file.Close();
            }

            return log;
        }

        /**
         * Returns the path of the log folder.
         */
        public string GetPath()
        {
            return GameLogFolder;
        }

        /**
         * Returns the path of the log file.
         */
        public string GetFile()
        {
            return logFile;
        }
    }
}
