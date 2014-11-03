using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace WindowsFormsApplication1
{
    public partial class MailSend : Form
    {
        public MailSend()
        {
            InitializeComponent();
        }
       
        //发送邮件的具体函数
        public bool Send(string a, string b, string c, string host, string sub, string body)
        {
            System.Net.Mail.SmtpClient client = new SmtpClient();
            client.Host = host;
            client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential(a, b);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                System.Net.Mail.MailMessage message = new MailMessage(a, c);
                message.Subject = sub;
                message.Body = body;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                AllError error = new AllError("邮件发送失败!");
                error.Show();
                return false;
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string host = "buptgogame@163.com";

            //发送者邮件密码
            string pwd = "buptgogame123";    //某某密码

            //接受者邮箱地址
            string reciver = "buptgogame@163.com";

            //SMTP服务器的主机名
            string domainhost = "smtp.163.com";

            //邮件标题
            string subject = "反馈";

            //邮件发送的主题内容
            string body = textBox1.Text;

            //调用方法，发送邮件
            if (Send(host, pwd, reciver, domainhost, subject, body))
            {
                AllSuccess success = new AllSuccess("邮件发送成功！");
                success.Show();
            }
        }
    }
}
