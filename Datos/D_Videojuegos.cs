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
                    //videojuego.ClasificacionId = Convert.ToInt32(reader["Clasificacion"]);
                    //videojuego.GeneroId = Convert.ToInt32(reader["Genero"]); //No estamos solicitando esto

                    clasificacion.Nombre = Convert.ToString(reader["Clasificacion"]); //Nombre de la clasificacion (en realidad sería NombreC, pero pusimos NombreC AS Clasificacion en el Stored Procedure)

                    //genero.Id = Convert.ToInt32(reader["IdG"]); //Tampoco estamos solicitando esto
                    genero.Nombre = Convert.ToString(reader["Genero"]); //Nombre del genero (en realidad sería NombreG, pero pusimos NombreG AS Genero en el Stored Procedure)

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

        public void EditarVideojuegos(E_Videojuegos videojuego)
        {
            E_Videojuegos juego = new E_Videojuegos();
            try
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("modify_by_id", conexion);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", videojuego.Id);
                cmd.Parameters.AddWithValue("@nombre", videojuego.Nombre);
                cmd.Parameters.AddWithValue("@clasificacionid", videojuego.ClasificacionId);
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

        public void EliminarVideojuegos(int id)
        {
            E_Videojuegos juego = new E_Videojuegos();
            try
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("delete_by_id", conexion);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);

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

        public E_Videojuegos ObtenerVideojuegosPorId(int id)
        {
            E_Videojuegos videojuego = new E_Videojuegos();
            try
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("get_by_id", conexion);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    E_Clasificaciones clasificacion = new E_Clasificaciones();
                    E_Generos genero = new E_Generos();

                    videojuego.Id = Convert.ToInt32(reader["Id"]);
                    videojuego.Nombre = Convert.ToString(reader["Nombre"]);
                    videojuego.ClasificacionId = Convert.ToInt32(reader["ClasificacionId"]); //En el stored procedure sí traemos el ID
                    videojuego.GeneroId = Convert.ToInt32(reader["GeneroId"]); //No estamos solicitando esto (ahora si, pero para el dropdown list)

                    clasificacion.Nombre = Convert.ToString(reader["Clasificacion"]); //Nombre de la clasificacion (en realidad sería NombreC, pero pusimos NombreC AS Clasificacion en el Stored Procedure)

                    //genero.Id = Convert.ToInt32(reader["GeneroId"]); //Tampoco estamos solicitando esto
                    genero.Nombre = Convert.ToString(reader["Genero"]); //Nombre del genero (en realidad sería NombreG, pero pusimos NombreG AS Genero en el Stored Procedure)

                    //Lo equivalente al INNER JOIN
                    videojuego.E_Generos = genero; // A la propiedad compuesta E_Generos (de E_Videojuegos) le asignamos el valor que recuperamos en genero.Nombre

                    videojuego.E_Clasificaciones = clasificacion; // A la propiedad compuesta E_Clasificaciones (de E_Videojuegos) le asignamos el valor que recuperamos en clasificacion.Nombre
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

            return videojuego;
        }

        public List<E_Videojuegos> D_GetByNameOrGenre(string searchText)
        {
            List<E_Videojuegos> videojuegos = new List<E_Videojuegos>();

            try
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("search_by_name_or_genre", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@searchText", searchText);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    E_Videojuegos videojuego = new E_Videojuegos();
                    E_Clasificaciones clasificacion = new E_Clasificaciones();
                    E_Generos genero = new E_Generos();

                    //Experimento: únicamente traer lo que necesitamos mostrar (no llenar todos los atributos del constructor E_Videojuegos) (de hecho es lo único que trae el Stored Procedure)
                    videojuego.Id = Convert.ToInt32(reader["Id"]);
                    videojuego.Nombre = Convert.ToString(reader["Nombre"]);
                    clasificacion.Nombre = Convert.ToString(reader["Clasificacion"]); //Es el nombre con el que renombramos la columna en el Stored Procedure (NombreC AS Clasificacion)
                    genero.Nombre = Convert.ToString(reader["Genero"]); //Es el nombre con el que renombramos la columna en el Stored Procedure (NombreG AS Genero)

                    //Pseudo Inner Join...
                    videojuego.E_Clasificaciones = clasificacion;
                    videojuego.E_Generos = genero;

                    videojuegos.Add(videojuego);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return videojuegos;
        }
    }
}
