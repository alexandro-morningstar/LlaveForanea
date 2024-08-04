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
    public class D_Videojuegos
    {
        //Lo mismo que hacíamos pero en una sola linea se realiza...
        //La string para sql con su referencia a web.config (private string connectionString = ConfigurationManager.ConnectionStrings["sql_RFCDB"].ConnectionString;)
        //La creación del objeto SqlConnection (SqlConnection connection = new SqlConnection(connectionString);)
        private SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString); //Esto es ADO.NET

        public List<E_Videojuegos> ObtenerVideojuegos()
        {
            List<E_Videojuegos> ListaVideojuegos = new List<E_Videojuegos>();
            try
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("get_all", conexion);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    E_Videojuegos videojuego = new E_Videojuegos();
                    E_Clasificaciones clasificacion = new E_Clasificaciones();
                    E_Generos genero = new E_Generos();

                    videojuego.Id = Convert.ToInt32(reader["Id"]);
                    videojuego.Nombre = Convert.ToString(reader["Nombre"]);
                    videojuego.ClasificacionId = Convert.ToInt32(reader["Clasificacion"]);
                    //videojuego.GeneroId = Convert.ToInt32(reader["Genero"]); //No estamos solicitando esto

                    clasificacion.Nombre = Convert.ToString(reader["NombreC"]); //Nombre de la clasificacion

                    //genero.Id = Convert.ToInt32(reader["IdG"]); //Tampoco estamos solicitando esto
                    genero.Nombre = Convert.ToString(reader["Genero"]); //Nombre del genero

                    //Lo equivalente al INNER JOIN
                    videojuego.E_Generos = genero; // Aqui le agregamos a su propiedad compuesta (de videojuego), un objeto del tipo genero

                    videojuego.E_Clasificaciones = clasificacion;

                    ListaVideojuegos.Add(videojuego);
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

            return ListaVideojuegos;
        }

        public void AgregarVideojuegos(E_Videojuegos videojuego)
        {
            E_Videojuegos juego = new E_Videojuegos();
            try
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("create_entry", conexion);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", videojuego.Nombre);
                cmd.Parameters.AddWithValue("@clasificacion", videojuego.ClasificacionId);
                cmd.Parameters.AddWithValue("@generoid", videojuego.GeneroId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}
