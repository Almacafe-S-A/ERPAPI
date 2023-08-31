using ERPAPI.Models;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Options;
using ERPAPI.Controllers;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Helpers
{
    public  class EmailHelper
    {
        private  readonly IOptions<MyConfig> _config;
        private  readonly ILogger _logger;
        public  EmailHelper(IOptions<MyConfig> config, ILogger<EmailHelper> logger)
        {
            _config = config;
            _logger= logger;
        }

        public  async Task<bool> EnviarCorreo(StringBuilder cuerpo, string asunto, string destinatario )
        {
            try
            {
                string correoOrigen = _config.Value.emailsender;
                string contraseña = _config.Value.passwordsmtp;
                string correoDestino = destinatario;
                string host = _config.Value.smtp;
                string puerto = _config.Value.port;
                string dominio = _config.Value.dominio;

                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(correoOrigen, "Notificación de Alerta", Encoding.UTF8);
                correo.To.Add(correoDestino);
                correo.Subject = asunto;

                StringBuilder bodyBuilder = new StringBuilder();
                
                //bodyBuilder.AppendLine(cuerpo);
                correo.Body = cuerpo.ToString();
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient("almacafehn-com.mail.protection.outlook.com");
                smtp.Host = host;
                smtp.Port = Convert.ToInt32(puerto);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(correoOrigen, contraseña, dominio);
                smtp.EnableSsl = true;

                ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, sslPolicyErrors) => true;

                await smtp.SendMailAsync(correo);

                Console.WriteLine("Correo enviado correctamente.");

                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
                _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return false;
            }


        }

    }
}
