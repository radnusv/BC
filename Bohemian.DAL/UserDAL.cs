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
    public class UserDAL
    {

        /*
         * Record Lock --bvRecordLock
         * Record Unlock -- bvRecordUnlock
         * Entity Log Proc -- bvLogEntityActivity 
         * All the above have not been implemented.         * 
         */

        //bvLoginAdd
        //bvLoginEdit
        //bvLoginDelete
        //bvLoginGetDetail
        //bvLoginIsUnique
        //bvLoginGetManagerList

        public static bool Add(User oEntity)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "spLoginAdd";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginID", oEntity.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginName", oEntity.LoginName));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginPW", oEntity.Password));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcUserName", oEntity.Name));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcUserDescr", oEntity.Description));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupID", oEntity.Group.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcActivationEmail", oEntity.ActivationEmail));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcActivationID", oEntity.ActivationID));
            Data.Command.Parameters.Add(new SqlParameter("@a_cIsActivated", oEntity.IsActivated));
            Data.Command.Parameters.Add(new SqlParameter("@a_dtLastPasswordChange", oEntity.LastPasswordChange));
            Data.Command.Parameters.Add(new SqlParameter("@a_dtLastLoginAttempt", oEntity.LastLoginAttempt));
            Data.Command.Parameters.Add(new SqlParameter("@a_iLoginFailCount", oEntity.LoginFailCount));
            Data.Command.Parameters.Add(new SqlParameter("@a_cAccountLocked", oEntity.IsAccountLocked));

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
        
        public static bool Update(User oEntity)
        {
            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "spLoginEdit";

            
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginID", oEntity.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginName", oEntity.LoginName));            
            Data.Command.Parameters.Add(new SqlParameter("@a_vcUserName", oEntity.Name));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcUserDescr", oEntity.Description));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupID", oEntity.Group.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcActivationEmail", oEntity.ActivationEmail));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcActivationID", oEntity.ActivationID));
            Data.Command.Parameters.Add(new SqlParameter("@a_cIsActivated", oEntity.IsActivated));
            Data.Command.Parameters.Add(new SqlParameter("@a_dtLastPasswordChange", oEntity.LastPasswordChange));
            Data.Command.Parameters.Add(new SqlParameter("@a_dtLastLoginAttempt", oEntity.LastLoginAttempt));
            Data.Command.Parameters.Add(new SqlParameter("@a_iLoginFailCount", oEntity.LoginFailCount));
            Data.Command.Parameters.Add(new SqlParameter("@a_cAccountLocked", oEntity.IsAccountLocked));

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

        public static bool IsLoginUnique(User oEntity)
        {
            bool doesExists=false;
            DBHelper Data = new DBHelper();
            
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginIsUnique";

            SqlParameter sqlParamLoginName = new SqlParameter("@a_vcLoginName", oEntity.LoginName);

            SqlParameter sqlParamIsUnique = new SqlParameter();
            sqlParamIsUnique.ParameterName = "@a_cUnique";
            sqlParamIsUnique.SqlDbType = SqlDbType.Char;
            sqlParamIsUnique.Size = 1;
            sqlParamIsUnique.Direction = ParameterDirection.Output;
            

            Data.Command.Parameters.Add(sqlParamLoginName);
            Data.Command.Parameters.Add(sqlParamIsUnique);
            
            try
            {
                Data.ExecuteNonQuery();
                doesExists = (Convert.ToString(sqlParamIsUnique.Value) == "Y" ? true : false);

            }
            catch (Exception ex)
            {
                throw ex;
                
            }
           return doesExists;


        }

        public static bool TestAccessTypeOnWindow(User oEntity, Window oWindow,AccessType oAccessType)
        {
            bool hasAccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "spLoginHasAccess";

            
            SqlParameter sqlParamHasAccess = new SqlParameter();
            sqlParamHasAccess.ParameterName = "@a_cAccessGranted";
            sqlParamHasAccess.SqlDbType = SqlDbType.Char;
            sqlParamHasAccess.Size = 1;
            sqlParamHasAccess.Direction = ParameterDirection.Output;


            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginID", oEntity.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oWindow.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeID", oAccessType.ID));
            Data.Command.Parameters.Add(sqlParamHasAccess);

            try
            {
                Data.ExecuteNonQuery();
                hasAccess = (Convert.ToString(sqlParamHasAccess.Value) == "Y" ? true : false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return hasAccess;


        }

        public static List<AccessType> GetWindowAccessTypes(User oEntity, Window oWindow)
        {
            List<AccessType> accessTypes = new List<AccessType>();
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "spLoginGetWindowAccess";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginID", oEntity.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oWindow.ID));


            try
            {
                dr = Data.ExecuteReader();
                while (dr.Read())
                {
                    AccessType oAccessType = new AccessType();
                    oAccessType.ID = Convert.ToString(dr["AccessTypeID"]);
                    oAccessType.Name = Convert.ToString(dr["AccessTypeName"]);
                    oAccessType.IsGranted = Convert.ToString(dr["Granted"]);
                    accessTypes.Add(oAccessType);
                }
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return accessTypes;


        }

        public static bool TestWindowAccess(User oEntity, Window oWindow)
        {
            bool hasAccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "spLoginHasWindowAccess";


            SqlParameter sqlParamHasAccess = new SqlParameter();
            sqlParamHasAccess.ParameterName = "@a_cAccessGranted";
            sqlParamHasAccess.SqlDbType = SqlDbType.Char;
            sqlParamHasAccess.Size = 1;
            sqlParamHasAccess.Direction = ParameterDirection.Output;


            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginID", oEntity.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oWindow.ID));            
            Data.Command.Parameters.Add(sqlParamHasAccess);

            try
            {
                Data.ExecuteNonQuery();
                hasAccess = (Convert.ToString(sqlParamHasAccess.Value) == "Y" ? true : false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return hasAccess;
        }

        public static bool ChangePassword(User oEntity )
        {
            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginPasswordChange";

            
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginID", oEntity.ID));            
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginPW", oEntity.Password));            
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

        public static bool UnLockAccount(User oEntity)
        {
            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginUnlockAccount";

            

            Data.Command.Parameters.Add(new SqlParameter("@a_vcUniqueID", oEntity.ID));
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

        public static bool Delete(User oEntity)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginDelete";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginID", oEntity.ID));

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

        public static User Get(string ID)
        {
            User oEntity = null;
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginGetDetail";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcUniqueID", ID));

            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        oEntity = new User();
                        oEntity.ID = Convert.ToString(dr["LoginID"]); 
                        oEntity.LoginName = Convert.ToString(dr["LoginName"]); 
                        oEntity.Password = Convert.ToString(dr["LoginPW"]); 
                        oEntity.Name = Convert.ToString(dr["UserName"]);
                        oEntity.Description = Convert.ToString(dr["UserDescr"]);
                        oEntity.Group = new LoginGroup { ID = Convert.ToString(dr["LoginGroupId"]) };
                        oEntity.ActivationEmail = Convert.ToString(dr["ActivationEmail"]);
                        oEntity.ActivationID = Convert.ToString(dr["ActivationID"]);
                        oEntity.IsActivated = Convert.ToString(dr["IsActivated"]);
                        oEntity.LastPasswordChange = Convert.ToDateTime(dr["LastPasswordChange"]);
                        oEntity.LastLoginAttempt = Convert.ToString(dr["LastLoginAttempt"]);
                        oEntity.LoginFailCount  = Convert.ToInt32(dr["LoginFailCount"]);
                        oEntity.IsAccountLocked = Convert.ToString(dr["AccountLocked"]);
                        
                        
                        
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

        public static List<User> GetAll()
        {
            List<User> users = new List<User>();
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginGetManagerList";
            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {
                    
                    while (dr.Read())
                    {
                        User oEntity = new User();
                        oEntity.ID = Convert.ToString(dr["LoginID"]); // Probably we need to add Null Check and if it is Null we should return empty string
                        oEntity.LoginName = Convert.ToString(dr["LoginName"]); // Probably we need to add Null Check and if it is Null we should return empty string
                        oEntity.Name = Convert.ToString(dr["UserName"]);
                        oEntity.Description = Convert.ToString(dr["UserDescr"]);
                        oEntity.IsAccountLocked = Convert.ToString(dr["AccountLocked"]);
                        if (dr["LoginGroupName"] != null)
                        {
                            oEntity.Group = new LoginGroup { Name = Convert.ToString(dr["LoginGroupName"]) };
                        }


                        users.Add(initialize(dr, ObjectInitializationTypes.KeyFields));
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
            return users;

        }

        public static User Authenticate(User oEntity, string clientComputerName)
        {

            DBHelper Data = new DBHelper();
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginValidate";

            SqlParameter sqlParamLoginId = new SqlParameter();
            sqlParamLoginId.ParameterName = "@a_vcLoginID";
            sqlParamLoginId.SqlDbType = SqlDbType.VarChar;
            sqlParamLoginId.Size = 20;
            sqlParamLoginId.Direction = ParameterDirection.Output;

            SqlParameter sqlParamPwdLastChangeDt = new SqlParameter();
            sqlParamPwdLastChangeDt.ParameterName = "@a_dtLastPasswordChange";
            sqlParamPwdLastChangeDt.SqlDbType = SqlDbType.DateTime;
            sqlParamPwdLastChangeDt.Size = 8;
            sqlParamPwdLastChangeDt.Direction = ParameterDirection.Output;


            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginName", oEntity.LoginName));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginPW", oEntity.Password));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcComputerName", clientComputerName));
            Data.Command.Parameters.Add(sqlParamLoginId);
            Data.Command.Parameters.Add(sqlParamPwdLastChangeDt);

            try
            {
                Data.ExecuteNonQuery();
                /*
                 * We should not assign LogInId to Authentication token
                 * This is just a temp solution - We should assign some randomly created Id 
                 * or we can store Encrypted LogId and this can later be used to compare against the LoginName
                 */
                if (sqlParamLoginId != null && sqlParamLoginId.Value != null && sqlParamLoginId.Value != System.DBNull.Value)
                {
                    oEntity = Get(Convert.ToString(sqlParamLoginId.Value));
                    oEntity.AuthenticationToken = Convert.ToString(sqlParamLoginId.Value);
                }
                else
                {
                    RecordLoginFailure(oEntity, Convert.ToInt32(Config.AppSettingValue("UserAuthenticationFailureThreshold")), Convert.ToInt32(Config.AppSettingValue("MinutesUntilReset")));
                    throw new UnauthorizedAccessException("InvalidLogin");
                }

            }
            catch (Exception ex)
            {

                throw new UnauthorizedAccessException("InvalidLogin");
            }
            return oEntity;


        }

        private static User initialize(SqlDataReader dr, ObjectInitializationTypes initializationType)
        {
            User oEntity = new User();
            oEntity.ID = Convert.ToString(dr["LoginID"]); 
            oEntity.LoginName = Convert.ToString(dr["LoginName"]); 
            oEntity.Name = Convert.ToString(dr["UserName"]);
            oEntity.Description = Convert.ToString(dr["UserDescr"]);
            oEntity.IsAccountLocked = Convert.ToString(dr["AccountLocked"]);
            if (dr["LoginGroupName"] != null)
            {
                oEntity.Group = new LoginGroup { Name = Convert.ToString(dr["LoginGroupName"]) };                    
            }

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

        public static bool RecordLoginFailure(User oEntity, int lockThreshold, int minutesUntilReset)
        {
            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginRecordFailure";

            
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginName", oEntity.LoginName));
            Data.Command.Parameters.Add(new SqlParameter("@a_iLockThreshold", lockThreshold));
            Data.Command.Parameters.Add(new SqlParameter("@a_iMinutesUntilReset", minutesUntilReset));
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

        public static List<SecureWindow> GetAccessibleManagerWindows(User oEntity)
        {
            List<SecureWindow> accessibleWindows = new List<SecureWindow> ();
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "spLoginGetAccessibleManagerWindows";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginID", oEntity.ID));
            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {

                    while (dr.Read())
                    {
                        SecureWindow oWindow = new SecureWindow();
                        oWindow.ID = Convert.ToString(dr["SecureWindowID"]);
                        oWindow.Name = Convert.ToString(dr["SecureWindowName"]);
                        accessibleWindows.Add(oWindow);
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
            return accessibleWindows;
        }


        public static bool ResetPassword(string loginName, string password)
        {
            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "spResetPassword";


            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginName", loginName));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcPassword", password));            
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

        public static List<LoginAccess> GetLoginAccessListByUser(User oUser)
        {
            List<LoginAccess> listLoginAccess = new List<LoginAccess>();
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "spLoginAccessListByUser";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginID", oUser.ID));
            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        LoginAccess oLoginAccess = new LoginAccess();
                        oLoginAccess.LoginID = Convert.ToString(dr["LoginID"]);
                        oLoginAccess.BCISSection = Convert.ToString(dr["BCISSection"]);
                        oLoginAccess.AccessType = Convert.ToString(dr["AccessType"]);
                        oLoginAccess.IsGranted = Convert.ToString(dr["Granted"]);
                        listLoginAccess.Add(oLoginAccess);
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
            return listLoginAccess;
        }

    }
}
