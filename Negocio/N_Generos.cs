using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{

    public class N_Generos
    {
        public List<E_Generos> N_ObtenerGeneros()
        {
            List<E_Generos> ListaGeneros = new List<E_Generos>();
            D_Generos D_obteneInador = new D_Generos();

            try
            {
                ListaGeneros = D_obteneInador.ObtenerGeneros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaGeneros;
        }
    }
}
