using CompetitionNetworking.networking.dto;
using CompetitionServices.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static CompetitionNetworking.networking.protocol.ObjectRequestProtocol;
using static CompetitionNetworking.networking.protocol.ObjectResponseProtocol;
using Com.Protocol;
using Google.Protobuf;

namespace CompetitionNetworking.networking.protobuffprotocol
{
    public class CompetitionProtobuffWorker: ICompetitionObserver
    {
        private ICompetitionServices server;
        private TcpClient connection;

        private NetworkStream stream;
        private volatile bool connected;

        public CompetitionProtobuffWorker(ICompetitionServices server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                stream = connection.GetStream();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private Com.Protocol.Response handleRequest(Com.Protocol.Request request)
        {
            Com.Protocol.Response response = null;
            if (request.Type == Com.Protocol.Request.Types.Type.Login)
            {
                Console.WriteLine("Login request ...");
                CompetitionModel.model.User user = ProtoUtils.getUser(request);
                try
                {
                    lock (server)
                    {
                        server.login(user, this);
                    }
                    return ProtoUtils.createOkResponse();
                }
                catch (CompetitionException e)
                {
                    connected = false;
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }
            if (request.Type == Com.Protocol.Request.Types.Type.Logout)
            {
                Console.WriteLine("Logout request");
                CompetitionModel.model.User user = ProtoUtils.getUser(request);
                try
                {
                    lock (server)
                    {
                        server.logout(user, this);
                    }
                    connected = false;
                    return ProtoUtils.createOkResponse();

                }
                catch (CompetitionException e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }
            if (request.Type == Com.Protocol.Request.Types.Type.GetParticipants)
            {
                Console.WriteLine("GetParticipantsRequest ...");
                try
                {
                    List<CompetitionModel.model.Participant> participants;
                    lock (server)
                    {
                        participants = server.getParticipantList();
                    }
                    return ProtoUtils.createParticipantsResponse(participants);
                }
                catch (CompetitionException e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }
            if (request.Type == Com.Protocol.Request.Types.Type.GetRounds)
            {
                Console.WriteLine("GetRoundsRequest ...");
                try
                {
                    List<CompetitionModel.model.Round> rounds;
                    lock (server)
                    {
                        rounds = server.getRoundList();
                    }
                    return ProtoUtils.createRoundsResponse(rounds);
                }
                catch (CompetitionException e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }
            if (request.Type == Com.Protocol.Request.Types.Type.GetScores)
            {
                Console.WriteLine("GetScoresRequest ...");
                string roundName = request.RoundName;
                try
                {
                    List<CompetitionModel.model.Score> scores;
                    lock (server)
                    {
                        scores = server.getScoreListFromRound(roundName);
                    }
                    return ProtoUtils.createScoresResponse(scores);
                }
                catch (CompetitionException e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }
            if (request.Type == Com.Protocol.Request.Types.Type.SendScore)
            {
                Console.WriteLine("SendScoreRequest ...");
                CompetitionModel.model.Score score = ProtoUtils.getScore(request);
                try
                {
                    lock (server)
                    {
                        server.addRoundScore(score.Round.Name, score.Participant, score.Points);
                    }
                    return ProtoUtils.createOkResponse();
                }
                catch (CompetitionException e)
                {
                    return ProtoUtils.createErrorResponse(e.Message);
                }
            }
            return response;
        }

        private void sendResponse(Com.Protocol.Response response)
        {
            Console.WriteLine("sending response " + response);
            lock (stream)
            {
                response.WriteDelimitedTo(stream);
                stream.Flush();
            }

        }

        public virtual void run()
        {
            while (connected)
            {
                try
                {
                    Com.Protocol.Request request = Com.Protocol.Request.Parser.ParseDelimitedFrom(stream);
                    Com.Protocol.Response response = handleRequest(request);
                    if (response != null)
                    {
                        sendResponse(response);
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
            sendResponse(ProtoUtils.createNewRoundResponse());
        }

        public void newScore(CompetitionModel.model.Score score)
        {
            sendResponse(ProtoUtils.createNewScoreResponse(score));
        }
    }
}
