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
       /*public bool SetNewDeviceType(DeviceType deviceType, out string ID)
        {
            return SQL_doInsertReturnID(deviceType, out ID);
        }

        public bool RemoveDeviceType(int ID)
        {
            return SQL_deleteEntryByID<DeviceType>(ID);
        }*/

        public bool GetDeviceType(int ID, out DeviceType result)
        {

            result = null;
            List<DeviceType> list = SQl_getEntryByID<DeviceType>(ID);

            if (list.Count != 1) return false;

            result = list[0];

            return true;
        }
        public bool GetAllDeviceTypes(out List<DeviceType> result)
        {
            result = SQL_getAllEntries<DeviceType>();

            if (result.Count > 0) return true;
            return false;
        }
    }
}
