using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Videojuegos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ClasificacionId { get; set; }
        public int GeneroId { get; set; }

        public E_Generos E_Generos { get; set; } //Esto es lo equivalente al INNER JOIN de SQL, es la referencia a la Clase (tabla) de generos
        public E_Clasificaciones E_Clasificaciones { get; set; } //[Nuevo] equivalente a E_Generos
    }
}