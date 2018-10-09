using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Bohemian.DAL
{
    public class ExportDataDAL
    {
        public static DataTable GroveMapCamps()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetGroveMapCampList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public static DataTable GroveMapCampMemberList(string campId)
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetGroveMapCampMemberList";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcCampID", campId));
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public static DataTable GroveMapNameList()
        {
             DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetGroveMapNameList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
        
        
        public static DataTable SquirrelMemberList()
        {
             DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetSquirrelMemberList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
        //

        public static DataTable SquirrelCampList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetSquirrelCampList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        //
        public static DataTable BlueBookRosterList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetBlueBookRosterList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }


        public static DataTable BlueBookListOf50List()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetBlueBookListOf50List";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }


        public static DataTable BlueBookHonoraryRegularList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetBlueBookHonoraryRegularList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        //
        public static DataTable BlueBookHonoraryAssociateList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetBlueBookHonoraryAssociateList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }


        //
        public static DataTable OwlWorksCommitteeMemberList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetOwlWorksCommitteeMemberList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
        

        
        public static DataTable OwlWorksAssociateList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetOwlWorksAssociateList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        
        public static DataTable OwlWorksMemberList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetOwlWorksMemberList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public static DataTable OwlWorksWaitingListList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetOwlWorksWaitingListList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public static DataTable MailingActiveMemeberList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetMailingActiveMemberList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public static DataTable MailingPostalMemeberList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetMailingPostalMemberList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        //bvRptExportGetSpecialMemberExcelMemberList
        public static DataTable SpecialMember_MemberList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetSpecialMemberExcelMemberList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        //bvRptExportGetSpecialMemberExcelWaitingListList
        public static DataTable SpecialMember_WaitingList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetSpecialMemberExcelWaitingListList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public static DataTable SpecialMember_CommitteeMemberList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetSpecialMemberExcelCommitteeMemberList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        
        public static DataTable SpecialMember_AssociateGroupList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetAssociateGroupList";
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public static DataTable SpecialMember_AssociateGroupMembers(string associateGroupId)
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvRptExportGetAssociateGroupMembers";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcAssociateGroupID", associateGroupId));
            try
            {
                DataSet ds = Data.ExecuteDataSet();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        

    }
}
