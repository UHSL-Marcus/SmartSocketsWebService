
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace SmartSocketsWebService.Utils
{
    public class ConnectionAuthenticator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName != "root" || password != "root")
                throw new FaultException("Invalid user and/or password");
        }
    }
}