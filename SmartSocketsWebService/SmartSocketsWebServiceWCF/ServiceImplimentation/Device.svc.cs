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
       public bool SetNewDevice(Device device)
        {
            return SQL_doInsert(device);
        }

        public Device GetDevice(int ID)
        {
            List<Device> list = SQl_getEntryByID<Device>(ID);

            if (list.Count > 1) return null;

            return list[0];
        }

        public List<Device> GetAllDevices()
        {
            return SQL_getAllEntries<Device>();
            
        }
    }
}
