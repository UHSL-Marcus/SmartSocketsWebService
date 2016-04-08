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
        public bool SetNewSocket(Socket socket)
        {
            return doSqlInsert(socket);
        }

        [WebMethod]
        public Socket GetSocket(int ID)
        {
            Socket socket = new Socket();

            socket = (Socket)getEntryByID(ID, socket);

            return socket;
        }
    }
}