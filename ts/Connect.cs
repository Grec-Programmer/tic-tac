using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ts
{
    public partial class Connect : Form
    {
        public IPEndPoint ep;
        public Connect()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (tBoxIp.Text != "" & tBoxPort.Text != "")
            {
                ep = new IPEndPoint(IPAddress.Parse(tBoxIp.Text), Convert.ToInt32(tBoxPort.Text));
                btnConnect.Enabled = true;
            }
        }
        void set() 
        {
            string mes = "XXXOOOXXX";
            int k = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++, k++)
                    Arr.setArr(i, j, mes[k]);
        }
       
        private void btnConnect_Click(object sender, EventArgs e)
        {
            //Server.Connect($"{tBoxIp.Text}", Convert.ToInt32(tBoxPort.Text));
            Server.clientIp = IPAddress.Parse($"{tBoxIp.Text}");
            Server.clientPort = Convert.ToInt32($"{tBoxPort.Text}");
            DialogResult= DialogResult.OK;
        }
    }
}
