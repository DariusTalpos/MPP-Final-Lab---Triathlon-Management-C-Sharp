using log4net;
using CompetitionModel.model;
using CompetitionPersistence.template;
using System.Data;
using CompetitionPersistence.persistence;

namespace CompetitionPersistence.repos
{
    public class ScoreDBRepo: IScoreRepo
    {
        private static readonly ILog log = LogManager.GetLogger("ScoreDBRepo");

        IDictionary<string, string> props;

        public ScoreDBRepo(IDictionary<string, string> properties)
        {
            props = properties;
        }

        public Score Delete(long id)
        {
            log.InfoFormat("deleting task {0}", id);
            Score score = FindOne(id);
            if (score != null)
            {
                IDbConnection dbConnection = CommonUtils.GetConnection(props);
                using (var comm = dbConnection.CreateCommand())
                {
                    comm.CommandText = "delete from Scores where id=@id";
                    var idParameter = comm.CreateParameter();
                    idParameter.ParameterName = "@id";
                    idParameter.Value = id;
                    comm.Parameters.Add(id);
                    var result = comm.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("DB failure");
                    }
                }
            }
            return score;
        }

        public List<Score> FindAll()
        {
            List<Score> scores = new List<Score>();
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "select r.id as 'round_id',r.name as 'round_name',p.id as 'participant_id',p.name as 'participant_name',p.full_points as 'participant_points', s.id as 'score_id', s.points as 'points' from Scores s inner join Rounds r on r.round_id=s.id inner join Participants p on p.id=s.participant_id";
                using (var result = comm.ExecuteReader())
                {
                    while(result.Read())
                    {
                        long roundID = result.GetInt64(0);
                        string roundName = result.GetString(1);
                        long participantID = result.GetInt64(2);
                        string participantName = result.GetString(3);
                        int fullPoints = result.GetInt32(4);
                        long scoreID = result.GetInt64(5);
                        int scorePoints = result.GetInt32(6);

                        Round round = new Round(roundName);
                        round.Id = roundID;
                        Participant participant = new Participant(participantName, fullPoints);
                        participant.Id = participantID;
                        Score score = new Score(participant, round, scorePoints);
                        score.Id = scoreID;
                        scores.Add(score);
                    }
                }
            }
            log.InfoFormat("Exiting with {0}", scores);
            return scores;
        }

        public List<Score> FindAllWithPointsInRound(string roundName)
        {
            List<Score> scores = new List<Score>();
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "select r.id as round_id,p.name as name,p.id as participant_id,p.full_points as participant_points,s.points as points,s.id as score_id from Scores s inner join Rounds r on r.id=s.round_id inner join Participants p on p.id=s.participant_id where r.name=@name order by s.points DESC";
                var nameParameter = comm.CreateParameter();
                nameParameter.ParameterName = "@name";
                nameParameter.Value = roundName;
                comm.Parameters.Add(nameParameter);
                using (var result = comm.ExecuteReader())
                {
                    while (result.Read())
                    {
                        long roundID = result.GetInt64(0);
                        long participantID = result.GetInt64(2);
                        string participantName = result.GetString(1);
                        int fullPoints = result.GetInt32(3);
                        long scoreID = result.GetInt64(5);
                        int scorePoints = result.GetInt32(4);

                        Round round = new Round(roundName);
                        round.Id = roundID;
                        Participant participant = new Participant(participantName, fullPoints);
                        participant.Id = participantID;
                        Score score = new Score(participant, round, scorePoints);
                        score.Id = scoreID;
                        scores.Add(score);
                    }
                }
            }
            log.InfoFormat("Exiting with {0}", scores);
            return scores;
        }

        public Score FindOne(long id)
        {
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "select r.id as 'round_id',r.name as 'round_name',p.id as 'participant_id',p.name as 'participant_name',p.full_points as 'participant_points', s.id as 'score_id', s.points as 'points' from Scores s inner join Rounds r on r.round_id=s.id inner join Participants p on p.id=s.participant_id where s.id=@id";
                var idParameter = comm.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.Value = id;
                comm.Parameters.Add(idParameter);
                using (var result = comm.ExecuteReader())
                {
                    if (result.Read())
                    {
                        long roundID = result.GetInt64(0);
                        string roundName = result.GetString(1);
                        long participantID = result.GetInt64(2);
                        string participantName = result.GetString(3);
                        int fullPoints = result.GetInt32(4);
                        long scoreID = result.GetInt64(5);
                        int scorePoints = result.GetInt32(6);

                        Round round = new Round(roundName);
                        round.Id = roundID;
                        Participant participant = new Participant(participantName, fullPoints);
                        participant.Id = participantID;
                        Score score = new Score(participant, round, scorePoints);
                        score.Id = scoreID;
                        log.InfoFormat("Exiting with {0}", score);
                        return score;
                    }
                }
            }
            return null;
        }

        public Score Save(Score entity)
        {
            log.InfoFormat("saving task {0}", entity);
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "insert into Scores(participant_id,round_id,points) values (@participant_id,@round_id,@points)";

                var participant_id = comm.CreateParameter();
                participant_id.ParameterName = "@participant_id";
                participant_id.Value = entity.Participant.Id;
                comm.Parameters.Add(participant_id);

                var round_id = comm.CreateParameter();
                round_id.ParameterName = "@round_id";
                round_id.Value = entity.Round.Id;
                comm.Parameters.Add(round_id);

                var points = comm.CreateParameter();
                points.ParameterName = "@points";
                points.Value = entity.Points;
                comm.Parameters.Add(points);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("DB failure");
                    return entity;
                }
            }
            return null;
        }

        public Score Update(Score entity)
        {
            log.InfoFormat("updating task {0}", entity);
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "update Score set participant_id=@participant_id,round_id=@round_id,points=@points where id=@id";

                var participant_id = comm.CreateParameter();
                participant_id.ParameterName = "@participant_id";
                participant_id.Value = entity.Participant.Id;
                comm.Parameters.Add(participant_id);

                var round_id = comm.CreateParameter();
                round_id.ParameterName = "@round_id";
                round_id.Value = entity.Round.Id;
                comm.Parameters.Add(round_id);

                var points = comm.CreateParameter();
                points.ParameterName = "@points";
                points.Value = entity.Points;
                comm.Parameters.Add(points);

                var id = comm.CreateParameter();
                id.ParameterName = "@id";
                id.Value = entity.Id;
                comm.Parameters.Add(id);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("DB failure");
                    return entity;
                }
            }
            return null;
        }
    }
}
