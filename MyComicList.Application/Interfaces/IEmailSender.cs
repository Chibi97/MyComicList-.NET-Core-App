using System;

namespace MyComicList.Application.Interfaces
{
    public interface IEmailSender
    {
        string ToEmail { get; set; }
        string Body { get; set; }
        string Subject { get; set; }

        void Send();
    }
}
