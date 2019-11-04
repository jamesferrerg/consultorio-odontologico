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
                                 join tiposContratos in bd.TiposContratos
                                 on empleado.IdTipoContrato equals tiposContratos.IdTipoContrato
                                 join sexos in bd.Sexos
                                 on empleado.IdSexo equals sexos.IdSexo
                                 join tiposIdentificacion in bd.TiposIdentificacion
                                 on empleado.IdTipoIdentificacion equals tiposIdentificacion.IdTipoIdentificacion
                                 join sueldos in bd.Sueldos
                                 on empleado.IdSueldo equals sueldos.IdSueldo
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
                                    celular = (long)empleado.Celular,
                                    //2. 
                                    identificacion = tiposIdentificacion.Identificacion,
                                    numeroIdentificacion = tiposIdentificacion.Numero,
                                    nombreSueldo = sueldos.Nombre,
                                    cantidad = (long)sueldos.Cantidad,
                                    sexoFM = sexos.Sexo,
                                    nombreContrato = tiposContratos.Nombre
                                 }).ToList();
            }

            return View(listaEmpleado);
        }
    }
}