using Sfw.Racing.DataRepository.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        public IDbConnection Create()
        {
            string key = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            SqlConnection conn = new SqlConnection(key);

            return (IDbConnection)conn;
        }
    }
}
