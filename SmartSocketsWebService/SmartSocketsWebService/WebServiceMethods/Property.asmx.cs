
using System.Web.Services;
using SmartSockets.Table_Objects;

namespace SmartSockets
{

    public partial class SmartSocketsService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool SetNewProperty(Property property)
        {
            return SQL_doInsert(property);
        }

        [WebMethod]
        public Property GetProperty(int ID)
        {
            Property property = new Property();

            property = (Property)SQl_getEntryByID(ID, property);

            return property;
        }
    }
}