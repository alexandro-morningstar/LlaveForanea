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
    }
}
