using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Haferbrei{
public static class SendEmail
{
    public static Action EmailWasSent;
    
    public static void SendMail(string _subject, string _body, List<Attachment> _attachments = null)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("Haferbrei.Bugreport@gmail.com");
        mail.To.Add("Haferbrei.Bugreport@gmail.com");
        mail.Subject = _subject;
        mail.Body = _body;
        if (_attachments != null)
        {
            foreach (var attachment in _attachments)
            {
                mail.Attachments.Add(attachment);
            }
        }
        // you can use others too.
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("Haferbrei.Bugreport@gmail.com", "BUG-r3p0rt3r") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        
        smtpServer.SendCompleted += SendCompletedCallback;
        smtpServer.SendMailAsync(mail);
    }
    
    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        if (e.Cancelled)
        {
            Debug.LogError("Bugreport send canceled.");
        }
        if (e.Error != null)
        {
            Debug.LogError("Bugreport send canceled.");
        } else
        {
            Debug.Log("Bugreport successfully sent!");
        }
        EmailWasSent?.Invoke();
    }
}
}