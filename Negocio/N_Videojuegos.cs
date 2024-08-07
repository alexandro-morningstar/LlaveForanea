using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Videojuegos
    {
        public List<E_Videojuegos> N_ObtenerVideojuegos()
        {
            List<E_Videojuegos> ListaVideojuegos = new List<E_Videojuegos>();
            D_Videojuegos D_obteneInador = new D_Videojuegos();

            try
            {
                ListaVideojuegos = D_obteneInador.ObtenerVideojuegos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaVideojuegos;
        }

        public void N_AgregarVideojuego(E_Videojuegos videojuego)
        {
            D_Videojuegos D_AgregaInador = new D_Videojuegos();

            try
            {
                D_AgregaInador.AgregarVideojuegos(videojuego);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void N_EditarVideojuego(E_Videojuegos videojuego)
        {
            D_Videojuegos D_EditaInador = new D_Videojuegos();

            try
            {
                D_EditaInador.EditarVideojuegos(videojuego);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void N_EliminarVideojuego(int id)
        {
            D_Videojuegos D_EliminaInador = new D_Videojuegos();

            try
            {
                D_EliminaInador.EliminarVideojuegos(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public E_Videojuegos N_ObtenerVideojuegosPorId(int id)
        {
            E_Videojuegos videojuego = new E_Videojuegos();
            D_Videojuegos D_obteneInadorId = new D_Videojuegos();

            try
            {
                videojuego = D_obteneInadorId.ObtenerVideojuegosPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return videojuego;
        }

        public List<E_Videojuegos> N_GetByNameOrGenre(string searchText)
        {
            List<E_Videojuegos> videojuegos = new List<E_Videojuegos>();
            D_Videojuegos N_obteneInadorNameGenre = new D_Videojuegos();

            try
            {
                videojuegos = N_obteneInadorNameGenre.D_GetByNameOrGenre(searchText);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return videojuegos;
        }
    }
}
