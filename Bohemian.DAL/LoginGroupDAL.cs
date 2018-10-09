using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Bohemian.Model;

namespace Bohemian.DAL
{
    public class LoginGroupDAL
    {
        public static bool Add(LoginGroup oEntity)
        {
            
            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginGroupAdd";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupID", oEntity.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupName", oEntity.Name));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupDescr", oEntity.Description));            

            try
            {
                int rowsEffected = Data.ExecuteNonQuery();
                isSuccess = rowsEffected > 0;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw ex;
            }
            return isSuccess;

        }

        public static bool Update(LoginGroup oEntity)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginGroupEdit";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupID", oEntity.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupName", oEntity.Name));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupDescr", oEntity.Description));

            try
            {
                int rowsEffected = Data.ExecuteNonQuery();
                isSuccess = rowsEffected > 0;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw ex;
            }
            return isSuccess;

        }

        public static bool Delete(LoginGroup oEntity)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginGroupDelete";


            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupID", oEntity.ID));

            try
            {
                int rowsEffected = Data.ExecuteNonQuery();
                isSuccess = rowsEffected > 0;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw ex;
            }
            return isSuccess;

        }

        public static LoginGroup Get(string ID)
        {
            LoginGroup oEntity = null;
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginGroupGetDetail";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcUniqueID", ID));

            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        oEntity = initialize(dr, ObjectInitializationTypes.KeyFields);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr.Close();
            }
            return oEntity;





        }

        public static List<LoginGroup> GetAll()
        {
            List<LoginGroup> oEntity = new List<LoginGroup>();
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginGroupGetManagerList";            

            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        oEntity.Add(initialize(dr, ObjectInitializationTypes.KeyFields));
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr.Close();
            }
            return oEntity;





        }


        private static LoginGroup initialize(SqlDataReader dr, ObjectInitializationTypes initializationType)
        {
            LoginGroup oEntity = new LoginGroup();
            oEntity.ID = Convert.ToString(dr["LoginGroupID"]); 
            oEntity.Name = Convert.ToString(dr["LoginGroupName"]); 
            oEntity.Description = Convert.ToString(dr["LoginGroupDescr"]);            

            switch (initializationType)
            {
                case ObjectInitializationTypes.CompleteAndNoChild:
                    break;
                case ObjectInitializationTypes.CompleteAndKeyFieldsOfChild:
                    break;
                case ObjectInitializationTypes.Full:
                    break;
            }

            return oEntity;
        }


    }
}
