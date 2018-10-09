using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bohemian.DAL;
using System.Data;
using System.Net;


namespace Bohemian.BLL
{
    public class ExportManager
    {
        protected void OwlWorksUploadMember(string a_strOwlWorksExportID, string a_strOwlWorksExportDirectory, DateTime a_dtExportTimestamp, int a_iRecordCount)
        {
            try
            {
                // Create a new WebClient instance.
                WebClient v_webClient = new WebClient();

                // Setup WebClient event handlers
                //v_webClient.UploadFileCompleted += new UploadFileCompletedEventHandler(this.OwlWorksUploadMemberCompleted);
                //v_webClient.UploadProgressChanged += new UploadProgressChangedEventHandler(this.OwlWorksUploadMemberProgressChanged);

                // Call the next function in the upload process
                //bvBCISExportTools.OwlWorksUploadMember(a_strOwlWorksExportID, a_strOwlWorksExportDirectory, a_dtExportTimestamp, a_iRecordCount, ref v_webClient);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        


        public string GetExportData(string exportType)
        {
            string exportData = "";
            
                switch (exportType)
                {
                    case Helper.Export_GROVE_MAP_CAMPS:
                        exportData = this.GroveMapCamps();
                        break;
                    case Helper.Export_GROVE_MAP_NAMES:
                        exportData = this.GroveMapNameList();
                        break;
                    case Helper.Export_SQUIRREL_MEMBER:
                        exportData = this.SquirrelMemberList();
                        break;
                    case Helper.Export_SQUIRREL_CAMP:
                        exportData = this.SquirrelCampList();
                        break;
                    case Helper.Export_BLUEBOOK_ALPHABETICAL_ROSTER:
                        exportData = this.BlueBookRosterList();
                        break;
                    case Helper.Export_BLUEBOOK_LIST_OF_50:
                        exportData = this.BlueBookListOf50List();
                        break;
                    case Helper.Export_BLUEBOOK_HONARY_REGULARS:
                        exportData = this.BlueBookHonoraryRegularList();
                        break;
                    case Helper.Export_BLUEBOOK_HONARY_ASSOCIATES:
                        exportData = this.BlueBookHonoraryAssociateList();
                        break;
                    case Helper.Export_TEXT_OWL_WORKS_SPECIAL_MEMBERS_COMMITTEE_MEMBER_LIST:
                        exportData = this.OwlWorksCommitteeMemberList();
                        break;
                    case Helper.Export_TEXT_OWL_WORKS_SPECIAL_MEMBERS_ASSOCIATE_LIST:
                        exportData = this.OwlWorksAssociateList();
                        break;
                    case Helper.Export_TEXT_OWL_WORKS_SPECIAL_MEMBERS_MEMBER_LIST:
                        exportData = this.OwlWorksMemberList();
                        break;
                    case Helper.Export_TEXT_OWL_WORKS_WAITING_LIST_LIST:
                        exportData = this.OwlWorksWaitingListList();
                        break;
                    case Helper.Export_MAILING_ACTIVE_MEMBERS:
                        exportData = "NOT IMPLEMENTED";
                        break;
                }
            
            return exportData;
        }

        public DataTable MailingActiveMemeberList()
        {
            return ExportDataDAL.MailingActiveMemeberList();
        }

        public DataTable MailingPostalMemeberList()
        {
            return ExportDataDAL.MailingPostalMemeberList();
        }

        public DataTable SpecialMember_MemberList()
        {
            return ExportDataDAL.SpecialMember_MemberList();
        }

        public DataTable SpecialMember_WaitingList()
        {
            return ExportDataDAL.SpecialMember_WaitingList();
        }
        
        public DataTable SpecialMember_CommitteeMemberList()
        {
            return ExportDataDAL.SpecialMember_CommitteeMemberList();
        }

        public DataTable SpecialMember_AssociateGroupList()
        {
            return ExportDataDAL.SpecialMember_AssociateGroupList();
        }

        public DataTable SpecialMember_AssociateGroupMembers(string associateGroupId)
        {
            return ExportDataDAL.SpecialMember_AssociateGroupMembers(associateGroupId);
        }

        

        private string GroveMapCamps()
        {
            StringBuilder sb = new StringBuilder();
            ExportManager oEM = new ExportManager();
            DataTable dt = ExportDataDAL.GroveMapCamps();
            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string strOutput = "";
                    int coCaptainIndex = 0;
                    int coCaptainCount = 0;
                    bool isFirstMember = true;

                    string campId = Convert.ToString(dr["CampID"]);
                    string campName = Convert.ToString(dr["CampName"]);

                    sb.AppendLine(campName);

                    DataTable dtCampMembers = this.GroveMapCampMemberList(campId);
                    if (dtCampMembers != null && dtCampMembers.Rows != null && dtCampMembers.Rows.Count > 0)
                    {
                        foreach (DataRow drMem in dtCampMembers.Rows)
                        {
                            if (Convert.ToString(drMem["CampPositionID"]) == Helper.CAMP_POSITION_CO_CAPTAIN)
                                coCaptainCount++;
                        }


                        foreach (DataRow drMem in dtCampMembers.Rows)
                        {
                            string campPositionId = Convert.ToString(drMem["CampPositionID"]);
                            if (campPositionId == Helper.CAMP_POSITION_CAPTAIN)
                            {
                                strOutput = Convert.ToString(drMem["Prefix"]) == "" ? "" : Convert.ToString(drMem["Prefix"]);
                                strOutput += Convert.ToString(drMem["FirstName"]) == "" ? "" : " " + Convert.ToString(drMem["FirstName"]);
                                strOutput += Convert.ToString(drMem["MiddleName"]) == "" ? "" : " " + Convert.ToString(drMem["MiddleName"]);
                                strOutput += Convert.ToString(drMem["LastName"]) == "" ? "" : " " + Convert.ToString(drMem["LastName"]);
                                strOutput += Convert.ToString(drMem["Suffix"]) == "" ? "" : " " + Convert.ToString(drMem["Suffix"]);
                                strOutput += ", Captain, and Messrs. ";

                            }
                            if (campPositionId == Helper.CAMP_POSITION_CO_CAPTAIN)
                            {
                                if (coCaptainIndex == 0)
                                    strOutput = "Messrs. ";
                                else
                                    strOutput += " and ";

                                strOutput += Convert.ToString(drMem["FirstName"]) == "" ? "" : " " + Convert.ToString(drMem["FirstName"]);
                                strOutput += Convert.ToString(drMem["MiddleName"]) == "" ? "" : " " + Convert.ToString(drMem["MiddleName"]);
                                strOutput += Convert.ToString(drMem["LastName"]) == "" ? "" : " " + Convert.ToString(drMem["LastName"]);
                                strOutput += Convert.ToString(drMem["Suffix"]) == "" ? "" : " " + Convert.ToString(drMem["Suffix"]);

                                coCaptainIndex++;

                                if (coCaptainIndex == coCaptainCount)
                                    strOutput += ", Co-Captains, and Messrs. ";
                            }

                            if (campPositionId != Helper.CAMP_POSITION_CAPTAIN && campPositionId != Helper.CAMP_POSITION_CO_CAPTAIN)
                            {
                                if (isFirstMember == false)
                                    strOutput += ", ";

                                strOutput += Convert.ToString(drMem["FirstName"]) == "" ? "" : " " + Convert.ToString(drMem["FirstName"]);
                                strOutput += Convert.ToString(drMem["MiddleName"]) == "" ? "" : " " + Convert.ToString(drMem["MiddleName"]);
                                strOutput += Convert.ToString(drMem["LastName"]) == "" ? "" : " " + Convert.ToString(drMem["LastName"]);
                                strOutput += Convert.ToString(drMem["Suffix"]) == "" ? "" : " " + Convert.ToString(drMem["Suffix"]);

                                isFirstMember = false;

                            }
                        }
                        sb.AppendLine(strOutput);
                    }
                    sb.AppendLine("");
                    sb.AppendLine("");
                }
            }
            return sb.ToString();
        }

