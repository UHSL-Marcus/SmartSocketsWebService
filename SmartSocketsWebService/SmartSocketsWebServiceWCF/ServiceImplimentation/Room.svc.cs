using SQLControlsLib;
using System.Collections.Generic;

namespace SmartSocketsWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmartSocketsWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmartSocketsWebService.svc or SmartSocketsWebService.svc.cs at the Solution Explorer and start debugging.
    public partial class SmartSocketsWebService : ISmartSocketsWebService
    {
        public bool SetNewRoom(Room room, out int? ID)
        {    
            return Set.doInsertReturnID(room, out ID);
        }

        public bool UpdateRoom(Room room)
        {
            return Update.doUpdateByID(room);
        }

        public bool RemoveRoom(int ID)
        {
            return Delete.doDeleteEntryByID<Room, int>(ID);
        }

        public bool GetRoom(int ID, out Room result) {

            bool success = false;
            result = null;
            List<Room> list;
            if (Get.doSelectByID(ID, out list))
                if (list.Count == 1) { result = list[0]; success = true; }

            return success;
        }

        public bool GetAllRooms(out List<Room> result)
        {
            return Get.doSelectAll(out result);
        }

    }
}
