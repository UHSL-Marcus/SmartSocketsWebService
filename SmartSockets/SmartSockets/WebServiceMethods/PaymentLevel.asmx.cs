using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SmartSockets.Table_Objects;

namespace SmartSockets
{

    public partial class SmartSocketsService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool SetNewPaymentLevel(PaymentLevel paymentlevel)
        {
            return doSqlInsert(paymentlevel);
        }

        [WebMethod]
        public PaymentLevel GetPaymentLevel(int ID)
        {
            PaymentLevel paymentLevel = new PaymentLevel();

            paymentLevel = (PaymentLevel)getEntryByID(ID, paymentLevel);

            return paymentLevel;
        }
    }
}