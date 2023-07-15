using CompetitionModel.model;
using CompetitionNetworking.networking.dto;
using CompetitionServices.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using static CompetitionNetworking.networking.protocol.ObjectRequestProtocol;
using static CompetitionNetworking.networking.protocol.ObjectResponseProtocol;
using static System.Formats.Asn1.AsnWriter;

namespace CompetitionNetworking.networking.protocol
{
    public class CompetitionObjectWorker : ICompetitionObserver
    {

        private ICompetitionServices server;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;

        public CompetitionObjectWorker(ICompetitionServices server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try 
            {
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private Response handleRequest(Request request)
        {
            Response response = null;
            if(request is LoginRequest)
            {
                Console.WriteLine("Login request ...");
                LoginRequest logReq = (LoginRequest)request;
                UserDTO udto = logReq.User;
                User user = DTOUtils.getFromDTO(udto);
                try
                {
                    lock (server)
                    {
                        server.login(user, this);
                    }
                    return new OkResponse();
                }
                catch (CompetitionException e)
                {
                    connected = false;
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is LogoutRequest)
            {
                Console.WriteLine("Logout request");
                LogoutRequest logReq = (LogoutRequest)request;
                UserDTO udto = logReq.User;
                User user = DTOUtils.getFromDTO(udto);
                try
                {
                    lock (server)
                    {

                        server.logout(user, this);
                    }
                    connected = false;
                    return new OkResponse();

                }
                catch (CompetitionException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is GetParticipantsRequest) 
            {
                Console.WriteLine("GetParticipantsRequest ...");
                GetParticipantsRequest getParticipantsRequest = (GetParticipantsRequest)request;
                try 
                {
                    List<Participant> participants;
                    lock (server)
                    {
                        participants = server.getParticipantList();
                    }
                    List<ParticipantDTO> participantDTOs = DTOUtils.getDTOParticipantList(participants);
                    return new GetParticipantsResponse(participantDTOs);
                }
                catch(CompetitionException e) 
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is GetRoundsRequest)
            {
                Console.WriteLine("GetRoundsRequest ...");
                GetRoundsRequest getRoundsRequest = (GetRoundsRequest)request;
                try
                {
                    List<Round> rounds;
                    lock (server)
                    {
                        rounds = server.getRoundList();
                    }
                    List<RoundDTO> roundDTOs = DTOUtils.getDTORoundList(rounds);
                    return new GetRoundsResponse(roundDTOs);
                }
                catch (CompetitionException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is GetScoresRequest)
            {
                Console.WriteLine("GetScoresRequest ...");
                GetScoresRequest getScoresRequest = (GetScoresRequest)request;
                string roundName = getScoresRequest.RoundName;
                try
                {
                    List<Score> scores;
                    lock (server)
                    {
                        scores = server.getScoreListFromRound(roundName);
                    }
                    List<ScoreDTO> scoresDTOs = DTOUtils.getDTOScoreList(scores);
                    return new GetScoresResponse(scoresDTOs);
                }
                catch (CompetitionException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is SendScoreRequest) 
            {
                Console.WriteLine("SendScoreRequest ...");
                SendScoreRequest sendScoreRequest = (SendScoreRequest)request;
                ScoreDTO scoreDTO = sendScoreRequest.Score;
                Score score = DTOUtils.getFromDTO(scoreDTO);
                try
                {
                    lock(server)
                    {
                        server.addRoundScore(score.Round.Name, score.Participant, score.Points);
                    }
                    return new OkResponse();
                }
                catch (CompetitionException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            return response;
        }

        private void sendResponse(Response response)
        {
            Console.WriteLine("sending response " + response);
            lock (stream)
            {
                formatter.Serialize(stream, response);
                stream.Flush();
            }

        }

        public virtual void run()
        {
            while (connected)
            {
                try
                {
                    object request = formatter.Deserialize(stream);
                    object response = handleRequest((Request)request);
                    if (response != null)
                    {
                        sendResponse((Response)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        public void newRound()
        {
            sendResponse(new NewRoundResponse());
        }

        public void newScore(Score score)
        {
            sendResponse(new NewScoreResponse(DTOUtils.getDTO(score)));
        }
    }
}
