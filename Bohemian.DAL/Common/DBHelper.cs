
#region " Imports "
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Bohemian.DAL
{
    [System.EnterpriseServices.Synchronization()]
    public class DBHelper
    {

        #region " Declarations "
        private IDbConnection _Connection;
        private IDbCommand _Command;
        private IDataReader _DataReader;
        private string _ErrorMessage;

        private DBHelper _TransactionalManager;
        private IDbConnection _TransactionConnection;
        private IDbTransaction _Transaction;
        #endregion

        #region " Properties "
        public IDbConnection Connection
        {
            get
            {
                //grab a singleton connection if available 
                if ((this._TransactionConnection != null))
                {
                    this._Connection = this._TransactionConnection;
                }

                if (this._Connection == null)
                {

                    this._Connection = DBFactory.GetConnection(Config.ConnectionString);


                }

                return this._Connection;
            }
            set { this._Connection = value; }
        }

        public IDbCommand Command
        {
            get
            {
                if (this._Command == null)
                {
                    this._Command = DBFactory.GetCommand(this.Connection);

                    if ((this._TransactionalManager != null))
                    {
                        this._Command.Transaction = this._Transaction;
                        this._TransactionalManager.Command = this._Command;
                    }
                }
                return this._Command;
            }
            set { this._Command = value; }
        }

        public IDataReader DataReader
        {
            get { return (IDataReader)this.DataReader[(int)CommandBehavior.Default]; }
            set { this._DataReader = value; }
        }

        public IDataReader DReader(CommandBehavior Type)
        {
            if (this._DataReader == null)
            {
                this.OpenConnection();
                this._DataReader = this.Command.ExecuteReader(Type);
            }
            return this._DataReader;
        }

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }
        #endregion

        #region " Public Methods "
        public void TransactionReset()
        {
            try
            {
                if ((this._TransactionalManager != null))
                {
                    this._TransactionalManager.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                // added 7/3/12
                throw (ex);
            }

            this._TransactionalManager = null;
        }

        public string ExecuteNonQueryToRetrieveReturnValue()
        {
            //Create return parameter 
            IDataParameter Parm = DBFactory.GetParameter("@Return", null);
            Parm.Direction = ParameterDirection.ReturnValue;
            this.Command.Parameters.Add(Parm);

            this.OpenConnection();

            //Execute the nonquery 
            try
            {
                this.Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.RollBackTransaction();
                this._ErrorMessage = ex.Message;
                throw (ex);
            }
            finally
            {
                this.CloseConnection();
            }

            return ((IDbDataParameter)this.Command.Parameters["@Return"]).Value.ToString();
        }

        public int ExecuteNonQuery()
        {
            //Create return parameter 
            IDataParameter Parm = DBFactory.GetParameter("@Return", null);
            Parm.Direction = ParameterDirection.ReturnValue;
            this.Command.Parameters.Add(Parm);

            this.OpenConnection();

            //Execute the nonquery 
            try
            {
                return this.Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.RollBackTransaction();
                this._ErrorMessage = ex.Message;
                throw (ex);
            }
            finally
            {
                this.CloseConnection();
            }

            ////Return value of 0 denotes a fail 
            //if ((int)((IDbDataParameter)this.Command.Parameters["@Return"]).Value != 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public SqlDataReader ExecuteReader()
        {
            SqlDataReader dr = null;
            this.OpenConnection();
            try
            {
                dr=(SqlDataReader) DBFactory.ExecuteReader(this.Command);
            }
            catch (Exception ex)
            {
                // should add code to handle exceptions
            }
            
            return dr;
        }

        public DataSet ExecuteDataSet()
        {
            DataSet Ds = new DataSet();

            this.OpenConnection();

            try
            {
                DBFactory.GetDataAdapter(this.Command).Fill(Ds);
            }
            catch (Exception ex)
            {
                this.RollBackTransaction();
                this._ErrorMessage = ex.Message;
                throw (ex);
            }
            finally
            {
                this.CloseConnection();
            }

            return Ds;
        }

        //Creating a transaction 
        public void BeginTransaction()
        {
            this.BeginTransaction(IsolationLevel.Serializable);
        }

        public void BeginTransaction(IsolationLevel Isolation)
        {
            //Only create a new transaction if there is not one already set 
            if (this.Command.Transaction == null)
            {
                this.OpenConnection();
                this._Transaction = this.Connection.BeginTransaction(Isolation);
                this.Command.Transaction = this._Transaction;
            }
        }

        //Rollback a transaction, if one exists 
        public void RollBackTransaction()
        {
            if ((this.Command.Transaction != null))
            {
                this.Command.Transaction.Rollback();
            }
        }

        //Commit a transaction if it exists 
        public void CommitTransaction()
        {
            if ((this.Command.Transaction != null))
            {
                try
                {
                    this.Command.Transaction.Commit();
                }
                catch (InvalidOperationException)
                {
                    this.CloseConnection();
                }
            }
        }

        //These will set the global transaction class of one exists. This will set the common connection for all transactions until it is unregistered. 
        public void RegisterAsTransactionalHelper()
        {
            if (this._TransactionalManager == null)
            {
                this.BeginTransaction();
                this._TransactionalManager = this;
                this._TransactionConnection = this._Connection;
            }
        }

        public void UnRegisterAsTransactionalHelper()
        {
            if (object.ReferenceEquals(this._TransactionalManager, this))
            {
                //Ensure connection is closed 
                if (this._TransactionConnection.State != ConnectionState.Closed)
                {
                    this._TransactionConnection.Close();
                }

                this._TransactionConnection = null;
                this._TransactionalManager = null;
            }
        }
        #endregion

        #region " Private Methods "
        //Checks to ensure the command object has a connection object associated with it. 
        private void EnsureConnection()
        {
            if (this.Command.Connection == null)
            {
                this.Command.Connection = this.Connection;
            }
        }

        //Open the connection if it is not already open 
        private void OpenConnection()
        {
            this.EnsureConnection();
            if (this.Connection.State != ConnectionState.Open)
            {
                this.Connection.Open();
            }
        }

        private void CloseConnection()
        {
            //Only close connection if this is not a transaction call 
            if (this.Command.Transaction == null)
            {
                this.Connection.Close();
            }
        }
        #endregion

    }
}

