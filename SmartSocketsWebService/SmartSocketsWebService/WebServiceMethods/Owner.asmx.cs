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
        public bool SetNewOwner(Owner owner)
        {
            return doSqlInsert(owner);
        }

        [WebMethod]
        public Owner GetOwner(int ID)
        {
            Owner owner = new Owner();

            owner = (Owner)getEntryByID(ID, owner);

            return owner;
        }
    }
}
