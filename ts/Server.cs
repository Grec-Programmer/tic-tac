using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ts
{
    internal class Server
    {
        public static IPAddress clientIp;
        public static int clientPort;
        static Socket listen = null;
        static Form1 form11 = Arr.GetForm();
        static public void Start()
        {
            while (true)
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.51.183"), 8001);
                listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listen.Bind(ep);
                listen.Listen(10);
                Socket listen1 = listen.Accept();
                byte[] data = new byte[1024];
                int bytes = 0;
                    bytes = listen1.Receive(data);
                    string mes = Encoding.Unicode.GetString(data, 0, bytes);
                    if (listen1.Connected)
                    {
                        Form1 form = new Form1();
                        //MessageBox.Show(mes, "");
                        int k = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++, k++)
                            {
                                Arr.setArr(i, j, mes[k]);
                            }
                           
                        }

                    }
                    form11.WaitOff();
                form11.isWin();
                    listen.Close();
                    listen1.Close();
            }
        }

        
        static Thread tServer = new Thread(Start);
        public static void StartServer(bool ServerON)
        {
            //ServerON = !ServerON;
            if (!ServerON)
            {
                tServer = new Thread(Start);
                tServer.Start();
                tServer.IsBackground= true;
                MessageBox.Show("server on","2");
            }
            else
            {
                //try
                //{
                //    listen.Close();
                //}
                //catch { }
                listen.Close();
                listen = null;
                
                tServer.Abort();
                MessageBox.Show("server off", "2");
            }
        }
        static string getString()
        {
            string mes = "";
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    mes += Arr.getArrChar(i, j);
            return mes;
        }

        public static void Connect(IPAddress ip, int port)
        {
            IPEndPoint ipPoint = new IPEndPoint(ip, port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);
            byte[] data = Encoding.Unicode.GetBytes(getString());
            socket.Send(data);
            //socket.Disconnect()
            //socket.Shutdown();
            socket.Close();
        }
    }
}
