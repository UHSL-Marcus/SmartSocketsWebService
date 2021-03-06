﻿
using System.Web.Services;
using SmartSockets.Table_Objects;

namespace SmartSockets
{
    
    public partial class SmartSocketsService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool SetNewOwner(Owner owner)
        {
            return SQL_doInsert(owner);
        }

        [WebMethod]
        public Owner GetOwner(int ID)
        {
            Owner owner = new Owner();

            owner = (Owner)SQl_getEntryByID(ID, owner);

            return owner;
        }
    }
}
