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
       public bool SetNewProperty(Property property)
        {
            return SQL_doInsert(property);
        }

        public Property GetProperty(int ID)
        {
            List<Property> list = SQl_getEntryByID<Property>(ID);

            if (list.Count > 1) return null;

            return list[0];
        }

        public List<Property> GetAllProperties()
        {
            return SQL_getAllEntries<Property>();
        }
    }
}
