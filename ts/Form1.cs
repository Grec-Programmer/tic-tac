using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ts
{
    public partial class Form1 : Form
    {
        public string Player = "O";
        //char[,] arr = new char[3, 3];
        public Form1()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Arr.setArr(i, j, ' ');
            InitializeComponent();
            timer1.Start();
        }
        public void updateView()
        {
            btn1.Text = Arr.getArrChar(0, 0).ToString();
            btn2.Text = Arr.getArrChar(0, 1).ToString();
            btn3.Text = Arr.getArrChar(0, 2).ToString();
            btn4.Text = Arr.getArrChar(1, 0).ToString();
            btn5.Text = Arr.getArrChar(1, 1).ToString();
            btn6.Text = Arr.getArrChar(1, 2).ToString();
            btn7.Text = Arr.getArrChar(2, 0).ToString();
            btn8.Text = Arr.getArrChar(2, 1).ToString();
            btn9.Text = Arr.getArrChar(2, 2).ToString();
        } 
        private void btn_Click(object sender, EventArgs e)
        {
            //((Button)sender).Text = Player;
            if(((Button)sender).Name=="btn1") Arr.setArr(0, 0, Player[0]);
            if (((Button)sender).Name == "btn2") Arr.setArr(0, 1, Player[0]);
            if (((Button)sender).Name == "btn3") Arr.setArr(0, 2, Player[0]); 
            if (((Button)sender).Name == "btn4") Arr.setArr(1, 0, Player[0]);
            if (((Button)sender).Name == "btn5") Arr.setArr(1, 1, Player[0]);
            if(((Button)sender).Name == "btn6")  Arr.setArr(1,2, Player[0]);
            if(((Button)sender).Name == "btn7")  Arr.setArr(2,0, Player[0]);
            if(((Button)sender).Name =="btn8")  Arr.setArr(2,1, Player[0]);
            if(((Button)sender).Name=="btn9")    Arr.setArr(2,2, Player[0]);
            ((Button)sender).Enabled = false;
            //Player = (Player == "X") ? "O" : "X";
            updateView();
            isWin();
            Server.Connect(Server.clientIp, Server.clientPort);
            WaitOn();
        }

        void WaitOn()
        {
            btn1.Enabled = false;
            btn2.Enabled = false;
            btn3.Enabled = false;
            btn4.Enabled = false;
            btn5.Enabled = false;
            btn6.Enabled = false;
            btn7.Enabled = false;
            btn8.Enabled = false;
            btn9.Enabled = false;
        }
        public void WaitOff()
        {
            if (btn1.Text == " ") btn1.Enabled = true;
            if (btn2.Text == " ") btn2.Enabled = true;
            if (btn3.Text == " ") btn3.Enabled = true;
            if (btn4.Text == " ") btn4.Enabled = true;
            if (btn5.Text == " ") btn5.Enabled = true;
            if (btn6.Text == " ") btn6.Enabled = true;
            if (btn7.Text == " ") btn7.Enabled = true;
            if (btn8.Text == " ") btn8.Enabled = true;
            if (btn9.Text == " ") btn9.Enabled = true;
        }

        public bool isWin()
        {
            //if ((btn1.Text != " " && btn1.Text == btn2.Text && btn1.Text == btn3.Text)
            //  ||(btn1.Text != " " && btn1.Text == btn4.Text && btn1.Text == btn7.Text)
            //  ||(btn1.Text != " " && btn1.Text == btn5.Text && btn1.Text == btn9.Text)
            //  ||(btn2.Text != " " && btn2.Text == btn5.Text && btn2.Text == btn8.Text)
            //  ||(btn1.Text != " " && btn1.Text == btn5.Text && btn1.Text == btn9.Text)
            //  ||(btn1.Text != " " && btn1.Text == btn5.Text && btn1.Text == btn9.Text)
            //  ||(btn1.Text != " " && btn1.Text == btn5.Text && btn1.Text == btn9.Text)
            //  ||(btn1.Text != " " && btn1.Text == btn5.Text && btn1.Text == btn9.Text))
            char[,] arr = Arr.getArr();
            if ((Arr.getArrChar(0, 0) != ' ' && arr[0, 0]== arr[0, 1] && arr[0, 0] == arr[0, 2])
              || (arr[0, 0] != ' ' && arr[0, 0] == arr[1, 0] && arr[0, 0] == arr[1, 0])
              || (arr[0, 0] != ' ' && arr[0, 0] == arr[1, 1] && arr[0, 0] == arr[2, 2])
              || (arr[0, 1] != ' ' && arr[0, 1] == arr[1, 1] && arr[0, 1] == arr[2, 1])
              || (arr[0, 0] != ' ' && arr[0, 0] == arr[1, 1] && arr[0, 0] == arr[2, 2])
              || (arr[0, 0] != ' ' && arr[0, 0] == arr[1, 1] && arr[0, 0] == arr[2, 2])
              || (arr[0, 0] != ' ' && arr[0, 0] == arr[1, 1] && arr[0, 0] == Arr.getArrChar(2, 2))
              || (arr[0, 0] != ' ' && arr[0, 0] == arr[1, 1] && arr[0, 0] == arr[2, 2]))
                MessageBox.Show($"{Player} победил!", "");
                {
                    return true;
                }
            }

        static Socket listen=null;
        static bool netGame=false;
        static void ServerStart() 
        { 
            IPEndPoint ep =new IPEndPoint(IPAddress.Parse("192.168.51.183"), 8001);
            listen=new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            listen.Bind(ep);
            listen.Listen(10);
            StringBuilder stringBuilder= new StringBuilder();
            int bytes = 0;
            byte[] data = new byte[1024];
            try
            {
                netGame=true;
                listen = listen.Accept();
                bytes = listen.Receive(data);
                string mes=Encoding.Unicode.GetString(data, 0, bytes); 
                if(listen.Connected) 
                { 
                    MessageBox.Show(mes,""); 
                    
                }
                //stringBuilder.Append(Encoding.Unicode.GetString(data,0,bytes));
            }
            catch (Exception ex) { }
           
        }
        Thread tServer = new Thread(ServerStart);
        bool ServerON = false;
        private void btnServer_Click(object sender, EventArgs e)
        {
                Server.StartServer(ServerON);
                ServerON=!ServerON;
        }
    
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            //Player = "X";
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Arr.setArr(i, j, ' ');
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            
            Connect connect = new Connect();
            connect.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateView();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateView();
        }
    }
    }
