using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

            //client.Endpoint.Contract.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            
            //System.ServiceModel.Channels.Binding

            client.ClientCredentials.UserName.UserName = "root";
            client.ClientCredentials.UserName.Password = "root";

            ServiceReference1.Device[] devices;
            string info = "";

            if (client.GetAllDevices(out devices)) {
                foreach (ServiceReference1.Device device in devices)
                {
                    info += device.DeviceID + "\n";
                }
            }

            infoBox.Text = info; 
        }
    }
}
