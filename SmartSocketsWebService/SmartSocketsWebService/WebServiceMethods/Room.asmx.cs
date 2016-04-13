
using System.Web.Services;
using SmartSockets.Table_Objects;

namespace SmartSockets
{

    public partial class SmartSocketsService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool SetNewRoom(Room room)
        {
            return SQL_doInsert(room);
        }

        [WebMethod]
        public Room GetRoom(int ID)
        {
            Room room = new Room();

            room = (Room)SQl_getEntryByID(ID, room);

            return room;
        }
    }
}