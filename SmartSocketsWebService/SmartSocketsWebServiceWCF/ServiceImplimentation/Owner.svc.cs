using SQLControlsLib;
using System.Collections.Generic;

namespace SmartSocketsWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmartSocketsWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmartSocketsWebService.svc or SmartSocketsWebService.svc.cs at the Solution Explorer and start debugging.
    public partial class SmartSocketsWebService : ISmartSocketsWebService
    {
       public bool SetNewOwner(Owner owner, out int? ID)
        {
            return Set.doInsertReturnID(owner, out ID);
        }

        public bool UpdateOwner(Owner owner)
        {
            return Update.doUpdateByID(owner);
        }

        public bool RemoveOwner(int ID)
        {
            return Delete.doDeleteEntryByID<Owner, int>(ID);
        }

        public bool GetOwner(int ID, out Owner result)
        {

            bool success = false;
            result = null;
            List<Owner> list;
            if (Get.doSelectByID(ID, out list))
                if (list.Count == 1) { result = list[0]; success = true; }

            return success;
        }
        public bool GetAllOwners(out List<Owner> result)
        {
            return Get.doSelectAll(out result);
        }
    }
}
