using Datos;
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
            try
            {
                ListaGeneros = negocio_g.N_ObtenerGeneros();
                ViewBag.GeneroId = new SelectList(ListaGeneros, "Id", "NombreG");
                return View("Agregar", ListaGeneros);
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
    }
}