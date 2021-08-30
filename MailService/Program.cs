using Rebex.Net;
using Rebex.Mail;
using Rebex.Mime.Headers;
using Limilabs.Client.POP3;
using System;
using System.IO;
using System.Net.Sockets;
using Pop3 = Rebex.Net.Pop3;
//using Rebex.Mail.MailMessage;
using EASendMail;
using System.Net.Mail;
using System.Collections.Generic;
//using static System.Collections.Generic.Dictionary;
namespace receiveemail
{

    class Program
    {
        
        static void Main(string[] args)
        {

            Rebex.Licensing.Key = "==AZZSDeZlKccFLLXGJOGzCAbr8jyuNF3VUOR83Ep4yA6k==";
            // 
            // create client, connect and log in 
            var imap = new Rebex.Net.Imap();
            Pop3 client = new Pop3();
            client.Connect("pop.gmail.com", SslMode.Implicit);

            client.Login("login", "pass");
           
            // get message list 
            Pop3MessageCollection list = client.GetMessageList();
            Dictionary<int, string> mail = new Dictionary<int, string>(list.Count);
            if (list.Count == 0)
            {
                Console.WriteLine("There are no messages in the mailbox.");
            }
            else
            {
                // download the first message 
                Rebex.Mail.MailMessage message = client.GetMailMessage(list[0].SequenceNumber);

            }
            Console.WriteLine();
            Console.WriteLine(client.GetMessageCount());
            int count = client.GetMessageCount();
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine(i+": "+client.GetMailMessage(i).Subject);
                mail.Add(i, client.GetMailMessage(i).Subject);
            }
            
            Console.WriteLine("Хотите удалить сообщение? Введите его номер.");
           
            string line="";
            line=Console.ReadLine();
            int key = Convert.ToInt32(line);
            mail[key] = "deleted#$&•~|)(!?*:;+-=™®©✓¢₽€£√π×÷}";
            //mail.Add(-1, client.GetMailMessage(key).Subject);
            client.Delete(key);
            
            int j = 1;
            foreach (KeyValuePair<int, string> keyValue in mail)
            {
                if (keyValue.Value != "deleted") { Console.WriteLine(j+" " + keyValue.Value); j++;}
               
            }
            client.Disconnect();
            
        }
    }
}