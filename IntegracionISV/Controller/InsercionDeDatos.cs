using NotVisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracionISV.Controller
{
    class InsercionDeDatos
    {
        public void insertaDatos(string csv_file, int lineas)
        {
            Conexion.Conexiones cn = new Conexion.Conexiones(); 
            DataTable csvData = new DataTable();
            using (CsvTextFieldParser csvReader = new CsvTextFieldParser(csv_file))
            {
                csvReader.Delimiters = new string[] { ";" };
                csvReader.HasFieldsEnclosedInQuotes = true;
                //read column names
                string[] colFields = csvReader.ReadFields();
                foreach (string column in colFields)
                {
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn);
                }
                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();
                    //Making empty value as null
                    csvData.Rows.Add(fieldData);
                }
                using (cn.Prosud_BI_A())
                {
                    for (int i = 0; csvData.Rows.Count > i; i++)
                    {
                        cn.SP_INSERT_Datos(
                            csvData.Rows[i].ItemArray.GetValue(0).ToString(),
                            csvData.Rows[i].ItemArray.GetValue(1).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(2).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(3).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(4).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(5).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(6).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(7).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(8).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(9).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(10).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(11).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(12).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(13).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(14).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(15).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(16).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(17).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(18).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(19).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(20).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(21).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(22).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(23).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(24).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(25).ToString() ,
                            csvData.Rows[i].ItemArray.GetValue(26).ToString() );
                        #region
                        //string query = "insert into Prosud_BI.dbo.ISV_Ventas (Fechas, Holding, Cadena, Zona, ZonaNielsen, Comuna, Supervisor, Mercaderista, Local, RutMercaderista, Linea, Categoria, Marca, SubCategoria, Descripcion, CodigoInterno, EAN, DUN, Vigencia, CodCadena, Unidades, ValoresCostoNeto, Cajas, ValVentaB2B, PVPIVA, Kilos, B2BPrecios) values ('" + csvData.Rows[i].ItemArray.GetValue(0).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(1).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(2).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(3).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(4).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(5).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(6).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(7).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(8).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(9).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(10).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(11).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(12).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(13).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(14).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(15).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(16).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(17).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(18).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(19).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(20).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(21).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(22).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(23).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(24).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(25).ToString() + "','" +
                        //    csvData.Rows[i].ItemArray.GetValue(26).ToString() + "');";
                        //SqlCommand command = new SqlCommand(query, cn.Prosud_BI_A());
                        //command.ExecuteNonQuery();
                        #endregion
                        cn.Prosud_BI_A().Close();
                    }
                }
            }
        }
    }
}
