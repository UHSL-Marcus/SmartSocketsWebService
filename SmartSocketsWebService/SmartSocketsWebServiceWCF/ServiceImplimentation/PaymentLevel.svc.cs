using SQLControlsLib;
using System.Collections.Generic;

namespace SmartSocketsWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmartSocketsWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmartSocketsWebService.svc or SmartSocketsWebService.svc.cs at the Solution Explorer and start debugging.
    public partial class SmartSocketsWebService : ISmartSocketsWebService
    {
       /*public bool SetNewPaymentLevel(PaymentLevel paymentLevel, out int? ID)
        {
            return Set.doInsertReturnID(paymentLevel, out ID);
        }
        public bool RemovePaymentLevel(int ID)
        {
            return Delete.doDeleteEntryByID<PaymentLevel, int>(ID)
        }*/


        public bool GetPaymentLevel(int ID, out PaymentLevel result)
        {

            bool success = false;
            result = null;
            List<PaymentLevel> list;
            if (Get.doSelectByID(ID, out list))
                if (list.Count == 1) { result = list[0]; success = true; }

            return success;
        }
        public bool GetAllPaymentLevels(out List<PaymentLevel> result)
        {
            return Get.doSelectAll(out result);
        }
    }
}
