using log4net;
using MPP_C_.domain;
using MPP_C_.repository.template;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.repository.repos
{
    internal class ParticipantDBRepo : IParticipantRepo
    {
        private static readonly ILog log = LogManager.GetLogger("ParticipantDBRepo");

        IDictionary<string, string> props;

        public ParticipantDBRepo(IDictionary<string, string> props)
        {
            this.props = props;
        }

        public Participant Delete(long id)
        {
            log.InfoFormat("deleting task {0}", id);
            Participant participant = FindOne(id);
            if(participant != null)
            {
                IDbConnection dbConnection = CommonUtils.GetConnection(props);
                using (var comm = dbConnection.CreateCommand())
                {
                    comm.CommandText = "delete from Participants where id=@id";
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
            return participant;
        }

        public List<Participant> FindAll()
        {
            List<Participant> participants = new List<Participant>();
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "select id,name,full_points from Participants order by name";
                using(var result = comm.ExecuteReader())
                {
                    while (result.Read())
                    {
                        long id = result.GetInt64(0);
                        string name = result.GetString(1);
                        int fullPoints = result.GetInt32(2);
                        Participant participant = new Participant(name, fullPoints);
                        participant.Id = id;
                        participants.Add(participant);
                    }
                }
            }
            log.InfoFormat("Exiting with {0}", participants);
            return participants;
        }

        public Participant FindOne(long id)
        {
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "select name,full_points from Participants where id=@id";
                var idParameter = comm.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.Value = id;
                comm.Parameters.Add(id);
                using (var result = comm.ExecuteReader())
                {
                    if (result.Read())
                    {
                        string name = result.GetString(0);
                        int fullPoints = result.GetInt32(1);
                        Participant participant = new Participant(name, fullPoints);
                        participant.Id = id;
                        log.InfoFormat("Exiting with {0}", participant);
                        return participant;
                    }
                }
            }
            return null;
        }

        public Participant Save(Participant entity)
        {
            log.InfoFormat("saving task {0}", entity);
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "insert into participants(name,full_points) values (@name,@full_points)";

                var name = comm.CreateParameter();
                name.ParameterName = "@name";
                name.Value = entity.Name;
                comm.Parameters.Add(name);

                var full_points = comm.CreateParameter();
                full_points.ParameterName = "@full_points";
                full_points.Value = entity.FullPoints;
                comm.Parameters.Add(full_points);


                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("DB failure");
                    return entity;
                }
            }
            return null;
        }

        public Participant Update(Participant entity)
        {
            log.InfoFormat("updating task {0}", entity);
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "update Participants set name=@name,full_points=@full_points where id=@id";

                var name = comm.CreateParameter();
                name.ParameterName = "@name";
                name.Value = entity.Name;
                comm.Parameters.Add(name);

                var full_points = comm.CreateParameter();
                full_points.ParameterName = "@full_points";
                full_points.Value = entity.FullPoints;
                comm.Parameters.Add(full_points);

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
