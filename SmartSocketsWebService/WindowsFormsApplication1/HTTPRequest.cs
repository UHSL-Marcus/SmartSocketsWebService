using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WindowsFormsApplication1
{
    class HTTPRequest
    {
        public string HttpSimpleSOAPRequest()
        {
            try
            {

                string SOAPReq = @" <?xml version=""1.0"" encoding=""utf - 8""?>
                                    <s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
                                        <s:Header>
                                            <Action s:mustUnderstand=""1"" xmlns=""http://schemas.microsoft.com/ws/2005/05/addressing/none"">http://tempuri.org/ISmartSocketsWebService/GetAllDevices</Action>
                                        </s:Header> 
                                        <s:Body>
                                            <GetAllDevices xmlns=""http://tempuri.org/"" />
                                        </s:Body>
                                    </s:Envelope>";

                //Builds the connection to the WebService.
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://localhost:80/soapService");
                req.Method = "POST";
                req.ContentType = "text/xml; charset=utf-8";
                req.ContentLength = Encoding.UTF8.GetByteCount(SOAPReq);


                //Pass the SoapRequest String to the WebService
                StreamWriter stmw = new StreamWriter(req.GetRequestStream());
                stmw.Write(SOAPReq);
                stmw.Flush();

                WebResponse resp = req.GetResponse();
                StreamReader r = new StreamReader(resp.GetResponseStream());

                string s = "*****Request******\n" + SOAPReq + "\n\n********Response********\n" + r.ReadToEnd(); ;

                return s;
            }
            catch (Exception e)
            {
                Console.WriteLine("HTTP Exception: " + e.ToString() + " (" + e.Message + ")");
            }
            return null;



        }
    }
}
