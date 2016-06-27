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


            clientOnline.ClientCredentials.UserName.UserName = "root";
            clientOnline.ClientCredentials.UserName.Password = "root";


            ServiceReferenceOnline.Owner ownerOnline = new ServiceReferenceOnline.Owner();
            ownerOnline.PaymentLevelID = 1;
            ownerOnline.OwnerName = "newOwner";

            int? id;
            if (clientOnline.SetNewOwner(out id, ownerOnline))
            {
                infoBox.Text += "\n Online true (" + id.Value + ")";
            }


            clientOnline.Close();
        }

    }
}
