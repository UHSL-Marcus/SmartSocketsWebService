using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartSocketsWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmartSocketsWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmartSocketsWebService.svc or SmartSocketsWebService.svc.cs at the Solution Explorer and start debugging.
    public partial class SmartSocketsWebService : ISmartSocketsWebService
    {
       public bool SetNewPaymentLevel(PaymentLevel paymentLevel, out string ID)
        {
            return SQL_doInsertReturnID(paymentLevel, out ID);
        }
        public bool RemovePaymentLevel(int ID)
        {
            return SQL_deleteEntryByID<PaymentLevel>(ID);
        }


        public bool GetPaymentLevel(int ID, out PaymentLevel result)
        {

            result = null;
            List<PaymentLevel> list = SQl_getEntryByID<PaymentLevel>(ID);

            if (list.Count != 1) return false;

            result = list[0];

            return true;
        }
        public bool GetAllPaymentLevels(out List<PaymentLevel> result)
        {
            result = SQL_getAllEntries<PaymentLevel>();

            if (result.Count > 0) return true;
            return false;
        }
    }
}
