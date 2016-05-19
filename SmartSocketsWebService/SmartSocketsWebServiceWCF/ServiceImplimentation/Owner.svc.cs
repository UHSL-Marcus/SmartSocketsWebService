using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartSocketsWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmartSocketsWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmartSocketsWebService.svc or SmartSocketsWebService.svc.cs at the Solution Explorer and start debugging.
    public partial class SmartSocketsWebService : ISmartSocketsWebService
    {
       public bool SetNewOwner(Owner owner, out string ID)
        {
            return SQL_doInsertReturnID(owner, out ID);
        }

        public bool UpdateOwner(Owner owner)
        {
            return SQL_doUpdate(owner);
        }

        public bool RemoveOwner(int ID)
        {
            return SQL_deleteEntryByID<Owner>(ID);
        }

        public bool GetOwner(int ID, out Owner result)
        {

            result = null;
            List<Owner> list = SQl_getEntryByID<Owner>(ID);

            if (list.Count != 1) return false;

            result = list[0];

            return true;
        }
        public bool GetAllOwners(out List<Owner> result)
        {
            result = SQL_getAllEntries<Owner>();

            if (result.Count > 0) return true;
            return false;
        }
    }
}
