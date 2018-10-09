#region " Imports "
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Bohemian.Model;

#endregion

namespace Bohemian.DAL
{
    public class Config
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["BCISConnectionString"].ToString();

        public static string AppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];

        }
    }

    public enum ObjectInitializationTypes
    {
        Full=0,
        IdAndNameOnly,
        IncludeDescription,
        KeyFields,
        CompleteAndNoChild,
        CompleteAndKeyFieldsOfChild

    }

    public static class DALConstants
    {
        public static string UniqueIdLoginGroupType = "LoginGroup";
        public static string UniqueIdLoginUserType = "Login";
        public static string UniqueIdAccessType = "AccessType";
        public static string UniqueIdSecureWindow = "SecureWindow";
        
    }

    public static class DALUtility
    {
        public static string GetNextId(string nextIdType)
        {
            string nextId = "";
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "mcsGetNextID";

            SqlParameter sqlParamLoginName = new SqlParameter("@aType", nextIdType);

            SqlParameter sqlParamExists = new SqlParameter();
            sqlParamExists.ParameterName = "@aID";
            sqlParamExists.SqlDbType = SqlDbType.VarChar ;
            sqlParamExists.Size = 20;
            sqlParamExists.Direction = ParameterDirection.Output;


            Data.Command.Parameters.Add(sqlParamLoginName);
            Data.Command.Parameters.Add(sqlParamExists);

            try
            {
                Data.ExecuteNonQuery();
                nextId = Convert.ToString(sqlParamExists.Value) ;

            }
            catch (Exception ex)
            {
                // throw back the exception so that calling procedure can take appropriate action;
                throw ex;
            }
            return nextId;
        }


        public static string GetUniqueId(string uniqueIdType)
        {
            string nextId = "";
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvGetUniqueID";

            

            SqlParameter sqlParamUniqueId = new SqlParameter();
            sqlParamUniqueId.ParameterName = "@a_vcUniqueID";
            sqlParamUniqueId.SqlDbType = SqlDbType.VarChar;
            sqlParamUniqueId.Size = 20;
            sqlParamUniqueId.Direction = ParameterDirection.Output;


            Data.Command.Parameters.Add(new SqlParameter("@a_vcUniqueIDType", uniqueIdType));
            Data.Command.Parameters.Add(sqlParamUniqueId);

            try
            {
                Data.ExecuteNonQuery();
                nextId = Convert.ToString(sqlParamUniqueId.Value);

            }
            catch (Exception ex)
            {
                // throw back the exception so that calling procedure can take appropriate action;
                throw ex;
            }
            return nextId;
        }

        public static int GetMemberDaysInClub(string memberID)
        {
            //
            //@ - in
            //@ - out


            int daysInClub = 0;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvMemberGetDaysInClub";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcMemberID", memberID));
            

            SqlParameter sqlParamDaysInClub = new SqlParameter();
            sqlParamDaysInClub.ParameterName = "@a_iDaysInClub";
            sqlParamDaysInClub.SqlDbType = SqlDbType.BigInt;

            sqlParamDaysInClub.Direction = ParameterDirection.Output;



            Data.Command.Parameters.Add(sqlParamDaysInClub);

            
            Data.ExecuteNonQuery();
                
            if (sqlParamDaysInClub != null && sqlParamDaysInClub.Value != null && sqlParamDaysInClub.Value != System.DBNull.Value)                
                daysInClub = Convert.ToInt32(sqlParamDaysInClub.Value);
            
            return daysInClub;



        }


        public static void RecordAuditActivity(AuditActivity oEntity)
        {
            
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "spAuditActivity_Record";

            Data.Command.Parameters.Add(new SqlParameter("@Application", oEntity.Application));
            Data.Command.Parameters.Add(new SqlParameter("@PageName", oEntity.PageName));
            Data.Command.Parameters.Add(new SqlParameter("@RequestUrl", oEntity.RequestUrl));
            Data.Command.Parameters.Add(new SqlParameter("@Client_Computer", oEntity.ClientComputer));
            Data.Command.Parameters.Add(new SqlParameter("@PageAction", oEntity.PageAction));
            Data.Command.Parameters.Add(new SqlParameter("@UserName", oEntity.UserName));
            Data.Command.Parameters.Add(new SqlParameter("@AdditionalInfo", oEntity.AdditionalInfo));
            
            try
            {
                Data.ExecuteNonQuery();
            
            }
            catch (Exception ex)
            {   
                throw ex;
            }
            

        }
    }

}
