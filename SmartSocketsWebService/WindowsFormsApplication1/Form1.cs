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
            ServiceReference1.SmartSocketsWebServiceClient client = new
        ServiceReference1.SmartSocketsWebServiceClient();

            client.Endpoint.Contract.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;


            //System.ServiceModel.Channels.Binding

            client.ClientCredentials.UserName.UserName = "root";
            client.ClientCredentials.UserName.Password = "root";

            ServiceReference1.Device d = new ServiceReference1.Device();
            d.Commands = new string[] { "temp", "temp2" };
            d.DeviceName = "name";
            d.DeviceTypeID = 1;
            d.RoomID = 22;
            string info = "";

            if (client.SetNewDevice(out info, d))
            {
                info += "\n true";
            }

            infoBox.Text = info;
        }

    }
}
