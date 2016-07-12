using System;

namespace XonixGame.Monogame
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        #region Private Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (var game = new XonixGame())
            {
                game.Run();
            }
        }

        #endregion Private Methods
    }
}