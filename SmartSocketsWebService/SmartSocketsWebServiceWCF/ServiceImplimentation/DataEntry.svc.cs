using System;
using System.Collections.Generic;

namespace SmartSocketsWebService
{
    public partial class SmartSocketsWebService : ISmartSocketsWebService
    {
        public bool AddDataEntry(DataEntry entry, string deviceID)
        {
            return MongoDB_InsertDocument(entry, deviceID);
        }

        public bool GetDataEntry(DateTime TimeStamp, string deviceID, out DataEntry result)
        {
            result = null;
            List<DataEntry> list = MongoDB_GetDocumentFromID<DataEntry>(deviceID, TimeStamp);

            if (list.Count != 1) return false;

            result = list[0];

            return true;
        }

        public bool GetAllDataEntires(string deviceID, out List<DataEntry> result)
        {
            result = MongoDB_GetAllDocuments<DataEntry>(deviceID);

            if (result.Count > 0) return true;
            return false;
        }
    }
}