        private DataTable GroveMapCampMemberList(string campId)
        {
            return ExportDataDAL.GroveMapCampMemberList(campId);
        }

        private string GroveMapNameList()
        {
            StringBuilder sb = new StringBuilder();
            ExportManager oEM = new ExportManager();
            DataTable dt =  ExportDataDAL.GroveMapNameList();

           

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string strOutput = "";
                        strOutput +=  Convert.ToString(dr["LastName"]) == "" ? "" : Convert.ToString(dr["LastName"]);
                        strOutput += Convert.ToString(dr["FirstName"]) == "" ? "" : " " + Convert.ToString(dr["FirstName"]);
                        strOutput +=  Convert.ToString(dr["MiddleName"]) == "" ? "" : " " + Convert.ToString(dr["MiddleName"]);
                        strOutput += Convert.ToString(dr["Suffix"]) == "" ? "" : " " + Convert.ToString(dr["Suffix"]);
                        strOutput +=  Convert.ToString(dr["CampName"]) == "" ? "" : (((char)9).ToString() + Convert.ToString(dr["CampName"]));

                        sb.AppendLine(strOutput);
                    }
                }

           

            return sb.ToString();

       
        }

        private string SquirrelMemberList()
        {
            StringBuilder sb = new StringBuilder();
            ExportManager oEM = new ExportManager();
            DataTable dt = ExportDataDAL.SquirrelMemberList();
            
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string strOutput = "";

                        string memberNumber =  Convert.ToString(dr["MemberNumber"]) == "" ? "" : (Convert.ToString(dr["MemberNumber"]) + new string(' ', 8));
                        string lastNameSuffix = (Convert.ToString(dr["LastName"]) == "" ? "" : Convert.ToString(dr["LastName"])) + " " + ( Convert.ToString(dr["Suffix"]) == "" ? "" : Convert.ToString(dr["Suffix"])) + new string(' ', 15);
                        string firstMiddle = ( Convert.ToString(dr["FirstName"]) == "" ? "" : Convert.ToString(dr["FirstName"])) + " " + ( Convert.ToString(dr["MiddleName"]) == "" ? "" : Convert.ToString(dr["MiddleName"])) + new string(' ', 20);

                        strOutput = memberNumber.Substring(0, 8) + "0";
                        strOutput += lastNameSuffix.Substring(0, 15);
                        strOutput += firstMiddle.Substring(0, 20) + "A0" + new string(' ', 59) + "0";

                        sb.AppendLine(strOutput);
                    }
                }
            

            return sb.ToString();
        }

        private  string SquirrelCampList()
        {

            StringBuilder sb = new StringBuilder();
            ExportManager oEM = new ExportManager();
            DataTable dt = ExportDataDAL.SquirrelCampList();
           
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string strOutput = "";

                        string campNumber = "000" + Convert.ToString(dr["CampID"]);
                        campNumber = campNumber.Substring(campNumber.Length - 3, 3) + new string(' ', 8);
                        string campName = Convert.ToString(dr["CampName"]) + new string(' ', 35);
                        strOutput = campNumber.Substring(0, 8) + "0";
                        strOutput += campName.Substring(0, 35) + "A0" + new string(' ', 59) + "0";

                        sb.AppendLine(strOutput);
                    }


                }

            
            return sb.ToString();
         
        }
        
        private string BlueBookRosterList()
        {
            StringBuilder sb = new StringBuilder();
            ExportManager oEM = new ExportManager();
            DataTable dt = ExportDataDAL.BlueBookRosterList();
           
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string strOutput = "";
                        DateTime dtMemberDate;
                        string memberDate = "";

                        if (DateTime.TryParse(Convert.ToString(dr["MemberDate"]), out dtMemberDate) == true)
                            memberDate = dtMemberDate.ToString("MMMM d, yyyy");

                        strOutput = (Convert.ToString(dr["OldGuard"]) == "Y" ? "*" : "");
                        strOutput += "\t" + Convert.ToString(dr["ReportName"]).Trim();
                        strOutput += Convert.ToString(dr["MembershipCategoryID"]) == "9" ? " " + Convert.ToString(dr["MilitaryServiceText"]) : (" " + Convert.ToString(dr["MembershipCategoryText"])) + (Convert.ToString(dr["MemberText"]) == "" ? "" : " " + Convert.ToString(dr["MemberText"]));
                        strOutput += "\t" + memberDate;
                        strOutput += "\t" + (Convert.ToString(dr["TalentText"]) == "" ? "" : Convert.ToString(dr["TalentText"])); // dum line of code why do we need to check? - Copied from BCIS
                        strOutput += "\t";

                        sb.AppendLine(strOutput);

                    }


                }

            
            return sb.ToString();           
        }
        
        private string BlueBookListOf50List()
        {

            StringBuilder sb = new StringBuilder();
            ExportManager oEM = new ExportManager();
            DataTable dt = ExportDataDAL.BlueBookListOf50List();
             if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string strOutput = "";
                        DateTime dtMemberDate;
                        string memberDate = "";

                        if (DateTime.TryParse(Convert.ToString(dr["MemberDate"]), out dtMemberDate) == true)
                            memberDate = dtMemberDate.ToString("MMMM d, yyyy");

                        strOutput = "/1" + (Convert.ToString(dr["OldGuard"]) == "Y" ? "*" : "");

                        strOutput += "/2" + Convert.ToString(dr["ReportName"]).Trim();
                        
                        strOutput += "/3" + memberDate;
                        strOutput += "/4" + (Convert.ToString(dr["TalentText"]) == "" ? "" : Convert.ToString(dr["TalentText"])); 
                        strOutput += "/5";

                        sb.AppendLine(strOutput);

                    }


                }

            
            return sb.ToString();            
        }
        private string BlueBookHonoraryRegularList()
        {
            StringBuilder sb = new StringBuilder();
            ExportManager oEM = new ExportManager();
            DataTable dt = ExportDataDAL.BlueBookHonoraryRegularList();
           
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string strOutput = "";
                        DateTime dtMemberDate;
                        string memberDate = "";

                        if (DateTime.TryParse(Convert.ToString(dr["MemberDate"]), out dtMemberDate) == true)
                            memberDate = dtMemberDate.ToString("MMMM d, yyyy");

                        strOutput = "/1" + (Convert.ToString(dr["OldGuard"]) == "Y" ? "*" : "");

                        strOutput += "/2" + Convert.ToString(dr["ReportName"]).Trim();

                        strOutput += "/3" + memberDate;
                        strOutput += "/4" + (Convert.ToString(dr["TalentText"]) == "" ? "" : Convert.ToString(dr["TalentText"]));
                        strOutput += "/5";

                        sb.AppendLine(strOutput);

                    }
                }

            
            return sb.ToString();
        }

        private string BlueBookHonoraryAssociateList()
        {
            StringBuilder sb = new StringBuilder();
            ExportManager oEM = new ExportManager();
            DataTable dt = ExportDataDAL.BlueBookHonoraryAssociateList();
             if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string strOutput = "";
                        DateTime dtMemberDate;
                        string memberDate = "";

                        if (DateTime.TryParse(Convert.ToString(dr["MemberDate"]), out dtMemberDate) == true)
                            memberDate = dtMemberDate.ToString("MMMM d, yyyy");

                        strOutput = "/1" + (Convert.ToString(dr["OldGuard"]) == "Y" ? "*" : "");

                        strOutput += "/2" + Convert.ToString(dr["ReportName"]).Trim();

                        strOutput += "/3" + memberDate;
                        strOutput += "/4" + (Convert.ToString(dr["TalentText"]) == "" ? "" : Convert.ToString(dr["TalentText"]));
                        strOutput += "/5";

                        sb.AppendLine(strOutput);

                    }


                }

           
            return sb.ToString();
        }
        
        private string OwlWorksCommitteeMemberList()
        {

            StringBuilder sb = new StringBuilder();
            ExportManager oEM = new ExportManager();
            DataTable dt =ExportDataDAL.OwlWorksCommitteeMemberList();
            
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    string strOutput = "";
                    string strData = "";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.Ordinal > 0)
                            strOutput += "\t";

                        strOutput += dc.Caption;

                    }

                    sb.AppendLine(strOutput);

                    foreach (DataRow dr in dt.Rows)
                    {
                        strOutput = "";
                        foreach (DataColumn dc1 in dt.Columns)
                        {
                            strData = Convert.ToString(dr[dc1.Ordinal]);

                            if (dc1.Ordinal > 0)
                                strOutput += "\t";

                            strOutput += strData;
                        }
                        sb.AppendLine(strOutput);
                    }
                }

           
            return sb.ToString();

        }

        private string OwlWorksAssociateList()
        {
            StringBuilder sb = new StringBuilder();
            ExportManager oEM = new ExportManager();
            DataTable dt = ExportDataDAL.OwlWorksAssociateList();
           
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    string strOutput = "";
                    string strData = "";
                    DateTime datDate = new DateTime();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.Ordinal > 0)
                            strOutput += "\t";

                        strOutput += dc.Caption;

                    }

                    sb.AppendLine(strOutput);

                    foreach (DataRow dr in dt.Rows)
                    {
                        strOutput = "";
                        foreach (DataColumn dc1 in dt.Columns)
                        {
                            strData = Convert.ToString(dr[dc1.Ordinal]);

                            switch (dc1.Caption)
                            {
                                case "ProbationaryDate":
                                case "HonoraryDate":
                                case "WhiteAppApprovedDate":
                                case "WhiteAppReceivedDate":
                                    {
                                        if (DateTime.TryParse(strData, out datDate) == true)
                                            strData = datDate.ToString("MM/dd/yyyy");
                                        else
                                            strData = "";
                                        break;
                                    }
                                default:
                                    break;

                            }

                            if (dc1.Ordinal > 0)
                                strOutput += "\t";

                            strOutput += strData;
                        }
                        sb.AppendLine(strOutput);
                    }
                }

            
            return sb.ToString();

            
        }

        private string OwlWorksMemberList()
        {

            StringBuilder sb = new StringBuilder();
            ExportManager oEM = new ExportManager();
            DataTable dt = ExportDataDAL.OwlWorksMemberList ();
           
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    string strOutput = "";
                    string strData = "";
                    DateTime datDate = new DateTime();
                    string memberId = "";
                     

                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.Ordinal > 0)
                            strOutput += "\t";

                        strOutput += dc.Caption;

                    }

                    sb.AppendLine(strOutput);


                    foreach (DataRow dr in dt.Rows)
                    {
                        strOutput = "";
                        foreach (DataColumn dc in dt.Columns)
                        {
                            strData = Convert.ToString(dr[dc.Ordinal]);
                            switch (dc.Caption)
                            {
                                case "BirthDate":
                                case "MemberDate":
                                case "ProbationaryDate":
                                case "ApplicationDate":
                                case "EWLDate":
                                case "WhiteAppApprovedDate":
                                case "WhiteAppReceivedDate":
                                    {
                                        if (DateTime.TryParse(strData, out datDate) == true)
                                            strData = datDate.ToString("MM/dd/yyyy");
                                        else
                                            strData = "";

                                        break;
                                    }
                                case "YearsInClub":
                                    memberId = Convert.ToString(dr["MemberID"]);
                                    strData = oEM.GetYearsInClub(memberId).ToString();
                                    break;
                                default:
                                    break;
                            }
                            if (dc.Ordinal > 0)
                                strOutput += "\t";

                            strOutput += strData;

                        }
                        sb.AppendLine(strOutput);

                    }

                }
            
            return sb.ToString();

           
        }

        private string OwlWorksWaitingListList()
        {

            StringBuilder sb = new StringBuilder();
            ExportManager oEM = new ExportManager();
            DataTable dt = ExportDataDAL.OwlWorksWaitingListList();

            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                string strOutput = "";
                string strData = "";
                DateTime datDate = new DateTime();
                string memberId = "";
           

                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.Ordinal > 0)
                        strOutput += "\t";

                    strOutput += dc.Caption;

                }

                sb.AppendLine(strOutput);


                foreach (DataRow dr in dt.Rows)
                {
                    strOutput = "";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        strData = Convert.ToString(dr[dc.Ordinal]);
                        switch (dc.Caption)
                        {
                            case "BirthDate":
                            case "MemberDate":
                            case "ProbationaryDate":
                            case "ApplicationDate":
                            case "EWLDate":
                            case "WhiteAppApprovedDate":
                            case "WhiteAppReceivedDate":
                                {
                                    if (DateTime.TryParse(strData, out datDate) == true)
                                        strData = datDate.ToString("MM/dd/yyyy");
                                    else
                                        strData = "";

                                    break;
                                }
                            case "YearsInClub":
                                memberId = Convert.ToString(dr["MemberID"]);
                                strData = oEM.GetYearsInClub(memberId).ToString();
                                break;
                            default:
                                break;
                        }
                        if (dc.Ordinal > 0)
                            strOutput += "\t";

                        strOutput += strData;

                    }
                    sb.AppendLine(strOutput);

                }

            }

            return sb.ToString(); 
        }

        public  int GetYearsInClub(string memberId)
        {
            int daysInClub = DALUtility.GetMemberDaysInClub(memberId);
            int yearsInClub=0;
            int out_DaysInClub;
            if (int.TryParse(Convert.ToString(daysInClub), out out_DaysInClub) == false)
                out_DaysInClub = 0;

            yearsInClub = (int)(out_DaysInClub / 365.25);


            return yearsInClub;
        }
        
    }
}
