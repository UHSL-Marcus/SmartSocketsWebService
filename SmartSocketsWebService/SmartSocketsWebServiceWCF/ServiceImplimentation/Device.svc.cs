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
       public bool SetNewDevice(Device device, out string ID)
        {
            return SQL_doInsertReturnID(device, out ID);
        }

        public bool UpdateDevice(Device device)
        {
            return SQL_doUpdate(device);
        }

        public bool RemoveDevice(int ID)
        {
            return SQL_deleteEntryByID<Device>(ID);
        }

        public bool GetDevice(int ID, out Device result)
        {

            result = null;
            List<Device> list = SQl_getEntryByID<Device>(ID);

            if (list.Count != 1) return false;

            result = list[0];

            return true;
        }
        public bool GetAllDevices(out List<Device> result)
        {
            result = SQL_getAllEntries<Device>();

            if (result.Count > 0) return true;
            return false;
        }
    }
}
