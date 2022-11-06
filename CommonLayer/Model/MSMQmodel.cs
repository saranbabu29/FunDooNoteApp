﻿using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQmodel
    {
        
        MessageQueue messageQueue = new MessageQueue();

        public void sendData2Queue(string token)
        {
            messageQueue.Path = @".\private$\token";
            if (!MessageQueue.Exists(messageQueue.Path))
            {
                //Exists
                MessageQueue.Create(messageQueue.Path);
            }

            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQueue.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            string subject = "Fundoo Notes Reset Link";
            string body = token;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("saranbabue2905@gmail.com");
            mail.To.Add("saranbabue2905@gmail.com");
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "<body><p>Dear User,<br><br>" +
              "Forgot your password?<br>" +
              "We have sent you a link for resetting your password.<br></body>" + token;

            mail.Body = htmlBody;

            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("saranbabue2905@gmail.com", "dtdcfyngmyjudcts"),
                EnableSsl = true,
            };
            SMTP.Send(mail);
            messageQueue.BeginReceive();
        }

    }
}
