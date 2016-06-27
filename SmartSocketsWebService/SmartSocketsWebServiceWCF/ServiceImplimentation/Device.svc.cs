using SQLControlsLib;
using System.Collections.Generic;

namespace SmartSocketsWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmartSocketsWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmartSocketsWebService.svc or SmartSocketsWebService.svc.cs at the Solution Explorer and start debugging.
    public partial class SmartSocketsWebService : ISmartSocketsWebService
    {
        public bool SetNewDevice(Device device, out int? ID)
        {
            return Set.doInsertReturnID(device, out ID);
        }

        public bool UpdateDevice(Device device)
        {
            return Update.doUpdateByID(device);
        }

        public bool RemoveDevice(int ID)
        {
            return Delete.doDeleteEntryByID<Device, int>(ID);
        }

        public bool GetDevice(int ID, out Device result)
        {
            bool success = false;
            result = null;
            List<Device> list; 
            if (Get.doSelectByID(ID, out list))
                if (list.Count == 1) { result = list[0]; success = true; }

            return success;
        }
        public bool GetAllDevices(out List<Device> result)
        {
            return Get.doSelectAll(out result);
        }
    }
}
