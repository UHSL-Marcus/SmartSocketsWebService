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
        bool SetNewDevice(Device device);

        [OperationContract]
        Device GetDevice(int ID);

        [OperationContract]
        List<Device> GetAllDevices();

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Device
    {
        [DataMember]
        public int DeviceID = -1;
        [DataMember]
        public int RoomID;
        [DataMember]
        public int DeviceTypeID;
    }
}
