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
        [OperationContract][WebInvoke]
        bool SetNewOwner(Owner owner, out string ID);

        [OperationContract]
        [WebInvoke]
        bool RemoveOwner(int ID);

        [OperationContract][WebInvoke]
        bool GetOwner(int ID, out Owner result);

        [OperationContract][WebInvoke]
        bool GetAllOwners(out List<Owner> result);
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
