using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bohemian.DAL;
using Bohemian.Model;

using System.Configuration ;

namespace Bohemian.BLL
{
    public class UserManager
    {
        public bool Add(User oEntity)
        {
            oEntity.ID = DALUtility.GetUniqueId(DALConstants.UniqueIdLoginUserType);
            return UserDAL.Add(oEntity);
        }
        public bool Update(User oEntity)
        {
            return UserDAL.Update(oEntity);
        }

        public bool Delete(User oEntity)
        {
            return UserDAL.Delete(oEntity);
        }

        public User Get(string Id)
        {
            return UserDAL.Get(Id);
        }

        public List<User> GetAll()
        {
            return UserDAL.GetAll();
        }

        public  bool IsLoginUnique(User oEntity)
        {
            return UserDAL.IsLoginUnique(oEntity);
        }
        
        public bool TestAccessTypeOnWindow(User oEntity, SecureWindow oWindow,AccessType oAccessType)
        {
            return UserDAL.TestAccessTypeOnWindow(oEntity,oWindow,oAccessType);
        }
        public bool TestWindowAccess(User oEntity, Window oWindow)
        {
            return UserDAL.TestWindowAccess(oEntity, oWindow);
        }

        public List<AccessType> GetWindowAccessTypes(User oEntity, SecureWindow oWindow)
        {
            return UserDAL.GetWindowAccessTypes(oEntity, oWindow);
        }

        public bool ChangePassword(User oEntity ){
            return UserDAL.ChangePassword(oEntity);

        }
        public bool UnLockAccount(User oEntity)
        {
            return UserDAL.UnLockAccount(oEntity);
        }
        public User AuthenticateAndCheckPasswordExpiration(User oEntity, string clientComputerName)
        {
            oEntity.AuthenticationToken = ""; // ensure that authentication token is empty before authenticating the user
            oEntity = UserDAL.Authenticate(oEntity, clientComputerName);

            if (string.IsNullOrEmpty(oEntity.AuthenticationToken.Trim()))
            {
                UserDAL.RecordLoginFailure(oEntity, Convert.ToInt32(Helper.AppSettingValue("UserAuthenticationFailureThreshold")), Convert.ToInt32(Helper.AppSettingValue("MinutesUntilReset")));
                throw new UnauthorizedAccessException(AuthenticationException.InvalidLogin.ToString()); // 1 indicates login failed
            }
            else if (DateTime.Compare(oEntity.LastPasswordChange.Value.AddDays(Convert.ToInt32(Helper.AppSettingValue("PasswordExpirationDays"))), DateTime.Now) < 0)
                throw new UnauthorizedAccessException(AuthenticationException.PasswordExpired.ToString()); // indicates password has expired and user should be redirected to password change screen

            


            oEntity.Password = null;
            return oEntity;

        }


        public User Authenticate(User oEntity, string clientComputerName)
        {
            oEntity.AuthenticationToken = ""; // ensure that authentication token is empty before authenticating the user
            oEntity = UserDAL.Authenticate(oEntity, clientComputerName);

            if (string.IsNullOrEmpty(oEntity.AuthenticationToken.Trim()))
            {
                UserDAL.RecordLoginFailure(oEntity, Convert.ToInt32(Helper.AppSettingValue("UserAuthenticationFailureThreshold")), Convert.ToInt32(Helper.AppSettingValue("MinutesUntilReset")));
                throw new UnauthorizedAccessException(AuthenticationException.InvalidLogin.ToString()); // 1 indicates login failed
            }
            
            oEntity.Password = null;
            return oEntity;

        }

        public bool UpdateLoginUserAccess(User oUser, Section oSection, AccessType oAccessType)
        {
            return SecureWindowDAL.UpdateLoginUser(oSection, oUser, oAccessType);
        }


        public List<SecureWindow> GetAccessibleManagerWindows(User oEntity)
        {
            return UserDAL.GetAccessibleManagerWindows(oEntity);
        }

        public bool ResetPassword(string loginName, string password)
        {
            return UserDAL.ResetPassword(loginName, password);
        }

        public List<LoginAccess> GetLoginAccessListByUser(User oUser)
        {
            return UserDAL.GetLoginAccessListByUser(oUser);
        }

    }
}
