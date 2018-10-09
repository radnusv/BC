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
    public class AccessTypeDAL
    {
        //

        //bvAccessTypeGetOnWindow -- fetches access type associated with window
        //bvAccessTypeGetNotOnWindow -- fetches access type not associated with window

        public static List<AccessType> GetAll()
        {

            List<AccessType> accessTypes = null;
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvAccessTypeGetManagerList";
            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {
                    accessTypes = new List<AccessType>();
                    while (dr.Read())
                    {
                        accessTypes.Add(initialize(dr, ObjectInitializationTypes.IncludeDescription));
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
            return accessTypes;

        }


        public static List<AccessType> GetAssignedAccessTypesByWindow(SecureWindow oSecWindow)
        {

            List<AccessType> accessTypes = null;
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvAccessTypeGetOnWindow";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcKeyOne", oSecWindow.ID));
            
            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {
                    accessTypes = new List<AccessType>();
                    while (dr.Read())
                    {
                        accessTypes.Add(initialize(dr, ObjectInitializationTypes.IdAndNameOnly));
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
            return accessTypes;

        }

        public static List<AccessType> GetUnAssignedAccessTypesByWindow(SecureWindow oSecWindow)
        {

            List<AccessType> accessTypes = new List<AccessType>();
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvAccessTypeGetNotOnWindow";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcKeyOne", oSecWindow.ID));

            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {   
                    while (dr.Read())
                    {
                        accessTypes.Add(initialize(dr, ObjectInitializationTypes.IdAndNameOnly));
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
            return accessTypes;

        }

        public static List<AccessType> GetAccessTypesByLoginGroupAndWindow(LoginGroup oLoginGroup, SecureWindow oSecWindow)
        {

            List<AccessType> accessTypes = new List<AccessType>();
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvLoginGroupAccessGetDoubleKeyedList";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcKeyOne", oLoginGroup.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcKeyTwo", oSecWindow.ID));

            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {                    
                    while (dr.Read())
                    {
                        AccessType oEntity = new AccessType();
                        oEntity.ID = Convert.ToString(dr["AccessTypeID"]); // Probably we need to add Null Check and if it is Null we should return empty string

                        oEntity.Name = Convert.ToString(dr["AccessTypeName"]);
                        oEntity.IsGranted = Convert.ToString(dr["Granted"]);
                        accessTypes.Add(oEntity );
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
            return accessTypes;

        }

        public static List<AccessType> GetAccessTypesByUserAndSection(string loginID, string sectionID)
        {

            List<AccessType> accessTypes = null;
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "spGetAccessTypesByUserAndSection";
            Data.Command.Parameters.Add(new SqlParameter("@loginID", loginID));
            Data.Command.Parameters.Add(new SqlParameter("@sectionID", sectionID));

            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {
                    accessTypes = new List<AccessType>();
                    while (dr.Read())
                    {
                        AccessType oEntity = new AccessType();
                        oEntity.ID = Convert.ToString(dr["BCISAccessTypeID"]); // Probably we need to add Null Check and if it is Null we should return empty string

                        oEntity.Name = Convert.ToString(dr["AccessTypeDesc"]);
                        oEntity.IsGranted = Convert.ToString(dr["Granted"]);
                        accessTypes.Add(oEntity);
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
            return accessTypes;

        }



        public static bool Add(AccessType oEntity)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvAccessTypeAdd";
            
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeID", oEntity.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeName", oEntity.Name));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeDescr", oEntity.Description));
            
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

        public static bool Update(AccessType oEntity)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvAccessTypeEdit";

            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeID", oEntity.ID));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeName", oEntity.Name));
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeDescr", oEntity.Description));

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

        public static bool Delete(AccessType oEntity)
        {

            bool isSuccess = false;
            DBHelper Data = new DBHelper();

            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvAccessTypeDelete";
            
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAccessTypeID", oEntity.ID));

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

        public static AccessType Get(string ID)
        {
            AccessType oEntity = null;
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvAccessTypeGetDetail";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcUniqueID", ID));

            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        oEntity = initialize(dr, ObjectInitializationTypes.IncludeDescription);
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

        private static AccessType initialize(SqlDataReader dr, ObjectInitializationTypes initializationType)
        {
            AccessType oEntity = new AccessType();
            oEntity.ID = Convert.ToString(dr["AccessTypeID"]);
            
            oEntity.Name = Convert.ToString(dr["AccessTypeName"]);

            if (initializationType == ObjectInitializationTypes.IncludeDescription || initializationType == ObjectInitializationTypes.Full)
                oEntity.Description = Convert.ToString(dr["AccessTypeDescr"]);

            if (initializationType == ObjectInitializationTypes.Full )
                oEntity.IsGranted = Convert.ToString(dr["Granted"]);


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

        public static List<Section> GetAllSections()
        {
            List<Section> listSections = null;
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "spGetAllBCISWebSections";
            try
            {
                dr = Data.ExecuteReader();
                if (dr != null)
                {
                    listSections = new List<Section>();
                    while (dr.Read())
                    {
                        Section oSection = new Section();
                        oSection.ID = Convert.ToString(dr["BCISSectionID"]);
                        oSection.Name = Convert.ToString(dr["BCISSection"]);
                        listSections.Add(oSection);
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
            return listSections;
        }
    }
}
