
using System.Web.Services;
using SmartSockets.Table_Objects;

namespace SmartSockets
{

    public partial class SmartSocketsService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool SetNewPaymentLevel(PaymentLevel paymentlevel)
        {
            return SQL_doInsert(paymentlevel);
        }

        [WebMethod]
        public PaymentLevel GetPaymentLevel(int ID)
        {
            PaymentLevel paymentLevel = new PaymentLevel();

            paymentLevel = (PaymentLevel)SQl_getEntryByID(ID, paymentLevel);

            return paymentLevel;
        }
    }
}