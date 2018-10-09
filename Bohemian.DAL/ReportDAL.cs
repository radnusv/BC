using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Bohemian.Model;

namespace Bohemian.DAL
{

    
    public class ReportDAL
    {


        private static string GetSPNameForTab(string tabName)
        {
            string spName = "";
            switch (tabName)
            {
                case "ASSOCIATES":
                    spName = "bvReportGetAssociatesReportList";
                    break;
                case "CAMP":
                    spName = "bvReportGetCampReportList";
                    break;
                case "CATEGORY":
                    spName = "bvReportGetCategoryReportList";
                    break;
                case "CHAIRMAN":
                    spName = "bvReportGetChairmanReportList";
                    break;
                case "COMMITTEE":
                    spName = "bvReportGetCommitteeReportList";
                    break;
                case "DATE":
                    spName = "bvReportGetDateReportList";
                    break;
                case "ENCAMPMENT":
                    spName = "bvReportGetEncampmentReportList";
                    break;
                case "EVENT":
                    spName = "bvReportGetEventReportList";
                    break;
                case "MEMBER":
                    spName = "bvReportGetMemberReportList";
                    break;
                
                case "OTHER":
                    spName = "bvReportGetOtherReportList";
                    break;
                case "SPRINGJINKS":
                    spName = "bvReportGetSpringJinksReportList";
                    break;
                case "OBSOLETE":
                default:
                    spName = "";
                    break;

            }

            return spName;
        }

        public static Report Get(string id)
        {
            Report rpt = null;
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvReportGetDetail";
            Data.Command.Parameters.Add(new SqlParameter("@a_vcUniqueID", id));
            try
            {
                dr = Data.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        rpt = new Report();
                        rpt.ID = Convert.ToString(dr["ReportID"]);
                        rpt.Name = Convert.ToString(dr["ReportName"]);
                        rpt.FileName = Convert.ToString(dr["ReportFileName"]);
                        rpt.TabName = Convert.ToString(dr["ReportTab"]);                        
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
            return rpt;
        }

        public static List<Report> GetAllByTab(string tabName)
        {
            List<Report> reports = new List<Report>();
            DBHelper Data = new DBHelper();
            SqlDataReader dr = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = ReportDAL.GetSPNameForTab(tabName);
            try
            {
                dr = Data.ExecuteReader();
                
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        Report rpt = new Report();                        
                        rpt.ID = Convert.ToString(dr["ReportID"]);
                        rpt.Name = Convert.ToString(dr["ReportName"]);
                        rpt.FileName = Convert.ToString(dr["ReportFileName"]);
                        rpt.TabName = tabName;
                        reports.Add(rpt);
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
            return reports;
        }
               
        public static DataTable GetReportCommitteeList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvReportGetCommitteeList";
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


        public static DataTable GetReportCampList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvCampGetList";
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
     
   
        
        public static DataTable GetReportCategoryList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvReportGetCategoryList";
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

        public static DataTable GetReportEventList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvReportGetEventList";
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

        public static DataTable GetReportMemberList()
        {
            DBHelper Data = new DBHelper();
            DataTable dt = null;
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvMemberGetLookupListCurrent";
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


        public static void ExecuteReportBuildOldGuardEligibility(string effDate)
        {

            //
            DBHelper Data = new DBHelper();
            
            Data.Command.CommandType = CommandType.StoredProcedure;
            Data.Command.CommandText = "bvReportBuildOldGuardEligibility";
            Data.Command.Parameters.Add(new SqlParameter("@a_dtEffectiveDate", effDate));
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
