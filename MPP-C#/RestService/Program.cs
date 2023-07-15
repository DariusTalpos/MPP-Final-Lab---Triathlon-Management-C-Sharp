using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CSharpRestClient
{
    class MainClass
    {
        static HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));

        private static string URL_Base = "http://localhost:8080/api/participants";

        public static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            RunAsync().Wait();
        }


        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:8080/api/participants");
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            //Get all
            //ParticipantREST[] resultALL = await GetParticipantsAsync("http://localhost:8080/api/participants");
            //Console.WriteLine("Am primit {0}", resultALL);

            //Get one
            //ParticipantREST resultONE = await GetParticipantAsync("http://localhost:8080/api/participants/1");
            //Console.WriteLine("Am primit {0}", resultONE);

            //Create
           //ParticipantREST participant = new ParticipantREST("C# Rester");
           /// ParticipantREST resultCREATE = await CreateParticipantAsync("http://localhost:8080/api/participants", participant);
            //Console.WriteLine("Am primit {0}", resultCREATE);
            //Console.ReadLine();

            //Update
            //ParticipantREST participantUpdated = new ParticipantREST(9,"C# Updated");
            //string resultUPDATE = await UpdateParticipantAsync("http://localhost:8080/api/participants", participantUpdated);
            //Console.WriteLine("Am primit {0}", resultUPDATE);
            //Console.ReadLine() ;

            //Delete
            string resultDELETE = await DeleteParticipantAsync("http://localhost:8080/api/participants/14");
            //Console.WriteLine("Am primit {0}", resultDELETE);

        }

        static async Task<ParticipantREST> GetParticipantAsync(string path)
        {
            ParticipantREST participant = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                participant = await response.Content.ReadAsAsync<ParticipantREST>();
            }
            return participant;
        }
        static async Task<ParticipantREST[]> GetParticipantsAsync(string path)
        {
            ParticipantREST[] participants = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                participants = await response.Content.ReadAsAsync<ParticipantREST[]>();
            }
            return participants;
        }


        static async Task<ParticipantREST> CreateParticipantAsync(string path, ParticipantREST participant)
        {
            ParticipantREST result = null;
            HttpResponseMessage response = await client.PostAsJsonAsync(path, participant);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<ParticipantREST>();
            }
            return result;
        }

        static async Task<string> UpdateParticipantAsync(string path, ParticipantREST participant)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(path, participant);
            if (response.IsSuccessStatusCode)
            {
                return "Success";
            }
            return "Failure";
        }

        static async Task<string> DeleteParticipantAsync(string path)
        {
            HttpResponseMessage response = await client.DeleteAsync(path);

            if (response.IsSuccessStatusCode)
            {
                return "Success";
            }
            return "Failure";
        }

    }

    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Request:");
            Console.WriteLine(request.ToString());
            if (request.Content != null)
            {
                Console.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Console.WriteLine("Response:");
            Console.WriteLine(response.ToString());
            if (response.Content != null)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            return response;
        }
    }

    public class ParticipantREST
    {
        public long id;
        public string name { get; set; }

        public int fullPoints { get; set; }

        public ParticipantREST()
        {

        }

        public ParticipantREST(string name, int fullPoints)
        {
            this.name = name;
            this.fullPoints = fullPoints;
        }

        public ParticipantREST(string name)
        {
            this.name = name;
            this.fullPoints=0;
        }

        public ParticipantREST(long id, string name)
        {
            this.id = id;
            this.name = name;
            this.fullPoints = 0;
        }
    }

}
