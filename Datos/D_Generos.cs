using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Generos
    {
        //Lo mismo que hacíamos pero en una sola linea se realiza...
        //La string para sql con su referencia a web.config (private string connectionString = ConfigurationManager.ConnectionStrings["sql_RFCDB"].ConnectionString;)
        //La creación del objeto SqlConnection (SqlConnection connection = new SqlConnection(connectionString);)
        private SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString); //Esto es ADO.NET

        public List<E_Generos> ObtenerGeneros ()
        {
            List<E_Generos> ListaGeneros = new List<E_Generos>();
            try
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("get_all_genres", conexion);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    E_Generos genero = new E_Generos();

                    genero.Id = Convert.ToInt32(reader["IdG"]);
                    genero.Nombre = Convert.ToString(reader["NombreG"]);
                    genero.Estatus = Convert.ToBoolean(reader["Estatus"]);

                    ListaGeneros.Add(genero);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

            return ListaGeneros;
        }
    }
}
