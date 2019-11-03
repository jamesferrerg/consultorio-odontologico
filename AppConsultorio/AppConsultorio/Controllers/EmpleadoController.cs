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
                                 where empleado.Habilitado==1
                                 select new EmpleadoCLS
                                 {
                                    idempleado = empleado.IdEmpleado,
                                    nombre = empleado.Nombre,
                                    apellido = empleado.Apellido,
                                    //Debe castear(realizar cambios en tipos de datos) por que es fecha, bigint
                                    fechacontrato = (DateTime)empleado.FechaContrato,
                                    direccion = empleado.Direccion,
                                    barrio = empleado.Barrio,
                                    telefono = (long)empleado.Telefono,
                                    celular = (long)empleado.Celular
                                 }).ToList();
            }

            return View(listaEmpleado);
        }
    }
}