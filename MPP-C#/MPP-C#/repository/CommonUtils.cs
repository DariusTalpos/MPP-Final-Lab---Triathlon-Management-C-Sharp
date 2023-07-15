using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.repository
{
    public class CommonUtils
    {
        private static IDbConnection instance = null;

        public static IDbConnection GetConnection(IDictionary<string, string> parameters)
        {
            if (instance == null || instance.State == System.Data.ConnectionState.Closed)
            {
                instance = GetNewConnection(parameters);
                instance.Open();
            }
            return instance;
        }

        public static IDbConnection GetNewConnection(IDictionary<string, string> parameters)
        {
            return ConnectionUtils.ConnectionFactory.getInstance().createConnection(parameters);
        }
    }
}
