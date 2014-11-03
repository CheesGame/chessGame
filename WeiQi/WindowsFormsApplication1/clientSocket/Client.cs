using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace clientSocket
{
    public class Client
    {
        private byte[] result = new byte[1024];
        private String sendMessage;     //保存要发送的消息
        private String receiveMessage;  //保存接收到的消息
        private int opponent;       //保存对手玩家的号
        private IPAddress ip = IPAddress.Parse("192.168.1.102");
        private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public bool connectSever = false;
        
        public Client()
        {
            try{
                clientSocket.Connect(new IPEndPoint(ip, 8099)); //配置服务器IP与端口  
                connectSever=true;
            }
            catch (Exception e)
            {
                connectSever=false;
            }
        }
        //接收消息
        public void receive()
        {
            try
            {
                int receiveLength = clientSocket.Receive(result);
                receiveMessage = Encoding.ASCII.GetString(result, 0, receiveLength);
            }
            catch (Exception e)
            {
                this.clientSocket.Close();
                return;
            }
        }
        //发送消息
        public void send()
        {
            clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
        }
        //获取接收到的消息
        public String getReceiveMessage()
        {
            return this.receiveMessage;
        }
        //设置要发送的消息
        public void setSendMessage(String sendMessage)
        {
            this.sendMessage = sendMessage;
        }
        //获取要发送的消息
        public String getSendMessage()
        {
            return this.sendMessage;
        }
    }
}
