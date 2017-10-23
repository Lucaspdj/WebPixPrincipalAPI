using System;
using WebPixPrincipalRepository;
using WebPixPrincipalRepository.Entity;
using MailKit.Net.Smtp;
using MimeKit;

namespace WebPixPrincipalBLL
{
    public static class EmailBO
    {       

        public static bool EnviaSimplesEmail(Email email, string remetente, string destinatario, int idCliente)
        {
            var paramentros = ConfiguracaoDAO.GetParametros(idCliente);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Contato", remetente));
            message.To.Add(new MailboxAddress(destinatario));
            message.Subject = email.Titulo;

            message.Body = new TextPart("plain")
            {
                Text = email.Conteudo
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(paramentros["HostSTMP"], int.Parse(paramentros["PortaSTMP"]), true);
                    //client.Connect("smtp.gmail.com", 465, true);
                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(paramentros["LoginSTMP"], paramentros["SenhaSTMP"]);
                    //client.Authenticate("lucas.fernando.web@gmail.com", "lucas-2007");

                    client.Send(message);
                    client.Disconnect(true);
                    return true;
                }
            }
            catch(Exception e)
            { return false; }
            
        }

    }
}
