using Com.Protocol;
using CompetitionModel.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CompetitionNetworking.networking.protobuffprotocol
{
    internal class ProtoUtils
    {
        public static Response createOkResponse()
        {
            return new Response { Type = Response.Types.Type.Ok };
        }

        public static Response createErrorResponse(String text)
        {
            return new Response { Type = Response.Types.Type.Error, Error = text};
        }

        public static Response createParticipantsResponse(List<CompetitionModel.model.Participant> participants)
        {
            Response response = new Response { Type = Response.Types.Type.Ok};
            foreach (CompetitionModel.model.Participant participant in participants)
            {
                Com.Protocol.Participant participantDTO = new Com.Protocol.Participant { Id=participant.Id, Name=participant.Name, FullPoints=participant.FullPoints };
                response.Participants.Add(participantDTO);
            }

            return response;
        }

        public static Response createRoundsResponse(List<CompetitionModel.model.Round> rounds)
        {
            Response response = new Response { Type = Response.Types.Type.Ok };
            foreach (CompetitionModel.model.Round round in rounds)
            {
                Com.Protocol.Round roundDTO = new Com.Protocol.Round {Id=round.Id, Name=round.Name };
                response.Rounds.Add(roundDTO);
            }

            return response;
        }

        public static Response createScoresResponse(List<CompetitionModel.model.Score> scores)
        {
            Response response = new Response { Type = Response.Types.Type.Ok };
            foreach (CompetitionModel.model.Score score in scores)
            {
                Com.Protocol.Participant participantDTO = new Com.Protocol.Participant { Id = score.Participant.Id, Name = score.Participant.Name, FullPoints = score.Participant.FullPoints };
                Com.Protocol.Round roundDTO = new Com.Protocol.Round { Id = score.Round.Id, Name = score.Round.Name };
                Com.Protocol.Score scoreDTO = new Com.Protocol.Score { Id = score.Id, Participant = participantDTO, Round = roundDTO, Points = score.Points };
                response.Scores.Add(scoreDTO);
            }

            return response;
        }

        public static Response createNewRoundResponse()
        {
            return new Response { Type = Response.Types.Type.NewRound };
        }

        public static Response createNewScoreResponse(CompetitionModel.model.Score score)
        {
            Response response = new Response { Type = Response.Types.Type.NewScore };
            Com.Protocol.Participant participantDTO = new Com.Protocol.Participant { Id = score.Participant.Id, Name = score.Participant.Name, FullPoints = score.Participant.FullPoints };
            Com.Protocol.Round roundDTO = new Com.Protocol.Round { Id = score.Round.Id, Name = score.Round.Name };
            Com.Protocol.Score scoreDTO = new Com.Protocol.Score { Id = score.Id, Participant = participantDTO, Round = roundDTO, Points = score.Points };
            response.Score =scoreDTO;
            return response;
        }

        public static CompetitionModel.model.User getUser(Request request)
        {
            Com.Protocol.User userDTO = request.User;
            CompetitionModel.model.User user = new CompetitionModel.model.User(userDTO.Username, userDTO.Password);
            user.Id = userDTO.Id;
            return user;
        }

        public static CompetitionModel.model.Score getScore(Request request)
        {
            Com.Protocol.Score scoreDTO = request.Score;
            Com.Protocol.Participant participantDTO = request.Score.Participant;
            Com.Protocol.Round roundDTO = request.Score.Round;
            
            CompetitionModel.model.Participant participant = new CompetitionModel.model.Participant(participantDTO.Name,participantDTO.FullPoints);
            participant.Id = participantDTO.Id;
            CompetitionModel.model.Round round = new CompetitionModel.model.Round(roundDTO.Name);
            round.Id = roundDTO.Id;
            CompetitionModel.model.Score score = new CompetitionModel.model.Score(participant,round,scoreDTO.Points);
            score.Id = scoreDTO.Id;
            return score;
        }



        public static List<CompetitionModel.model.Round> getRoundList(Response response)
        {
            var roundList = response.Rounds;
            List<CompetitionModel.model.Round> rounds = new List<CompetitionModel.model.Round>();
            foreach (Com.Protocol.Round r in roundList)
            {
                CompetitionModel.model.Round round = new CompetitionModel.model.Round(r.Name);
                round.Id = r.Id;
                rounds.Add(round);
            }
            return rounds;
        }

    }

}
