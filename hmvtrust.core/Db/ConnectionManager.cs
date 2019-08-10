using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;

namespace hmvtrust.core
{
    
    public class ConnectionManager
    {

        static Dictionary<long, string> clientConnections = new Dictionary<long, string>();
        static object synclock = new object();

        /// <summary>
        /// Get connection to user catelog database there will always be one instance of the catelog database
        /// </summary>
        /// <returns></returns>
        internal static IDbConnection GetCatalogConnection()
        {
            IDbConnection dbConnection = null;
            string conStr = ConfigurationManager.ConnectionStrings["CatalogConnectionStr"].ConnectionString;

            if (!string.IsNullOrEmpty(conStr))
                dbConnection = new SqlConnection(conStr);

            return dbConnection;
        }


        /// <summary>
        /// Get connection to the specific user db instance.
        /// </summary>
        /// <returns></returns>
        internal static IDbConnection GetClientConnection()
        {
            long clientId = 0;//Not requored
            string conStr = "";//FetchClientConnection(clientId);

            if (!string.IsNullOrEmpty(conStr))
                return new SqlConnection(conStr);
            else
                throw new ArgumentNullException("Invalid Client Connection");

        }

        

        /// <summary>
        /// Create a nhibernate Session Factory for Given client
        /// </summary>
        /// <returns></returns>

        public static ISessionFactory GetSessionFactory()
        {
            NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration();
            cfg.Configure();

            ISessionFactory sessionFactory = cfg.BuildSessionFactory();
            return sessionFactory;
        }


      

    }
}
