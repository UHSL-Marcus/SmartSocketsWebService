﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SmartSocketsWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISmartSocketsWebService" in both code and config file together.
    public partial interface ISmartSocketsWebService
    {
        [OperationContract][WebInvoke]
        bool AddDataEntry(DataEntry entry, string deviceID);

        [OperationContract]
        [WebInvoke]
        bool GetDataEntry(DateTime TimeStamp, string deviceID, out DataEntry result);

        [OperationContract]
        [WebInvoke]
        bool GetAllDataEntires(string deviceID, out List<DataEntry> result);

    }


    
    [DataContract]
    public class DataEntry
    {
        [BsonId]
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
