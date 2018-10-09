using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bohemian.DAL;
using Bohemian.Model;

namespace Bohemian.BLL
{
    public class WindowManager
    {
        public List<SecureWindow> GetAll()
        {
            return SecureWindowDAL.GetAll();
        }

        public List<AccessType> GetAssignedAccessTypesByWindow(SecureWindow oSecWin)
        {
            return AccessTypeDAL.GetAssignedAccessTypesByWindow(oSecWin);
        }

        public List<AccessType> GetUnAssignedAccessTypesByWindow(SecureWindow oSecWin)
        {
            return AccessTypeDAL.GetUnAssignedAccessTypesByWindow(oSecWin);
        }

        public List<AccessType> GetAccessTypesByLoginGroupAndWindow(LoginGroup oLoginGroup, SecureWindow oSecWindow)
        {
            return AccessTypeDAL.GetAccessTypesByLoginGroupAndWindow(oLoginGroup, oSecWindow);
        }

        //public List<AccessType> GetAccessTypesByUserAndWindow(User oUser, SecureWindow oSecWindow)
        //{
        //    return AccessTypeDAL.GetAccessTypesByUserAndWindow(oUser, oSecWindow);
        //}


        public bool AddAccessType(SecureWindow oSecWin, AccessType oAccessTyp)
        {
            return SecureWindowDAL.AddAccessType(oSecWin, oAccessTyp);
        }

        public bool DeleteAccessType(SecureWindow oSecWin, AccessType oAccessTyp)
        {
            return SecureWindowDAL.DeleteAccessType(oSecWin, oAccessTyp);
        }

        public bool Add(SecureWindow oEntity)
        {
            oEntity.ID = DALUtility.GetUniqueId(DALConstants.UniqueIdSecureWindow);
            return SecureWindowDAL.Add(oEntity);
        }

        public bool Update(SecureWindow oEntity)
        {
            return SecureWindowDAL.Update(oEntity);
        }

        

        public bool Delete(SecureWindow oEntity)
        {
            return SecureWindowDAL.Delete(oEntity);
        }

        public SecureWindow Get(string ID)
        {
            return SecureWindowDAL.Get(ID);
        }

    }
}
