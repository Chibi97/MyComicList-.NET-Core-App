﻿using System;
using System.Net;
using System.Net.Mail;
using MyComicList.Application.Interfaces;

namespace MyComicList.Application.Helpers
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly string host;
        private readonly int port;
        private readonly string from;
        private readonly string password;

        public SmtpEmailSender(string host, int port, string from, string password)
        {
            this.host = host;
            this.port = port;
            this.from = from;
            this.password = password;
        }
        public string ToEmail { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }

        public void Send()
        {
            var smtp = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from, password)
            };

            using (var message = new MailMessage(from, ToEmail)
            {
                Subject = Subject,
                Body = Body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
