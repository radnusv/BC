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
    public class SecureWindowDAL
    {
        public static bool AddLoginGroup(SecureWindow oSecWin,LoginGroup oGroup,AccessType oAccessTyp)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginGroupAccessAdd";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupID", oGroup.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oSecWin.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeId", oAccessTyp.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcGranted", oAccessTyp.IsGranted));

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

        public static bool UpdateLoginGroup(SecureWindow oSecWin, LoginGroup oGroup, AccessType oAccessTyp)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginGroupAccessEdit";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupID", oGroup.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oSecWin.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeId", oAccessTyp.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcGranted", oAccessTyp.IsGranted));

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

        public static bool DeleteLoginGroup(SecureWindow oSecWin, LoginGroup oGroup, AccessType oAccessTyp)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginGroupAccessDelete";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupID", oGroup.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oSecWin.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeId", oAccessTyp.ID));
            

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

        public static bool UpdateLoginUser(Section oSection, User oUser, AccessType oAccessType)
        {

            //bool isSuccess = false;
            //DBHelper Data = new DBHelper();

            //Data.Command.CommandType = CommandType.StoredProcedure;
            //Data.Command.CommandText = "bvLoginAccessEdit";

            //Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginID", oUser.ID));
            //Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oSecWin.ID));
            //Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeId", oAccessTyp.ID));
            //Data.Command.Parameters.Add(new SqlParameter("@a_vcGranted", oAccessTyp.IsGranted));

            //try
            //{
            //    int rowsEffected = Data.ExecuteNonQuery();
            //    isSuccess = rowsEffected > 0;
            //}
            //catch (Exception ex)
            //{
            //    isSuccess = false;
            //    throw ex;
            //}
            //return isSuccess;


            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "spBCISWebLoginAccessEdit";

            Data.Command.Parameters.Add(new SqlParameter("@LoginID", oUser.ID));
            Data.Command.Parameters.Add(new SqlParameter("@SectionID", oSection.ID));
            Data.Command.Parameters.Add(new SqlParameter("@AccessTypeID", oAccessType.ID));
            Data.Command.Parameters.Add(new SqlParameter("@Granted", oAccessType.IsGranted));

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


        public static bool AddUser(SecureWindow oSecWin, User oUser, AccessType oAccessTyp)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginAccessAdd";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginID", oUser.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oSecWin.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeID", oAccessTyp.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcGranted", oAccessTyp.IsGranted));

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

        public static bool UpdateUser(SecureWindow oSecWin, User oUser, AccessType oAccessTyp)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginAccessEdit";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginID", oUser.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oSecWin.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeId", oAccessTyp.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcGranted", oAccessTyp.IsGranted));

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

        public static bool DeleteUser(SecureWindow oSecWin, User oUser, AccessType oAccessTyp)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginGroupAccessDelete";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcLoginGroupID", oUser.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oSecWin.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeId", oAccessTyp.ID));


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

        public static bool AddAccessType(SecureWindow oSecWin, AccessType oAccessTyp)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvSecureWindowAccessAdd";

            
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oSecWin.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeID", oAccessTyp.ID));
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

        public static bool DeleteAccessType(SecureWindow oSecWin, AccessType oAccessTyp)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvSecureWindowAccessDelete";


            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oSecWin.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeID", oAccessTyp.ID));
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


        public static List<SecureWindow> GetAll()
        {
           // 

            List<SecureWindow> lst = null;
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvSecureWindowGetManagerList";
            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {
                    lst = new List<SecureWindow>();
                    while (dr.Read())
                    {
                        lst.Add(initialize(dr, ObjectInitializationTypes.Full));
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
            return lst;


        }

        public static bool Add(SecureWindow oEntity)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvSecureWindowAdd";


            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oEntity.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowName", oEntity.Name));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowDescr", oEntity.Description));

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

        public static bool Update(SecureWindow oEntity)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvSecureWindowEdit";

            
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oEntity.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowName", oEntity.Name));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowDescr", oEntity.Description));

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

        public static bool Delete(SecureWindow oEntity)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvSecureWindowDelete";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcSecureWindowID", oEntity.ID));

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

        public static SecureWindow Get(string ID)
        {
            SecureWindow oEntity = null;
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvSecureWindowGetDetail";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcUniqueID", ID));

            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        oEntity = initialize(dr, ObjectInitializationTypes.Full);
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



       



        private static SecureWindow initialize(SqlDataReader dr, ObjectInitializationTypes initializationType)
        {
            SecureWindow oEntity = new SecureWindow();
            oEntity.ID = Convert.ToString(dr["SecureWindowID"]); 

            oEntity.Name = Convert.ToString(dr["SecureWindowName"]);         
   
            if(initializationType==ObjectInitializationTypes.Full)
                oEntity.Description = Convert.ToString(dr["SecureWindowDescr"]);         

            return oEntity;
        }

    }
}
