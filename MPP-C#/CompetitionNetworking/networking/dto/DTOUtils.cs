using CompetitionModel.model;

namespace CompetitionNetworking.networking.dto
{
    public class DTOUtils
    {
        public static User getFromDTO(UserDTO uDTO)
        {
            String username = uDTO.Username;
            String password = uDTO.Password;
            return new User(username, password);
        }

        public static UserDTO getDTO(User user)
        {
            String username = user.Username;
            String password = user.Password;
            return new UserDTO(username, password);
        }

        public static Participant getFromDTO(ParticipantDTO pDTO)
        {
            String name = pDTO.Name;
            int fullPoints = pDTO.fullPoints;
            long id = pDTO.id;
            Participant participant = new Participant(name, fullPoints);
            participant.Id=id;
            return participant;
        }

        public static ParticipantDTO getDTO(Participant participant)
        {
            String name = participant.Name;
            int fullPoints = participant.FullPoints;
            long id = participant.Id;
            return new ParticipantDTO(name, fullPoints, id);
        }

        public static List<Participant> getFromDTOParticipantList(List<ParticipantDTO> pDTO)
        {
            List<Participant> participants = new List<Participant>();
            foreach (ParticipantDTO participant in pDTO)
                participants.Add(getFromDTO(participant));
            return participants;
        }

        public static List<ParticipantDTO> getDTOParticipantList(List<Participant> participants)
        {
            List<ParticipantDTO> pDTO = new List<ParticipantDTO>();
            foreach (Participant participant in participants)
                pDTO.Add(getDTO(participant));
            return pDTO;
        }

        public static Round getFromDTO(RoundDTO rDTO)
        {
            String name = rDTO.Name;
            long id = rDTO.id;
            Round round = new Round(name);
            round.Id = id;
            return round;
        }

        public static RoundDTO getDTO(Round round)
        {
            String name = round.Name;
            long id = round.Id;
            return new RoundDTO(name, id);
        }

        public static List<RoundDTO> getDTORoundList(List<Round> rounds)
        {
            List<RoundDTO> roundDTOS = new List<RoundDTO>();
            foreach (Round round in rounds)
                roundDTOS.Add(getDTO(round));
            return roundDTOS;
        }

        public static List<Round> getFromDTORoundList(List<RoundDTO> roundDTOS)
        {
            List<Round> rounds = new List<Round>();
            foreach (RoundDTO roundDTO in roundDTOS)
                rounds.Add(getFromDTO(roundDTO));
            return rounds;
        }

        public static ScoreDTO getDTO(Score score)
        {
            ParticipantDTO participantDTO = getDTO(score.Participant);
            RoundDTO roundDTO = getDTO(score.Round);
            int points = score.Points;
            long id = score.Id;
            return new ScoreDTO(participantDTO, roundDTO, points, id);
        }

        public static Score getFromDTO(ScoreDTO sDTO)
        {
            Participant participant = getFromDTO(sDTO.Participant);
            Round round = getFromDTO(sDTO.Round);
            int points = sDTO.Points;
            long id = sDTO.id;
            Score score = new Score(participant, round, points);
            score.Id = id;
            return score;
        }

        public static List<Score> getFromDTOScoreList(List<ScoreDTO> scoreDTOS)
        {
            List<Score> scores = new List<Score>();
            foreach (ScoreDTO scoreDTO in scoreDTOS)
                scores.Add(getFromDTO(scoreDTO));
            return scores;
        }

        public static List<ScoreDTO> getDTOScoreList(List<Score> scores)
        {
            List<ScoreDTO> scoreDTOS = new List<ScoreDTO>();
            foreach (Score score in scores)
                scoreDTOS.Add(getDTO(score));
            return scoreDTOS;
        }
    }
}
