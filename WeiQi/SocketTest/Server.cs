using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace SocketTest
{
    class Server
    {
        private const int MAXPLAYER = 20;
        private static byte[] result = new byte[1024];
        private static int myPort = 8099;   //端口  
        static Socket serverSocket;
        public static int[] groupNo = new int[12];
        static public Socket[] client = new Socket[12];
        static public string[] playersName = new string[12];
        public static void Main(string[] args)
        {
            //服务器IP地址  
            // IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, myPort));  //绑定IP地址：端口  
            serverSocket.Listen(MAXPLAYER);    //设定最多20个排队连接请求  
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());
            //通过Clientsoket发送数据  
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            Console.ReadLine();
        }

        static private bool vertify(string user, string password)
        {
            FileStream f = new FileStream("users/users.txt", FileMode.Open);
            StreamReader r = new StreamReader(f);
            string fileString = r.ReadToEnd();
            r.Close();
            f.Close();
            String[] fileSplit = new String[10000];
            fileSplit = fileString.Split(';');
            for (int i = 1; i < fileSplit.Length; i++)
            {
                string[] userstring = new string[5];
                userstring = fileSplit[i].Split(':');
                if (userstring[1] == user && userstring[3] == password)
                {
                    return true;
                }
            }
            return false;
        }

        static public bool isLogin(string name)
        {
            for (int i = 0; i < 12; i++)
            {
                if (playersName[i] == name)
                {
                    return true;
                }
            }
            return false;
        }
        //获得一个号
        static public int GetPlayNumber()
        {
            for (int i = 0; i < 12; i++)
            {
                if (groupNo[i] > 6 || groupNo[i] <= 0)
                {
                    return i;
                }
            }
            return 100;
        }
        //先登录还是后登陆
        static public int GetSequence(int number)
        {
            for (int i = 0; i < 12; i++)
            {
                if (groupNo[i] == number)
                {
                    return i;
                }
            }
            return number;
        }
        static public string GetLoginContent()
        {
            string loginContent = "";
            for (int i = 0; i < 12; i++)
            {
                if (groupNo[i] > 0 && groupNo[i] < 6)
                    loginContent = loginContent + ";" + groupNo[i] + "," + playersName[i];
                else
                    loginContent = loginContent + ";";
            }
            return loginContent;
        }
        static public bool isExist(string namestring)
        {
            FileStream f = new FileStream("users/users.txt", FileMode.Open);
            StreamReader r = new StreamReader(f);
            string fileString = r.ReadToEnd();
            r.Close();
            f.Close();
            String[] fileSplit = new String[10000];
            fileSplit = fileString.Split(';');
            for (int i = 1; i < fileSplit.Length; i++)
            {
                string[] userstring = new string[5];
                userstring = fileSplit[i].Split(':');
                if (userstring[1] == namestring)
                {
                    return true;
                }
            }
            return false;
        }
        static public void saveInfor(string username, string password)
        {
            FileStream fs = new FileStream("users/users.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(";user:" + username + ":password:" + password);
            sw.Close();
            fs.Close();
        }
        private static void ListenClientConnect()
        {
            while (true)
            {
                int number = GetPlayNumber();
                client[number] = serverSocket.Accept();
                byte[] result = new byte[1024];
                int receiveNumber = client[number].Receive(result);
                String receiveMessage = Encoding.ASCII.GetString(result, 0, receiveNumber);
                string[] recieveMessageSplit = receiveMessage.Split(',');
                if (recieveMessageSplit[2] == "0")
                {
                    //登陆
                    if (vertify(recieveMessageSplit[0], recieveMessageSplit[1]) && !(isLogin(recieveMessageSplit[0])))
                    {
                        try
                        {
                            playersName[number] = recieveMessageSplit[0];
                            //给客户端返回登陆成功
                            string LoginContent = GetLoginContent();
                            client[number].Send(Encoding.ASCII.GetBytes("true:" + GetLoginContent()));
                            //服务器接收组号数据
                            receiveNumber = client[number].Receive(result);
                            receiveMessage = Encoding.ASCII.GetString(result, 0, receiveNumber);
                            groupNo[number] = int.Parse(receiveMessage);
                            //判断是first还是last
                            int opponentNo = GetSequence(int.Parse(receiveMessage));
                            if (opponentNo == number)
                            {   //first
                                //        client[number].Send(Encoding.ASCII.GetBytes("first"));
                            }
                            else
                            {  //last
                                //        client[number].Send(Encoding.ASCII.GetBytes("last"));
                                new Process(client[opponentNo], client[number]).clientToClient();
                            }
                        }
                        catch (Exception e)
                        {
                            return;
                        }

                    }
                    else
                    {
                        client[number].Send(Encoding.ASCII.GetBytes("false"));
                    }
                }
                else
                {
                    //注册
                    if (!isExist(recieveMessageSplit[0]))
                    {
                        try
                        {
                            //在文件中进行存储信息
                            saveInfor(recieveMessageSplit[0], recieveMessageSplit[1]);
                            playersName[number] = recieveMessageSplit[0];
                            //给客户端返回注册成功
                            string LoginContent = GetLoginContent();
                            client[number].Send(Encoding.ASCII.GetBytes("true:" + GetLoginContent()));
                            //服务器接收组号数据
                            receiveNumber = client[number].Receive(result);
                            receiveMessage = Encoding.ASCII.GetString(result, 0, receiveNumber);
                            groupNo[number] = int.Parse(receiveMessage);
                            //判断是first还是last
                            int opponentNo = GetSequence(int.Parse(receiveMessage));
                            if (opponentNo == number)
                            {   //first
                                //        client[number].Send(Encoding.ASCII.GetBytes("first"));
                            }
                            else
                            {  //last
                                //        client[number].Send(Encoding.ASCII.GetBytes("last"));
                                new Process(client[opponentNo], client[number]).clientToClient();
                            }
                        }
                        catch (Exception e)
                        {
                            return;
                        }

                    }
                    else
                    {
                        client[number].Send(Encoding.ASCII.GetBytes("false"));
                    }
                }
            }
        }

        private class Process
        {
            Socket clientSocket1;
            Socket clientSocket2;

            public Process(Socket client1, Socket client2)
            {
                this.clientSocket1 = client1;
                this.clientSocket2 = client2;
            }

            public void clientToClient()
            {
                new Thread(client1ToClient2).Start();
                new Thread(client2ToClient1).Start();
            }

            private void client1ToClient2()
            {
                while (true)
                {
                    try
                    {
                        byte[] result1 = new byte[1024];
                        int receiveNumber1 = clientSocket1.Receive(result1);
                        String receiveMessage1 = Encoding.ASCII.GetString(result1, 0, receiveNumber1);
                        clientSocket2.Send(Encoding.ASCII.GetBytes(receiveMessage1));
                    }
                    catch (Exception ex)
                    {
                        clientSocket1.Shutdown(SocketShutdown.Both);
                        clientSocket1.Close();
                        try
                        {
                            clientSocket2.Send(Encoding.ASCII.GetBytes("end"));
                        }
                        catch
                        {
                        }
                        break;
                    }
                }
            }
            private void client2ToClient1()
            {
                while (true)
                {
                    try
                    {
                        byte[] result2 = new byte[1024];
                        int receiveNumber2 = clientSocket2.Receive(result2);
                        String receiveMessage2 = Encoding.ASCII.GetString(result2, 0, receiveNumber2);
                        clientSocket1.Send(Encoding.ASCII.GetBytes(receiveMessage2));
                    }
                    catch (Exception ex)
                    {
                        clientSocket2.Shutdown(SocketShutdown.Both);
                        clientSocket2.Close();
                        try
                        {
                            clientSocket1.Send(Encoding.ASCII.GetBytes("end"));
                        }
                        catch
                        {
                        }
                        break;
                    }
                }
            }
        }
    }

}