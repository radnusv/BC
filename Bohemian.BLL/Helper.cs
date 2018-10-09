using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Bohemian.BLL
{
    public static class Helper
    {
        public static string AppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];

        }

        #region Export Constants"
        public const string Export_GROVE_MAP_CAMPS = "camps";
        public const string Export_GROVE_MAP_NAMES = "names";
        public const string Export_SQUIRREL_MEMBER = "member";
        public const string Export_SQUIRREL_CAMP = "camp";
        public const string Export_BLUEBOOK_ALPHABETICAL_ROSTER = "AlphabeticalRoster";
        public const string Export_BLUEBOOK_LIST_OF_50 = "ListOf50";
        public const string Export_BLUEBOOK_HONARY_REGULARS = "HonoraryRegulars";
        public const string Export_BLUEBOOK_HONARY_ASSOCIATES = "HonoraryAssociates";
        public const string Export_MAILING_ACTIVE_MEMBERS = "BCIS_Mailing_Active_Members";
        public const string Export_TEXT_OWL_WORKS_SPECIAL_MEMBERS_COMMITTEE_MEMBER_LIST = "BCIS_Committee_Member_To_OwlWorks";
        public const string Export_TEXT_OWL_WORKS_SPECIAL_MEMBERS_ASSOCIATE_LIST = "BCIS_Associate_To_OwlWorks";
        public const string Export_TEXT_OWL_WORKS_SPECIAL_MEMBERS_MEMBER_LIST = "BCIS_Member_To_OwlWorks";
        public const string Export_TEXT_OWL_WORKS_WAITING_LIST_LIST = "BCIS_Waiting_List_To_OwlWorks";

        public const string Export_EXCEL_OWL_WORKS_SPECIAL_MEMBERS = "BCIS_Special_Member_Export";
        #endregion


        #region camp positions
        public const string CAMP_POSITION_NONE = "0";
        public const string CAMP_POSITION_CAPTAIN = "99";
        public const string CAMP_POSITION_CO_CAPTAIN = "98";
        public const string CAMP_POSITION_MAYOR = "97";
        #endregion



        public static void RecordAuditActivity(Bohemian.Model.AuditActivity oEntity)
        {
            DAL.DALUtility.RecordAuditActivity(oEntity);
        }
        
    }

    public enum AuthenticationException
    {
        InvalidLogin=0,
        PasswordExpired=1,
        LoginFailCountExceded
    }




}
