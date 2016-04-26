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
        bool SetNewOwner(Owner owner);

        [OperationContract]
        Owner GetOwner(int ID);

        [OperationContract]
        List<Owner> GetAllOwners();
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Owner
    {
        [DataMember]
        public int OwnerID = -1;
        [DataMember]
        public int PaymentLevelID;
        [DataMember]
        public string Name;
    }
}
