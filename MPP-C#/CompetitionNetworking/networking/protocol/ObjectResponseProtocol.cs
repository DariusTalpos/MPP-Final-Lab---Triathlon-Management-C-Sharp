using CompetitionNetworking.networking.dto;

namespace CompetitionNetworking.networking.protocol
{
    internal class ObjectResponseProtocol
    {
        public interface Response
        {
        }

        [Serializable]
        public class OkResponse : Response
        {

        }

        [Serializable]
        public class ErrorResponse : Response
        {
            private string message;

            public ErrorResponse(string message)
            {
                this.message = message;
            }

            public virtual string Message
            {
                get
                {
                    return message;
                }
            }
        }

        [Serializable]
        public class GetParticipantsResponse: Response
        {
            private List<ParticipantDTO> participants;

            public GetParticipantsResponse(List<ParticipantDTO> participants)
            {
                this.participants = participants;
            }

            public virtual List<ParticipantDTO> Participants
            {
                get
                {
                    return participants;
                }
            }
        }

        [Serializable]
        public class GetRoundsResponse : Response
        {
            private List<RoundDTO> rounds;

            public GetRoundsResponse(List<RoundDTO> participants)
            {
                this.rounds = participants;
            }

            public virtual List<RoundDTO> Rounds
            {
                get
                {
                    return rounds;
                }
            }
        }

        [Serializable]
        public class GetScoresResponse : Response
        {
            private List<ScoreDTO> scores;

            public GetScoresResponse(List<ScoreDTO> scores)
            {
                this.scores = scores;
            }

            public virtual List<ScoreDTO> Scores
            {
                get
                {
                    return scores;
                }
            }
        }


        public interface UpdateResponse : Response
        {
        }

        [Serializable]
        public class NewRoundResponse : UpdateResponse
        {

        }

        [Serializable]
        public class NewScoreResponse : UpdateResponse
        {
            private ScoreDTO score;

            public NewScoreResponse(ScoreDTO score)
            {
                this.score = score;
            }

            public virtual ScoreDTO Score 
            { get { return score; } }

        }

    }
}
