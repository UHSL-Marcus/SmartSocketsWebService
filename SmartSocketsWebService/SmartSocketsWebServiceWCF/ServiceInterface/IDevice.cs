using SQLControlsLib;
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
        bool SetNewDevice(Device device, out int? ID);

        [OperationContract]
        [WebInvoke]
        bool UpdateDevice(Device device);


        [OperationContract]
        [WebInvoke]
        bool RemoveDevice(int ID);

        [OperationContract][WebInvoke]
        bool GetDevice(int ID, out Device result);

        [OperationContract][WebInvoke]
        bool GetAllDevices(out List<Device> result);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Device : DatabaseTableObject
    {
        [DatabaseID]
        [DataMember]
        public int? DeviceID;
        [DataMember]
        public int? RoomID;
        [DataMember]
        public int? DeviceTypeID;
        [DataMember]
        public string DeviceName;
        [DataMember]
        public string[] Commands;
    }
}
