
using System.Web.Services;
using SmartSockets.Table_Objects;

namespace SmartSockets
{

    public partial class SmartSocketsService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool SetNewDevice(Device device)
        {
            return SQL_doInsert(device);
        }

        [WebMethod]
        public Device GetDevice(int ID)
        {
            Device device = new Device();

            device = (Device)SQl_getEntryByID(ID, device);

            return device;
        }
    }
}