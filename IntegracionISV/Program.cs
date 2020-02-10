using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracionISV
{
    class Program
    {
        static void Main(string[] args)
        {
            Conexion.Conexiones cn = new Conexion.Conexiones();
            cn.FLAG_1();
            cn.SP_ReprocesoMensual();

            Controller.ObtenerArchivo oa = new Controller.ObtenerArchivo();
            oa.EliminacionArchivosComprimidos();
            oa.ProcesarArchivos();

            if (cn.obtenerFlag() == "1")
            {
                Extra.CuerpoCorreo cc = new Extra.CuerpoCorreo();
                Controller.EnviarCorreo cr = new Controller.EnviarCorreo();
                cr.CorreoFinal(cc.cuerpoCorreo(), "Carga Avance de Metas Prosud(" + DateTime.Now.ToString("dd / MMM / yyy hh: mm:ss") + ")");
                cn.ReprocesoISV();
                cr.CorreoOK("Proceso de Avance de Metas terminado correctamente.", "ISV(" + DateTime.Now.ToString("dd / MMM / yyy hh: mm:ss") + ")");
            }
            cn.FLAG_0();

            //Controller.EnviarCorreo cr = new Controller.EnviarCorreo();
            //cr.CorreoErrores("Prueba","Prueba");
        }
    }
}
