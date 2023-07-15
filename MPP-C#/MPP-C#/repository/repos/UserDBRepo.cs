using log4net;
using MPP_C_.domain;
using MPP_C_.repository.template;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace MPP_C_.repository.repos
{
    internal class UserDBRepo : IUserRepo
    {
        private static readonly ILog log = LogManager.GetLogger("UserDBRepo");

        IDictionary<string, string> props;

        public UserDBRepo(IDictionary<string,string> properties)
        {
            props = properties;
        }

        public User Delete(long id)
        {
            log.InfoFormat("deleting task {0}", id);
            User user = FindOne(id);
            if (user != null)
            {
                IDbConnection dbConnection = CommonUtils.GetConnection(props);
                using (var comm = dbConnection.CreateCommand())
                {
                    comm.CommandText = "delete from Users where id=@id";
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
            return user;
        }

        public List<User> FindAll()
        {
            List<User> users = new List<User>();
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "select id,username,password from Users";
                using (var result = comm.ExecuteReader())
                {
                    while(result.Read())
                    {
                        long id = result.GetInt64(0);
                        string username = result.GetString(1);
                        string password = result.GetString(2);
                        User user = new User(username, password);
                        user.Id = id;
                        users.Add(user);
                    }
                }
            }
            log.InfoFormat("Exiting with {0}", users);
            return users;
        }

        public User FindOne(long id)
        {
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "select username,password from Users where id=@id";
                var idParameter = comm.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.Value = id;
                comm.Parameters.Add(id);
                using (var result = comm.ExecuteReader())
                {
                    if(result.Read())
                    {
                        string username = result.GetString(0);
                        string password = result.GetString(1);
                        User user = new User(username, password);
                        user.Id = id;
                        log.InfoFormat("Exiting with {0}", user);
                        return user;
                    }
                }
            }
            return null;

        }

        public User findUserWithNameAndPassword(string username, string password)
        {
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "select * from Users where username=@username and password=@password";
                var usernameParameter = comm.CreateParameter();
                usernameParameter.ParameterName = "@username";
                usernameParameter.Value = username;
                comm.Parameters.Add(usernameParameter);
                var passwordParameter = comm.CreateParameter();
                passwordParameter.ParameterName = "@password";
                passwordParameter.Value = password;
                comm.Parameters.Add(passwordParameter);
                using (var result = comm.ExecuteReader())
                {
                    if (result.Read())
                    {
                        long id = result.GetInt64(0);
                        User user = new User(username, password);
                        user.Id = id;
                        log.InfoFormat("Exiting with {0}", user);
                        return user;
                    }
                }
            }
            return null;
        }

        public User Save(User entity)
        {
            log.InfoFormat("saving task {0}", entity);
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using(var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "save into Users values (@username,@password)";
                
                var username = comm.CreateParameter();
                username.ParameterName = "@username";
                username.Value = entity.Username;
                comm.Parameters.Add(username);

                var password = comm.CreateParameter();
                password.ParameterName = "@password";
                password.Value = entity.Password;
                comm.Parameters.Add(password);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("DB failure");
                    return entity;
                }
            }
            return null;
        }

        public User Update(User entity)
        {
            log.InfoFormat("Updating task {0}", entity);
            IDbConnection dbConnection = CommonUtils.GetConnection(props);
            using (var comm = dbConnection.CreateCommand())
            {
                comm.CommandText = "update Users set username=@username, password=@password where id=@id)";

                var username = comm.CreateParameter();
                username.ParameterName = "@username";
                username.Value = entity.Username;
                comm.Parameters.Add(username);

                var password = comm.CreateParameter();
                password.ParameterName = "@password";
                password.Value = entity.Password;
                comm.Parameters.Add(password);

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
