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
        bool SetNewProperty(Property property);

        [OperationContract]
        bool GetProperty(int ID, out Property result);

        [OperationContract]
        bool GetAllProperties(out List<Property> result);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Property
    {
        [DataMember]
        public int PropertyID = -1;
        [DataMember]
        public int OwnerID;
    }
}
