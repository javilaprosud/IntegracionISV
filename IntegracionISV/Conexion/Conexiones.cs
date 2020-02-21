using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracionISV.Conexion
{
    class Conexiones
    {
        public string conexion_Prosud_BI = @"Data Source=192.168.1.68;Initial Catalog=PROSUD_BI;Min Pool Size=0;Max Pool Size=10024;Pooling=true;user=sa;pwd=procesadora1";
        public string conexion_Procesadora_Analisis = @"Data Source=192.168.1.68;Initial Catalog=procesadora_analisis;user=sa;pwd=procesadora1";
        public string query_fechas = "select CONVERT(varchar,ParamFecha,120) as ParamFecha from ReprocesoSemanalParam";
        public string query_correo = "select * from VW_CargaISV";
        public string query_parametros = "select mes, ano from ParamAvanceMetas";
        public string query_updateflag_1 = "update FLAG_Metas set FMEstado = 1 , FMFecha = getdate() ";
        public string query_updateflag_0 = "update FLAG_Metas set FMEstado = 0 , FMFecha = getdate() ";
        public string query_select_flag = "select FMEstado from FLAG_Metas";

        public SqlConnection procesadora_analisis()
        {
            SqlConnection sql_conexion = new SqlConnection(conexion_Procesadora_Analisis); 
            sql_conexion.Open();

            return sql_conexion;
        }

        public SqlConnection Prosud_BI_A()
        {
            SqlConnection sql_conexion = new SqlConnection(conexion_Prosud_BI);
            sql_conexion.Open();

            return sql_conexion;
        }

        public void ReprocesoISV()
        {
            string mes = "";
            string ano = ""; 
            SqlCommand command = new SqlCommand(query_parametros, procesadora_analisis());
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                   mes =  reader["mes"].ToString();
                   ano = reader["ano"].ToString();
                }
            }
            using (procesadora_analisis())
            {
                SqlCommand cmd = new SqlCommand("Reproceso_ISV_Mensual", procesadora_analisis());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@mes", SqlDbType.VarChar).Value = mes;
                cmd.Parameters.Add("@ano", SqlDbType.VarChar).Value = ano;
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            procesadora_analisis().Close();
        }

        public void SP_ReprocesoMensual()
        {
            string mes = "";
            string ano = "";
            SqlCommand command = new SqlCommand(query_parametros, procesadora_analisis());
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    mes = reader["mes"].ToString();
                    ano = reader["ano"].ToString();
                }
            }
            using (procesadora_analisis())
            {
                SqlCommand cmd = new SqlCommand("SP_ParamReproMensual", procesadora_analisis());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@mes", SqlDbType.VarChar).Value = mes;
                cmd.Parameters.Add("@ano", SqlDbType.VarChar).Value = ano;
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            procesadora_analisis().Close();
        }

        public void SP_INSERT_Datos(string Fechas, string Holding, string Cadena, string Zona, string ZonaNielsen, string Comuna, string Supervisor, 
            string Mercaderista, string  Local, string RutMercaderista, string Linea, string Categoria, string Marca, string SubCategoria, string Descripcion, 
            string CodigoInterno, string EAN, string DUN, string Vigencia, string CodCadena, string Unidades, string ValoresCostoNeto, 
            string Cajas, string ValVentaB2B, string PVPIVA, string Kilos, string B2BPrecios)
        {
            SqlCommand cmd = new SqlCommand("SP_INSERT_Datos", Prosud_BI_A());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Fechas", SqlDbType.VarChar).Value = Fechas;
            cmd.Parameters.Add("@Holding", SqlDbType.VarChar).Value = Holding;
            cmd.Parameters.Add("@Cadena", SqlDbType.VarChar).Value = Cadena;
            cmd.Parameters.Add("@Zona", SqlDbType.VarChar).Value = Zona;
            cmd.Parameters.Add("@ZonaNielsen", SqlDbType.VarChar).Value = ZonaNielsen;
            cmd.Parameters.Add("@Comuna", SqlDbType.VarChar).Value = Comuna;
            cmd.Parameters.Add("@Supervisor", SqlDbType.VarChar).Value = Supervisor;
            cmd.Parameters.Add("@Mercaderista", SqlDbType.VarChar).Value = Mercaderista;
            cmd.Parameters.Add("@Local", SqlDbType.VarChar).Value = Local;
            cmd.Parameters.Add("@RutMercaderista", SqlDbType.VarChar).Value = RutMercaderista;
            cmd.Parameters.Add("@Linea", SqlDbType.VarChar).Value = Linea;
            cmd.Parameters.Add("@Categoria", SqlDbType.VarChar).Value = Categoria;
            cmd.Parameters.Add("@Marca", SqlDbType.VarChar).Value = Marca;
            cmd.Parameters.Add("@SubCategoria", SqlDbType.VarChar).Value = SubCategoria;
            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion;
            cmd.Parameters.Add("@CodigoInterno", SqlDbType.VarChar).Value = CodigoInterno;
            cmd.Parameters.Add("@EAN", SqlDbType.VarChar).Value = EAN;
            cmd.Parameters.Add("@DUN", SqlDbType.VarChar).Value = DUN;
            cmd.Parameters.Add("@Vigencia", SqlDbType.VarChar).Value = Vigencia;
            cmd.Parameters.Add("@CodCadena", SqlDbType.VarChar).Value = CodCadena;
            cmd.Parameters.Add("@Unidades", SqlDbType.VarChar).Value = Unidades;
            cmd.Parameters.Add("@ValoresCostoNeto", SqlDbType.VarChar).Value = ValoresCostoNeto;
            cmd.Parameters.Add("@Cajas", SqlDbType.VarChar).Value = Cajas;
            cmd.Parameters.Add("@ValVentaB2B", SqlDbType.VarChar).Value = ValVentaB2B;
            cmd.Parameters.Add("@PVPIVA", SqlDbType.VarChar).Value = PVPIVA;
            cmd.Parameters.Add("@Kilos", SqlDbType.VarChar).Value = Kilos;
            cmd.Parameters.Add("@B2BPrecios", SqlDbType.VarChar).Value = B2BPrecios;
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
        }

        public void FLAG_1()
        {
            SqlCommand command = new SqlCommand(query_updateflag_1, procesadora_analisis());
            command.ExecuteReader();
            procesadora_analisis().Close();
        }

        public void FLAG_0()
        {
            SqlCommand command = new SqlCommand(query_updateflag_0, procesadora_analisis());
            command.ExecuteReader();
            procesadora_analisis().Close();
        }

        public string obtenerFlag()
        {
            string flag = "";

            SqlCommand command = new SqlCommand(query_select_flag, procesadora_analisis());
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    flag = reader["FMEstado"].ToString();
                }
            }
            procesadora_analisis().Close();
            return flag; 
        }
    }
}
