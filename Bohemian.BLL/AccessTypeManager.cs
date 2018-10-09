using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bohemian.Model;

using Bohemian.DAL;

namespace Bohemian.BLL
{
    public class AccessTypeManager
    {
        public List<AccessType> GetAll()
        {
            return AccessTypeDAL.GetAll();
        }

        
        public  bool Add(AccessType oEntity)
        {
            oEntity.ID = DALUtility.GetUniqueId(DALConstants.UniqueIdAccessType);
            return AccessTypeDAL.Add(oEntity);
        }

        public bool Update(AccessType oEntity)
        {
            return AccessTypeDAL.Update(oEntity);
        }

        public bool Delete(AccessType oEntity)
        {
            return AccessTypeDAL.Delete(oEntity);
        }

        public AccessType Get(string ID)
        {
            return AccessTypeDAL.Get(ID);
        }

        public List<Section> GetAllSections()
        {
            return AccessTypeDAL.GetAllSections();
        }
         
        public List<AccessType> GetAccessTypesByUserAndSection(string loginID, string sectionID)
        {
            return AccessTypeDAL.GetAccessTypesByUserAndSection(loginID, sectionID);
        }
    }
}
