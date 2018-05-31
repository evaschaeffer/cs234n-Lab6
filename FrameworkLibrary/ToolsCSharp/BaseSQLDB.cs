using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ToolsCSharp
{
    /// <summary>
    /// BaseSQLDB is the class from which all data access classes that access a SQLServer 2000 database will be derived.
    /// The core functionality of establishing a connection with the database and executing simple stored procedures is
    /// also provided by this class.
    /// </summary>
    public abstract class BaseSQLDB
    {
        protected SqlConnection mConnection;
        private string mConnectionString = "";

        #region Constructors

        /// <summary>
        /// The default constructor, it gets the connection string from app.config and
        /// instantiates a connection object.  The connection is not yet open.
        /// </summary>
        public BaseSQLDB()
        {
            AppSettingsReader configReader = new AppSettingsReader();
            mConnectionString = (configReader.GetValue("dbObjectConnectionString", mConnectionString.GetType())).ToString();
            mConnection = new SqlConnection(mConnectionString);
        }

        public BaseSQLDB(string cnString)
        {
            if (cnString != "")
            {
                mConnectionString = cnString;
            }
            else
            {
                AppSettingsReader configReader = new AppSettingsReader();
                mConnectionString = (configReader.GetValue("dbObjectConnectionString", mConnectionString.GetType())).ToString();
            }
            mConnection = new SqlConnection(mConnectionString);
        }

        public BaseSQLDB(SqlConnection cn)
        {
            mConnectionString = cn.ConnectionString;
            mConnection = cn;
        }

        #endregion

        #region Properties

        protected string ConnectionString
        {
            get
            {
                return mConnectionString;
            }
        }
        #endregion

        #region Methods

        public SqlDataReader RunSQL(string sql)
        {
            SqlDataReader reader;

            mConnection.Open();
            SqlCommand command = new SqlCommand(sql, mConnection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public SqlDataReader RunProcedure(string spName)
        {
            SqlDataReader reader;

            mConnection.Open();
            SqlCommand command = new SqlCommand(spName, mConnection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public SqlDataReader RunProcedure(SqlCommand command)
        {
            SqlDataReader reader;

            mConnection.Open();
            command.Connection = mConnection;
            reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public int RunNonQueryProcedure(SqlCommand command)
        {
            mConnection.Open();
            command.Connection = mConnection;
            int rows = command.ExecuteNonQuery();
            mConnection.Close();
            return rows;
        }

        public DataSet RunProcedure(SqlCommand command, string tableAlias)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();

            mConnection.Open();
            command.Connection = mConnection;
            da.SelectCommand = command;
            da.Fill(ds, tableAlias);
            mConnection.Close();

            return ds;
        }

        public void RunProcedure(SqlCommand command, string tableAlias, DataSet ds)
        {
            SqlDataAdapter da = new SqlDataAdapter();

            if (this.mConnection.State == ConnectionState.Closed)
            {
                this.mConnection.Open();
            }
            command.Connection = this.mConnection;
            da.SelectCommand = command;
            da.Fill(ds, tableAlias);
            mConnection.Close();
        }

        #endregion
    }
}
