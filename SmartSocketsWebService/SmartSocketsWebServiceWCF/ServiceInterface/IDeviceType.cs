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
        /*[OperationContract][WebInvoke]
        bool SetNewDeviceType(DeviceType deviceType, out int? ID);

        [OperationContract]
        [WebInvoke]
        bool RemoveDeviceType(int ID);*/

        [OperationContract][WebInvoke]
        bool GetDeviceType(int ID, out DeviceType result);

        [OperationContract][WebInvoke]
        bool GetAllDeviceTypes(out List<DeviceType> result);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class DeviceType : DatabaseTableObject
    {
        [DatabaseID]
        [DataMember]
        public int? DeviceTypeID;
        [DataMember]
        public string DeviceTypeName;
    }
}
