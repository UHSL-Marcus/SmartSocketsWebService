using SQLControlsLib;
using System.Collections.Generic;

namespace SmartSocketsWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmartSocketsWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmartSocketsWebService.svc or SmartSocketsWebService.svc.cs at the Solution Explorer and start debugging.
    public partial class SmartSocketsWebService : ISmartSocketsWebService
    {
       /*public bool SetNewDeviceType(DeviceType deviceType, out int? ID)
        {
            return Set.doInsertReturnID(deviceType, out ID);
        }

        public bool RemoveDeviceType(int ID)
        {
            return Delete.doDeleteEntryByID<DeviceType, int>(ID)
        }*/

        public bool GetDeviceType(int ID, out DeviceType result)
        {

            bool success = false;
            result = null;
            List<DeviceType> list;
            if (Get.doSelectByID(ID, out list))
                if (list.Count == 1) { result = list[0]; success = true; }

            return success;
        }
        public bool GetAllDeviceTypes(out List<DeviceType> result)
        {
            return Get.doSelectAll(out result);
        }
    }
}
