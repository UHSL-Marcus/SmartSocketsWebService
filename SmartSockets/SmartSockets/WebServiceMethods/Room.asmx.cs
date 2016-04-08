using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SmartSockets.Table_Objects;

namespace SmartSockets
{

    public partial class SmartSocketsService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool SetNewRoom(Room room)
        {
            return doSqlInsert(room);
        }

        [WebMethod]
        public Room GetRoom(int ID)
        {
            Room room = new Room();

            room = (Room)getEntryByID(ID, room);

            return room;
        }
    }
}