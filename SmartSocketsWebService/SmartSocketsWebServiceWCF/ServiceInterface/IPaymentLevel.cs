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
        bool SetNewPaymentLevel(PaymentLevel paymentLevel, out int? ID);

        [OperationContract]
        [WebInvoke]
        bool RemovePaymentLevel(int ID);*/

        [OperationContract][WebInvoke]
        bool GetPaymentLevel(int ID, out PaymentLevel result);

        [OperationContract][WebInvoke]
        bool GetAllPaymentLevels(out List<PaymentLevel> result);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class PaymentLevel : DatabaseTableObject
    {
        [DatabaseID]
        [DataMember]
        public int? PaymentLevelID;
        [DataMember]
        public string Name;
    }
}
