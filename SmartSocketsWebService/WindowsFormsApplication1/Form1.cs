using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceReferenceOnline.SmartSocketsWebServiceClient clientOnline = new
        ServiceReferenceOnline.SmartSocketsWebServiceClient();

            ServiceReference1.SmartSocketsWebServiceClient client = new
                ServiceReference1.SmartSocketsWebServiceClient();

            //client.Endpoint.Contract.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;


            //System.ServiceModel.Channels.Binding

            clientOnline.ClientCredentials.UserName.UserName = "root";
            clientOnline.ClientCredentials.UserName.Password = "root";

            client.ClientCredentials.UserName.UserName = "root";
            client.ClientCredentials.UserName.Password = "root";


            ServiceReferenceOnline.Owner ownerOnline = new ServiceReferenceOnline.Owner();
            ownerOnline.PaymentLevelID = 1;
            ownerOnline.OwnerName = "newOwner";
            string info = "";

            ServiceReference1.Owner owner = new ServiceReference1.Owner();
            owner.PaymentLevelID = 1;
            owner.OwnerName = "newOwner";


            if (client.SetNewOwner(out info, owner))
            {
                infoBox.Text += "\n Online true";
            }

            if (clientOnline.SetNewOwner(out info, ownerOnline))
            {
                infoBox.Text += "\n Online true";
            }


            clientOnline.Close();
            client.Close();
        }

    }
}
