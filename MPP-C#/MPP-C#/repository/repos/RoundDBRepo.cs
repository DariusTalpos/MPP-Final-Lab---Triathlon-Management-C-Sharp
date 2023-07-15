using log4net;
using Microsoft.VisualBasic.ApplicationServices;
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
    internal class RoundDBRepo: IRoundRepo
    {
        private static readonly ILog log = LogManager.GetLogger("RoundDBRepo");

        IDictionary<string, string> props;

        public RoundDBRepo(IDictionary<string, string> properties)
        {
            props = properties;
        }

        public Round Delete(long id)
        {
            log.InfoFormat("deleting task {0}", id);
            Round round = FindOne(id);
            if (round != null) 
            {
                IDbConnection dbConnection = CommonUtils.GetConnection(props);
                using (var comm = dbConnection.CreateCommand())
                {
                    comm.CommandText = "delete from Rounds where id=@id";
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
            return round;
        }

        public List<Round> FindAll()
        {
            List<Round> rounds = new List<Round>();
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "select id,name from Rounds";
                using (var result = comm.ExecuteReader())
                {
                    while(result.Read())
                    {
                        long id = result.GetInt64(0);
                        string name = result.GetString(1);
                        Round round = new Round(name);
                        round.Id = id;
                        rounds.Add(round);
                    }
                }
            }
            log.InfoFormat("Exiting with {0}", rounds);
            return rounds;
        }

        public Round FindOne(long id)
        {
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "select name from Rounds where id=@id";
                var idParameter = comm.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.Value = id;
                comm.Parameters.Add(id);
                using (var result = comm.ExecuteReader())
                {
                    if (result.Read())
                    {
                        string name = result.GetString(0);
                        Round round = new Round(name);
                        round.Id = id;
                        log.InfoFormat("Exiting with {0}", round);
                        return round;
                    }
                }
            }
            return null;
        }

        public Round findRoundWithName(string name)
        {
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "select * from Rounds where name=@name";
                var nameParameter = comm.CreateParameter();
                nameParameter.ParameterName = "@name";
                nameParameter.Value = name;
                comm.Parameters.Add(nameParameter);
                using (var result = comm.ExecuteReader())
                {
                    if (result.Read())
                    {
                        long id = result.GetInt64(0);
                        Round round = new Round(name);
                        round.Id = id;
                        log.InfoFormat("Exiting with {0}", round);
                        return round;
                    }
                }
            }
            return null;
        }

        public Round Save(Round entity)
        {
            log.InfoFormat("saving task {0}", entity);
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "insert into Rounds(name) values (@name)";

                var name = comm.CreateParameter();
                name.ParameterName = "@name";
                name.Value = entity.Name;
                comm.Parameters.Add(name);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("DB failure");
                    return entity;
                }
            }
            return null;
        }

        public Round Update(Round entity)
        {
            log.InfoFormat("updating task {0}", entity);
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "update Rounds set name=@name where id=@id";

                var name = comm.CreateParameter();
                name.ParameterName = "@name";
                name.Value = entity.Name;
                comm.Parameters.Add(name);

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
