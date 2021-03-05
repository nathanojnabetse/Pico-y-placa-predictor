using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pico_y_Placa_predictor.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
    
        /// <summary>
        /// Erro page
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public ActionResult Index(int error = 0)
        {
            switch (error)
            {
                case 505:
                    ViewBag.Title = "Ocurrió un error inesperado";
                    ViewBag.Description = "Esperemos que esto no vuelva a suceder...";
                    break;

                case 404:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL a la que estás intentando ingresar no existe";
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Algo ha salido mal :(";
                    break;
            }

            return View("~/Views/Error/_ErrorPage.cshtml");
        }
    }
}