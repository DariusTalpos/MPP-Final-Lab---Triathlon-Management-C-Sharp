using CompetitionNetworking.networking.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionNetworking.networking.protocol
{
    internal class ObjectRequestProtocol
    {
        public interface Request { }

        [Serializable]
        public class LoginRequest : Request
        {
            private UserDTO user;

            public LoginRequest(UserDTO user)
            {
                this.user = user;
            }

            public virtual UserDTO User
            {
                get
                {
                    return user;
                }
            }
        }

        [Serializable]
        public class LogoutRequest : Request
        {
            private UserDTO user;

            public LogoutRequest(UserDTO user)
            {
                this.user = user;
            }

            public virtual UserDTO User
            {
                get
                {
                    return user;
                }
            }
        }

        [Serializable]
        public class GetRoundsRequest: Request
        {
           
        }

        [Serializable]
        public class GetScoresRequest : Request
        {
            private string roundName;

            public GetScoresRequest(string roundName)
            {
                this.roundName = roundName;
            }

            public virtual string RoundName
            { get { return roundName; } }
        }

        [Serializable]
        public class GetParticipantsRequest : Request
        {

        }

        [Serializable]
        public class SendScoreRequest : Request
        {
            private ScoreDTO score;

            public SendScoreRequest(ScoreDTO score)
            { this.score = score; }

            public virtual ScoreDTO Score
            { get { return score; } }
        }


    }
}
