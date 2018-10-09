using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bohemian.Model;

using Bohemian.DAL;

namespace Bohemian.BLL
{
    public class LoginGroupManager
    {
        public bool Add(LoginGroup oEntity)
        {
            oEntity.ID = DALUtility.GetUniqueId(DALConstants.UniqueIdLoginGroupType);
            return LoginGroupDAL.Add(oEntity);
        }
        public bool Update(LoginGroup oEntity)
        {
            return LoginGroupDAL.Update(oEntity);
        }
        public bool Delete(LoginGroup oEntity)
        {
            return LoginGroupDAL.Delete(oEntity);
        }
        public LoginGroup Get(string Id)
        {
            return LoginGroupDAL.Get(Id);
        }
        public List<LoginGroup> GetAll()
        {
            return LoginGroupDAL.GetAll();
        }

       public bool UpdateLoginGroupAccess(LoginGroup oGroup, SecureWindow oWindow, AccessType oAccessType)
        {
            return SecureWindowDAL.UpdateLoginGroup(oWindow, oGroup, oAccessType);
        }
    

    }
}
