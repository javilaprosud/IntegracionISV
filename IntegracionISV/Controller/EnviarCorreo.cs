using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IntegracionISV.Controller
{
    class EnviarCorreo
    {
        public void CorreoErrores(string cuerpo, string asunto)
        {
            Model.Correo cr = new Model.Correo();
            cr.destinatarios = "desarrollo@prosud.cl";
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress(cr.destinatarios));
            email.From = new MailAddress(cr.emisor);
            email.Subject = asunto;
            email.Body = cuerpo;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = cr.host;
            smtp.Port = cr.puerto;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(cr.emisor, cr.emisorContraseña);

            string output = null;
            try
            {
                smtp.Send(email);
                email.Dispose();
                output = "Correo electrónico fue enviado satisfactoriamente.";
            }
            catch (Exception ex)
            {
                output = "Error enviando correo electrónico: " + ex.Message;
            }
            Console.WriteLine(output);
        }

        public void CorreoFinal(string cuerpo, string asunto)
        {
            Model.Correo cr = new Model.Correo();
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("javila@prosud.cl"));
            email.To.Add(new MailAddress("mgonzalez@prosud.cl"));
            email.To.Add(new MailAddress("snavarrete@prosud.cl"));
            email.To.Add(new MailAddress("soporte@prosud.cl"));
            email.To.Add(new MailAddress("rcarrasco@prosud.cl"));
            email.To.Add(new MailAddress("rundurraga@prosud.cl"));
            email.From = new MailAddress(cr.emisor);
            email.Subject = asunto;
            email.Body = cuerpo;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = cr.host;
            smtp.Port = cr.puerto;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(cr.emisor, cr.emisorContraseña);

            string output = null;
            try
            {
                smtp.Send(email);
                email.Dispose();
                output = "Correo electrónico fue enviado satisfactoriamente.";
            }
            catch (Exception ex)
            {
                output = "Error enviando correo electrónico: " + ex.Message;
            }
            Console.WriteLine(output);
        }
        public void CorreoOK(string cuerpo, string asunto)
        {
            Model.Correo cr = new Model.Correo();
            cr.destinatarios = "desarrollo@prosud.cl";
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress(cr.destinatarios));
            email.From = new MailAddress(cr.emisor);
            email.Subject = asunto;
            email.Body = cuerpo;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = cr.host;
            smtp.Port = cr.puerto;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(cr.emisor, cr.emisorContraseña);

            string output = null;
            try
            {
                smtp.Send(email);
                email.Dispose();
                output = "Correo electrónico fue enviado satisfactoriamente.";
            }
            catch (Exception ex)
            {
                output = "Error enviando correo electrónico: " + ex.Message;
            }
            Console.WriteLine(output);
        }
    }
}
