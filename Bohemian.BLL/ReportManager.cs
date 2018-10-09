using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using Bohemian.Model;
using Bohemian.DAL;


namespace Bohemian.BLL
{
    public class ReportManager
    {
        public List<Report> GetAllByTab(string tabName)
        {
            return ReportDAL.GetAllByTab(tabName);
        }

        public Report Get(string Id)
        {
            return ReportDAL.Get(Id);
        }

        public DataTable GetReportCommitteeList()
        {
            return ReportDAL.GetReportCommitteeList();
        }

        public DataTable GetReportCampList()
        {
            return ReportDAL.GetReportCampList();
        }

        public DataTable GetReportCategoryList()
        {
            return ReportDAL.GetReportCategoryList();
        }
        public DataTable GetReportEventList()
        {
            return ReportDAL.GetReportEventList();
        }
        public DataTable GetReportMemberList()
        {
            return ReportDAL.GetReportMemberList();
        }

        public List<Member> GetMemberList()
        {
            List<Member> members = new List<Member>();

            DataTable dt = ReportDAL.GetReportMemberList();
            
            foreach (DataRow dr in dt.Rows)
            {
                Member m = new Member();
                m.ID=dr["MemberID"].ToString();
                m.ReportName=  dr["ReportName"].ToString();
                m.Number=  dr["MemberNumber"].ToString();
                m.CategoryName=  dr["MembershipCategoryName"].ToString();
                members.Add(m);            
            }
            return members;
        }

        public void ExecuteReportBuildOldGuardEligibility(string effDate)
        {
            ReportDAL.ExecuteReportBuildOldGuardEligibility(effDate);
        }
        
    }
}
