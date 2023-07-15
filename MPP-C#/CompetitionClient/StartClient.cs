using CompetitionClient.forms;
using CompetitionNetworking.networking.protocol;
using CompetitionServices.services;

namespace CompetitionClient
{
    internal static class StartClient
    {
        private static int defaultCompetitionPort = 55555;
        private static String defaultServer = "localhost";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            string serverIP = System.Configuration.ConfigurationSettings.AppSettings["competition.server.host"];
            int serverPort = defaultCompetitionPort;
            try
            {
                serverPort = Int32.Parse(System.Configuration.ConfigurationSettings.AppSettings["competition.server.port"]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong Port Number" + e.Message);
                Console.WriteLine("Using default port " + defaultCompetitionPort);
            }
            Console.WriteLine("Using server IP " + serverIP);
            Console.WriteLine("Using server port " + serverPort);

            ICompetitionServices server = new CompetitionObjectProxy("127.0.0.1", serverPort);
            ApplicationConfiguration.Initialize();
            MainMenuForm mainMenuForm = new MainMenuForm();
            Application.Run(new LogInForm(server,mainMenuForm));
        }
    } 
}