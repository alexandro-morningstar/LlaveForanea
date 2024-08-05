using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Clasificaciones
    {
        public List<E_Clasificaciones> N_ObtenerClasificaciones()
        {
            List<E_Clasificaciones> ListaClasificaciones = new List<E_Clasificaciones>();
            D_Clasificaciones D_obteneInador = new D_Clasificaciones();

            try
            {
                ListaClasificaciones = D_obteneInador.ObtenerClasificaciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListaClasificaciones;
        }

    }
}
