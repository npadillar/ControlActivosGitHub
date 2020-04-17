using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Logistica.Libreria.Datos
{
   public  class CorreosDAO
    {
        //SmtpClient server = new SmtpClient("smtp.gmail.com", 587);

        public void Correos()
        {
            /*
             * Autenticacion en el Servidor
             * Utilizaremos nuestra cuenta de correo
             *
             * Direccion de Correo (Gmail o Hotmail)
             * y Contrasena correspondiente
             */
           // server.Credentials = new System.Net.NetworkCredential("yayra@sise.com.pe", "xxxx");
           // server.EnableSsl = true;
        }

        public void MandarCorreo(MailMessage mensaje)
        {
           // server.Send(mensaje);
        }
    }
}
