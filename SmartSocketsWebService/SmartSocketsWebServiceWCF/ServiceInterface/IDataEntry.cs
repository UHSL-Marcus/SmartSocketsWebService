using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartSocketsWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISmartSocketsWebService" in both code and config file together.
    public partial interface ISmartSocketsWebService
    {
        [OperationContract]
        bool AddDataEntry(DataEntry entry, string deviceID);

        [OperationContract]
        DataEntry GetDataEntry(DateTime TimeStamp, string deviceID);

        [OperationContract]
        List<DataEntry> GetAllEntires(string deviceID);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class DataEntry
    {
        [DataMember]
        public DateTime TimeStamp;
        [DataMember]
        public int EntryType;
        [DataMember]
        public int DeviceSignature;
        [DataMember]
        public Data Data;

    }
    [DataContract]
    public class Data
    {
        [DataMember]
        public int Voltage;
        [DataMember]
        public int Current;
        [DataMember]
        public int Power;
    }
}
