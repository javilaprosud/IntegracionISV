using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracionISV.Model
{
    class Correo
    {
        public string destinatarios { get; set; }
        public string asunto { get; set; }
        public string cuerpo { get; set; }
        public string host { get; set; }
        public int puerto { get; set; }
        public string emisor { get; set; }
        public string emisorContraseña { get; set; }

        public Correo()
        {
            this.emisor = "procesos@prosud.cl";
            this.emisorContraseña = "Masofejo.88";
            this.host = "smtp.office365.com";
            this.puerto = 587;
        }
    }
}
