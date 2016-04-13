
using System.Web.Services;
using SmartSockets.Table_Objects;

namespace SmartSockets
{

    public partial class SmartSocketsService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool SetNewDeviceType(DeviceType deviceType)
        {
            return SQL_doInsert(deviceType);
        }

        [WebMethod]
        public DeviceType GetDeviceType(int ID)
        {
            DeviceType deviceType = new DeviceType();

            deviceType = (DeviceType)SQl_getEntryByID(ID, deviceType);

            return deviceType;
        }
    }
}
