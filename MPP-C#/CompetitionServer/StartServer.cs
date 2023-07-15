using CompetitionNetworking.networking.protocol;
using CompetitionNetworking.networking.protobuffprotocol;
using CompetitionNetworking.networking.utils;
using CompetitionPersistence.repos;
using CompetitionServer.server;
using CompetitionServices.services;
using System.Configuration;
using System.Net.Sockets;

namespace CompetitionServer
{
    public class StartServer
    {
        private static int defaultPort = 55556;

        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }

        public static void Main(String[] args)
        {
            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", GetConnectionStringByName("Competition"));
            
            UserDBRepo userDBRepo = new UserDBRepo(props);
            ParticipantDBRepo participantDBRepo = new ParticipantDBRepo(props);
            RoundDBRepo roundDBRepo = new RoundDBRepo(props);
            ScoreDBRepo scoreDBRepo = new ScoreDBRepo(props);

            CompetitionServiceFacade service = new CompetitionServiceFacade(userDBRepo, participantDBRepo, roundDBRepo, scoreDBRepo);
            int serverPort = defaultPort;
            try 
            {
                serverPort = Int32.Parse(System.Configuration.ConfigurationSettings.AppSettings["competition.server.port"]);
            }
            catch (Exception e) 
            {
                Console.WriteLine("Wrong Port Number" + e.Message);
                Console.WriteLine("Using default port " + defaultPort);
            }
            Console.WriteLine("Starting server on port: " + serverPort);
            SerialCompetitionServer server = new SerialCompetitionServer("127.0.0.1", serverPort, service);
            server.Start();
            Console.WriteLine("Server started...");
            Console.ReadLine();
        }
    }

    public class SerialCompetitionServer : ConcurrentServer
    {
        private ICompetitionServices server;
        //private CompetitionObjectWorker worker;
        private CompetitionProtobuffWorker worker;

        public SerialCompetitionServer(string host, int port, ICompetitionServices server): base(host,port)
        {
            this.server = server;
            Console.WriteLine("SerialCompetitionServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            //worker = new CompetitionObjectWorker(server, client); 
            worker = new CompetitionProtobuffWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
}