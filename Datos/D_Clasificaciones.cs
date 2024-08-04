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
    public class D_Clasificaciones
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;//"Como en el proyecto anterior"
        
        public List<E_Clasificaciones> ObtenerClasificaciones()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion); //"Como en el proyecto anterior"
            List<E_Clasificaciones> ListaClasificaciones = new List<E_Clasificaciones> ();

            try
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("get_all_classifications", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    E_Clasificaciones clasificacion = new E_Clasificaciones();

                    clasificacion.Id = Convert.ToInt32(reader["IdC"]);
                    clasificacion.Nombre = Convert.ToString(reader["NombreC"]);
                    clasificacion.Estatus = Convert.ToBoolean(reader["EstatusC"]);

                    ListaClasificaciones.Add(clasificacion);
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
            return ListaClasificaciones;

            // NOTA: AGREGAR N_Clasificaciones y modificar el front para que traiga el nombre de la clasificacion
        }
    }
}
