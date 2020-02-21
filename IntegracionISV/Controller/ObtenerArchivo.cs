using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Newtonsoft.Json;
using System.IO.Compression;

namespace IntegracionISV.Controller
{
    class ObtenerArchivo
    {
        public void EliminacionArchivosComprimidos()
        {
            Model.Archivo arch = new Model.Archivo();
            Conexion.Conexiones cn = new Conexion.Conexiones();
            arch.allFilesZip = Directory.GetFiles(arch.pathZip, "*.zip", SearchOption.AllDirectories);
            try
            {
                for (int i = 0; i < arch.allFilesZip.Count(); i++)
                {
                    File.Delete(arch.allFilesZip[i]);
                }
                cn.EjecutarLog("Correcta Eliminacion", "OK ELIMINACION");
            }
            catch (Exception e)
            {
               
                cn.EjecutarLog(e.ToString(),"ERROR ELIMINACION" );              
                EnviarCorreo cr = new EnviarCorreo();
                cr.CorreoErrores("Se genero un error al momento de eliminar los archivos. "+e , "Error ISV Integracion");
            }
        }

        public void ProcesarArchivos()
        {
            Conexion.Conexiones cn = new Conexion.Conexiones();
            Model.Archivo arch = new Model.Archivo();
            Model.Integracion it = new Model.Integracion();
            DataTable dt = new DataTable();
            using (cn.procesadora_analisis())
            {
                SqlCommand cmd = new SqlCommand(cn.query_fechas, cn.procesadora_analisis());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        try
                        {
                            var httpWebRequest = (HttpWebRequest)WebRequest.Create(it.url);
                            httpWebRequest.ContentType = "application/json";
                            httpWebRequest.Headers.Add("Authorization", it.token);
                            httpWebRequest.Headers.Add("Postman-Token", "69dda9c7-6396-45c4-934f-7c59fa6f32e9");
                            httpWebRequest.Headers.Add("cache-control", "no_cache");
                            httpWebRequest.Method = "POST";

                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                it.json = "";
                                it.json = "{";
                                it.json = it.json + "\"views\": [],";
                                it.json = it.json + "\"view_type\": \"diario\",";
                                it.json = it.json + "\"dates\": [\"" + row[dc].ToString() + "\"";
                                //it.json = it.json + "\"dates\": [ \" \"";
                                it.json = it.json + "],";
                                it.json = it.json + "\"chain_codes\":[],";
                                it.json = it.json + "\"hierarchy\": {";
                                it.json = it.json + "\"Holding\":[],";
                                it.json = it.json + "\"Cadena\":[],";
                                it.json = it.json + "\"Supervisor\":[],";
                                it.json = it.json + "\"Zona\":[],";
                                it.json = it.json + "\"Local\":[],";
                                it.json = it.json + "\"Zona Nielsen\":[],";
                                it.json = it.json + "\"Comuna\":[],";
                                it.json = it.json + "\"Mercaderista\":[],";
                                it.json = it.json + "\"Rut Mercaderista\":[],";
                                it.json = it.json + "\"Descripción\":[],";
                                it.json = it.json + "\"Linea\":[],";
                                it.json = it.json + "\"Categoría\":[],";
                                it.json = it.json + "\"Marca\":[],";
                                it.json = it.json + "\"Sub Categoría\":[],";
                                it.json = it.json + "\"Código Interno\":[],";
                                it.json = it.json + "\"EAN\":[],";
                                it.json = it.json + "\"DUN\":[],";
                                it.json = it.json + "\"Vigencia\":[]";
                                it.json = it.json + "}";
                                it.json = it.json + "}";
                                streamWriter.Write(it.json);
                                streamWriter.Flush();
                            }
                            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var responseText = streamReader.ReadToEnd();
                                Console.WriteLine(responseText);
                                if (responseText == "Error en el servidor remoto: (401) No autorizado.")
                                {
                                    EnviarCorreo cr = new EnviarCorreo();
                                    cr.CorreoErrores("Error en el servidor remoto: (401) No autorizado. Descarga("+ row[dc].ToString() + "). - Procesar Tarea Nuevamente", "Error Integracion ISV");
                                    cn.FLAG_0();
                                }
                                dynamic stuff = JsonConvert.DeserializeObject(responseText);
                                arch.link = stuff.download_url;
                                arch.zipFile = arch.pathZip + row[dc].ToString() + ".zip";
                                WebClient webClient = new WebClient();
                                webClient.DownloadFile(arch.link, arch.zipFile);
                                arch.allFiles = Directory.GetFiles(arch.pathExtract, "*.csv", SearchOption.AllDirectories);
                                try
                                {
                                    File.Delete(arch.allFiles[0]);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }

                                ZipFile.ExtractToDirectory(arch.zipFile, arch.pathExtract);

                                arch.allFilesCsv =
                                Directory.GetFiles(arch.pathExtract, "*.csv", SearchOption.AllDirectories);

                                File.Move(arch.allFilesCsv[0], arch.pathExtract + row[dc].ToString() + ".csv");

                                arch.csvFile = arch.pathExtract + row[dc].ToString() + ".csv";

                                string text = File.ReadAllText(arch.csvFile);
                                text = text.Replace("\"", "");
                                text = text.Replace(".", "");
                                text = text.Replace(",", ".");
                                File.WriteAllText(arch.csvFile, text);

                                var lineCount = File.ReadLines(arch.csvFile).Count();
                                
                                if (cn.obtenerFlag() == "1")
                                {
                                    InsercionDeDatos ins = new InsercionDeDatos();
                                    ins.insertaDatos(arch.csvFile, lineCount);
                                }
                                Console.WriteLine(arch.link);
                            }
                            cn.EjecutarLog("Correcto Proceso de archivos", "OK PROCESADO");
                        }
                        catch (WebException ex)
                        {
                            cn.EjecutarLog(ex.ToString(), "ERROR PROCESAR ARCHIVOS");
                            EnviarCorreo cr = new EnviarCorreo();
                            cr.CorreoErrores("Se genero un error al momento de procesar los archivos. " + ex, "Error ISV Integracion");
                            Console.WriteLine(ex.Message);
                            Console.ReadKey();
                        }
                    }
                }
            }
            cn.procesadora_analisis().Close();
        }
    }
}
