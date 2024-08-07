using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebLlaveForanea.Controllers
{
    public class HomeController : Controller
    {
        N_Generos negocio_g = new N_Generos();
        N_Videojuegos negocio_v = new N_Videojuegos();
        N_Clasificaciones negocio_c = new N_Clasificaciones();
        // GET: Home
        public ActionResult Index()
        {
            List<E_Videojuegos> ListaVideojuegos = new List<E_Videojuegos>();
            try
            {
                ListaVideojuegos = negocio_v.N_ObtenerVideojuegos();
                return View("Index", ListaVideojuegos);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Index", ListaVideojuegos);
            }
        }

        public ActionResult IrAgregar()
        {
            List<E_Generos> ListaGeneros = new List<E_Generos>();
            List<E_Clasificaciones> ListaClasificaciones = new List<E_Clasificaciones>();
            try
            {
                ListaGeneros = negocio_g.N_ObtenerGeneros(); //Recuperamos todos los generos (lista de generos)
                ListaClasificaciones = negocio_c.N_ObtenerClasificaciones();

                //ViewBag.Nombre (El .Nombre tiene que ser igual al nombre de la propiedad a la que hacemos referencia, de quen queremos obtener o mandar datos)
                // A través de un ViewBag que se llama GeneroId, voy a pasar info al HTML (un objeto SelectList)
                // El primer parametro (ListaGeneros) es quien da la información para llenar la lista
                // "Id" y "NombreG" son las propiedades de E_Genero, de donde obtendrá la info.
                ViewBag.GeneroId = new SelectList(ListaGeneros, "Id", "Nombre");
                ViewBag.ClasificacionId = new SelectList(ListaClasificaciones, "Id", "Nombre");
                return View("Agregar"); //El ViewBag no es necesario pasarlo como Modelo, es como un TempData
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult AgregarVideojuego(E_Videojuegos videojuego)
        {
            try
            {
                negocio_v.N_AgregarVideojuego(videojuego);
                TempData["success"] = $"El videojuego {videojuego.Nombre} se ha agregado exitosamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult IrEditar(int id)
        {
            E_Videojuegos videojuegoEditar = new E_Videojuegos();
            List<E_Generos> ListaGeneros = new List<E_Generos>();
            List<E_Clasificaciones> ListaClasificaciones = new List<E_Clasificaciones> ();

            try
            {
                ListaGeneros = negocio_g.N_ObtenerGeneros(); //Recuperamos todos los generos (lista de generos)
                ListaClasificaciones = negocio_c.N_ObtenerClasificaciones();
                
                videojuegoEditar = negocio_v.N_ObtenerVideojuegosPorId(id);

                ViewBag.GeneroId = new SelectList(ListaGeneros, "Id", "Nombre", videojuegoEditar.GeneroId);
                ViewBag.ClasificacionId = new SelectList(ListaClasificaciones, "Id", "Nombre", videojuegoEditar.ClasificacionId);

                return View("VistaEditar", videojuegoEditar);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult EditarPost(E_Videojuegos videojuegoEditar)
        {
            try
            {
                negocio_v.N_EditarVideojuego(videojuegoEditar);
                TempData["success"] = $"El videojuego {videojuegoEditar.Nombre} se ha editado exitosamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult IrEliminar(int id)
        {
            E_Videojuegos videojuegoEliminar = new E_Videojuegos();

            try
            {
                videojuegoEliminar = negocio_v.N_ObtenerVideojuegosPorId(id);
                return View("VistaEliminar", videojuegoEliminar);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                negocio_v.N_EliminarVideojuego(id);
                TempData["success"] = $"El videojuego con ID: {id} fue eliminado exitosamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Buscar(string searchText)
        {
            List<E_Videojuegos> videojuegos = new List<E_Videojuegos>();

            try
            {
                videojuegos = negocio_v.N_GetByNameOrGenre(searchText);
                return View("Index", videojuegos);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}