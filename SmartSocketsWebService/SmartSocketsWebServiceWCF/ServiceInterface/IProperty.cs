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
        bool SetNewProperty(Property property, out int? ID);

        [OperationContract]
        [WebInvoke]
        bool UpdateProperty(Property property);

        [OperationContract]
        [WebInvoke]
        bool RemoveProperty(int ID);

        [OperationContract][WebInvoke]
        bool GetProperty(int ID, out Property result);

        [OperationContract][WebInvoke]
        bool GetAllProperties(out List<Property> result);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Property : DatabaseTableObject
    {
        [DatabaseID]
        [DataMember]
        public int? PropertyID;
        [DataMember]
        public int? OwnerID;
    }
}
