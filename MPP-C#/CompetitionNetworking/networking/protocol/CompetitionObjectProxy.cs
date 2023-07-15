using CompetitionModel.model;
using CompetitionNetworking.networking.dto;
using CompetitionServices.services;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using static CompetitionNetworking.networking.protocol.ObjectRequestProtocol;
using static CompetitionNetworking.networking.protocol.ObjectResponseProtocol;

namespace CompetitionNetworking.networking.protocol
{
    public class CompetitionObjectProxy : ICompetitionServices
    {
        private string host;
        private int port;

        private ICompetitionObserver client;

        private NetworkStream stream;
        private IFormatter formatter;
        private TcpClient connection;
        private Queue<Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;

        public CompetitionObjectProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<Response>();
        }

        public void addRoundScore(string roundName, Participant participant, int points)
        {
            sendRequest(new SendScoreRequest(DTOUtils.getDTO(new Score(participant, new Round(roundName), points))));
            Response response = readResponse();
        }

        public List<Participant> getParticipantList()
        {
            sendRequest(new GetParticipantsRequest());
            Response response = readResponse();
            if (response is ErrorResponse) 
            {
                ErrorResponse errorResponse = (ErrorResponse) response;
                throw new CompetitionException(errorResponse.Message);
            }
            GetParticipantsResponse resp = (GetParticipantsResponse) response;
            List<ParticipantDTO> participantsDTO = resp.Participants;
            List<Participant> participants = DTOUtils.getFromDTOParticipantList(participantsDTO);
            return participants;

        }

        public List<Round> getRoundList()
        {
            sendRequest(new GetRoundsRequest());
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse errorResponse = (ErrorResponse)response;
                throw new CompetitionException(errorResponse.Message);
            }
            GetRoundsResponse resp = (GetRoundsResponse)response;
            List<RoundDTO> roundsDTO = resp.Rounds;
            List<Round> rounds = DTOUtils.getFromDTORoundList(roundsDTO);
            return rounds;
        }

        public List<Score> getScoreListFromRound(string roundName)
        {
            sendRequest(new GetScoresRequest(roundName));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse errorResponse = (ErrorResponse)response;
                throw new CompetitionException(errorResponse.Message);
            }
            GetScoresResponse resp = (GetScoresResponse)response;
            List<ScoreDTO> scoresDTO = resp.Scores;
            List<Score> scores = DTOUtils.getFromDTOScoreList(scoresDTO);
            return scores;
        }

        public void login(User user, ICompetitionObserver client)
        {
            initializeConnection();
            UserDTO udto = DTOUtils.getDTO(user);
            sendRequest(new LoginRequest(udto));
            Response response = readResponse();
            if (response is OkResponse)
            {
                this.client = client;
                return;
            }
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                closeConnection();
                throw new CompetitionException(err.Message);
            }
        }

        public void logout(User user, ICompetitionObserver client)
        {
            UserDTO udto = DTOUtils.getDTO(user);
            sendRequest(new LogoutRequest(udto));
            Response response = readResponse();
            closeConnection();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new CompetitionException(err.Message);
            }
        }

        private void initializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                startReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void closeConnection()
        {
            finished = true;
            try
            {
                stream.Close();

                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private void startReader()
        {
            Thread tw = new Thread(run);
            tw.Start();
        }
        private void sendRequest(Request request)
        {
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                throw new CompetitionException("Error sending object " + e);
            }

        }

        private Response readResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();

                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        private void handleUpdate(UpdateResponse update)
        {
            if (update is NewRoundResponse)
            {
                NewRoundResponse response = (NewRoundResponse)update;
                client.newRound();
            }
            if (update is NewScoreResponse)
            {
                NewScoreResponse response = (NewScoreResponse)update;
                Score score = DTOUtils.getFromDTO(response.Score);
                client.newScore(score);
            }
        }

        public virtual void run()
        {
            while (!finished)
            {
                try
                {
                    object response = formatter.Deserialize(stream);
                    Console.WriteLine("response received " + response);
                    if (response is UpdateResponse)
                    {
                        handleUpdate((UpdateResponse)response);
                    }
                    else
                    {

                        lock (responses)
                        {


                            responses.Enqueue((Response)response);

                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }

            }
        }

    }
}
