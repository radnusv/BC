
#region " Imports "

using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System;

#endregion

namespace Bohemian.DAL
{
    #region " Enums "
    public enum DBType
    {
        Sql,
        Odbc,
        OleDb
    }
    #endregion

    [System.EnterpriseServices.Synchronization()]
    public class DBFactory
    {

        #region " Declarations "
        private static DBType _DatabaseType = DBType.Sql;
        #endregion

        #region " Properties "
        public static DBType DatabaseType
        {
            get { return DBFactory._DatabaseType; }
            set { DBFactory._DatabaseType = value; }
        }
        #endregion

        #region " Methods "
        public static System.Data.IDbConnection GetConnection()
        {
            switch (DatabaseType)
            {
                case DBType.Sql:
                    return new SqlConnection();
                case DBType.Odbc:
                    break;

                case DBType.OleDb:
                    return new System.Data.OleDb.OleDbConnection();
            }

            return null;
        }

        public static System.Data.IDbConnection GetConnection(string Connect)
        {
            switch (DatabaseType)
            {
                case DBType.Sql:
                    return new SqlConnection(Connect);
                case DBType.Odbc:
                    break;

                case DBType.OleDb:
                    return new System.Data.OleDb.OleDbConnection(Connect);
            }

            return null;
        }

        public static System.Data.IDbCommand GetCommand(System.Data.IDbConnection Connection)
        {
            IDbCommand Comm = new SqlCommand();

            switch (DBFactory.DatabaseType)
            {
                case DBType.Sql:
                    Comm.Connection = Connection;
                    return Comm;
                //break;
                case DBType.Odbc:
                    break;
                case DBType.OleDb:
                    Comm = new OleDbCommand();
                    Comm.Connection = Connection;
                    return Comm;
                //break;
            }

            return null;
        }

        public static System.Data.IDataParameter GetParameter()
        {
            switch (DBFactory.DatabaseType)
            {
                case DBType.Sql:
                    return new SqlParameter();
                case DBType.Odbc:
                    break;

                case DBType.OleDb:
                    return new OleDbParameter();
            }

            return null;
        }

        public static System.Data.IDataParameter GetParameter(string Name, object Value)
        {
            switch (DBFactory.DatabaseType)
            {
                case DBType.Sql:
                    return new SqlParameter(Name, Value);
                case DBType.Odbc:
                    break;

                case DBType.OleDb:
                    return new OleDbParameter(Name, Value);
            }

            return null;
        }

        public static System.Data.IDataAdapter GetDataAdapter()
        {
            switch (DBFactory.DatabaseType)
            {
                case DBType.Sql:
                    return new SqlDataAdapter();
                case DBType.Odbc:
                    break;

                case DBType.OleDb:
                    return new OleDbDataAdapter();
            }

            return null;
        }

        public static System.Data.IDataAdapter GetDataAdapter(System.Data.IDbCommand Command)
        {
            switch (DBFactory.DatabaseType)
            {
                case DBType.Sql:
                    return new SqlDataAdapter((SqlCommand)Command);
                case DBType.Odbc:
                    break;

                case DBType.OleDb:
                    return new OleDbDataAdapter((OleDbCommand)Command);
            }

            return null;
        }

        public static System.Data.IDataReader ExecuteReader(System.Data.IDbCommand Command)
        {
            switch (DBFactory.DatabaseType)
            {
                case DBType.Sql:
                    return ((SqlCommand)Command).ExecuteReader(CommandBehavior.CloseConnection);
                case DBType.Odbc: 
                    break;

                case DBType.OleDb:
                    return ((OleDbCommand)Command).ExecuteReader(CommandBehavior.CloseConnection);
            }

            return null;
        }

        #endregion

    }
}
