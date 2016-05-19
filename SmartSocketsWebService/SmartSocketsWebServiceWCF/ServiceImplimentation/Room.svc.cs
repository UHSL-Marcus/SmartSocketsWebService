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
       public bool SetNewRoom(Room room, out string ID)
        {
            return SQL_doInsertReturnID(room, out ID);
        }

        public bool UpdateRoom(Room room)
        {
            return SQL_doUpdate(room);
        }

        public bool RemoveRoom(int ID)
        {
            return SQL_deleteEntryByID<Room>(ID);
        }

        public bool GetRoom(int ID, out Room result) {

            result = null;
            List<Room> list = SQl_getEntryByID<Room>(ID);

            if (list.Count != 1) return false;

            result = list[0];

            return true;
        }

        public bool GetAllRooms(out List<Room> result)
        {
            result = SQL_getAllEntries<Room>();

            if (result.Count > 0) return true;
            return false;
        }

    }
}
