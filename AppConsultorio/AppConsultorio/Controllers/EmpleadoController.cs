using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppConsultorio.Models;

namespace AppConsultorio.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {
            List<EmpleadoCLS> listaEmpleado = null;
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                listaEmpleado = (from empleado in bd.Empleados
                                 select new EmpleadoCLS
                                 {
                                    idempleado = empleado.IdEmpleado,
                                    nombre = empleado.Nombre,
                                    apellido = empleado.Apellido,
                                    //fechacontrato = Convert.ToDateTime(empleado.FechaContrato),
                                    direccion = empleado.Direccion,
                                    barrio = empleado.Barrio
                                    //telefono = Convert.ToInt32(empleado.Telefono),
                                    //celular = Convert.ToInt32(empleado.Celular)
                                 }).ToList();
            }

            return View(listaEmpleado);
        }
    }
}