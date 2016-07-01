using SQLControlsLib;
using System.Collections.Generic;

namespace SmartSocketsWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmartSocketsWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmartSocketsWebService.svc or SmartSocketsWebService.svc.cs at the Solution Explorer and start debugging.
    public partial class SmartSocketsWebService : ISmartSocketsWebService
    {
       public bool SetNewProperty(Property property, out int? ID)
        {
            return Set.doInsertReturnID(property, out ID);
        }

        public bool UpdateProperty(Property property)
        {
            return Update.doUpdateByID(property);
        }

        public bool RemoveProperty(int ID)
        {
            return Delete.doDeleteEntryByID<Property, int>(ID);
        }

        public bool GetProperty(int ID, out Property result)
        {

            bool success = false;
            result = null;
            List<Property> list;
            if (Get.doSelectByID(ID, out list))
                if (list.Count == 1) { result = list[0]; success = true; }

            return success;
        }

        public bool GetPropertyExists(int ID)
        {
            return Get.doEntryExistsByID<Property, int>(ID);
        }
        public bool GetAllProperties(out List<Property> result)
        {
            return Get.doSelectAll(out result);
        }
    }
}
