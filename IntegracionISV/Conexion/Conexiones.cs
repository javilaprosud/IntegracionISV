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
        public string conexion_Prosud_BI = @"Data Source=192.168.1.68;Initial Catalog=PROSUD_BI;Max Pool Size=10024;user=sa;pwd=procesadora1";
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
                cmd.ExecuteNonQuery();
            }
        }

        public void FLAG_1()
        {
            SqlCommand command = new SqlCommand(query_updateflag_1, procesadora_analisis());
            command.ExecuteReader();
        }

        public void FLAG_0()
        {
            SqlCommand command = new SqlCommand(query_updateflag_0, procesadora_analisis());
            command.ExecuteReader();
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

            return flag; 
        }
    }
}
