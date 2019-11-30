using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppConsultorio.Models;

namespace AppConsultorio.Controllers
{
    public class PaginaController : Controller
    {
        // GET: Pagina
        public ActionResult Index()
        {
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();

            using(var bd = new ConsultorioOdonBDEntities1())
            {
                listaPagina = (from pagina in bd.Paginas
                               where pagina.Habilitado == 1
                               select new PaginaCLS
                               {
                                   idPagina = pagina.IdPagina,
                                   mensaje = pagina.Mensaje,
                                   accion = pagina.Accion,
                                   controlador = pagina.Controlador
                               }).ToList();
            }

            return View(listaPagina);
        }
    }
}