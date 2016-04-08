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
        public bool SetNewProperty(Property property)
        {
            return doSqlInsert(property);
        }

        [WebMethod]
        public Property GetProperty(int ID)
        {
            Property property = new Property();

            property = (Property)getEntryByID(ID, property);

            return property;
        }
    }
}