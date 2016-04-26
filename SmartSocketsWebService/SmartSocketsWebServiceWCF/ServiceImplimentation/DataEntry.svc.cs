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
        public bool AddDataEntry(DataEntry entry, string deviceID)
        {
            return MongoDB_InsertDocument(entry, deviceID, string.Format("{0:dd/MM/YYYY-HH:mm:ss}", entry.TimeStamp));
        }

        public DataEntry GetDataEntry(DateTime TimeStamp, string deviceID)
        {
            List<DataEntry> list = MongoDB_GetDocumentFromID<DataEntry>(deviceID, string.Format("{0:dd/MM/YYYY-HH:mm:ss}"));

            if (list.Count > 1) return null;

            return list[0];
        }


        public List<DataEntry> GetAllEntires(string deviceID)
        {
            return MongoDB_GetDocuments<DataEntry>(deviceID);
        }
    }
}
