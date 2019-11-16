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
        public void listarComboSexo()
        {
            //agregar
            List<SelectListItem> lista;
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                lista = (from item in bd.Sexos
                         where item.Habilitado == 1
                         select new SelectListItem
                         {
                             Text = item.Sexo,
                             Value = item.IdSexo.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "-- Selecione --", Value = "" });
                ViewBag.listaSexo = lista;
            }
        }
        public void listarComboIdentificacion()
        {
            List<SelectListItem> lista;
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                lista = (from item in bd.TiposIdentificacion
                         where item.Habilitado == 1
                         select new SelectListItem
                         {
                             Text = item.TipoIdentificacion,
                             Value = item.IdTipoIdentificacion.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "-- Selecione --", Value = "" });
                ViewBag.listaIdentificacion = lista;
            }
        }
        public void listarComboContrato()
        {
            List<SelectListItem> lista;
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                lista = (from item in bd.TiposContratos
                         where item.Habilitado == 1
                         select new SelectListItem
                         {
                             Text = item.Contrato,
                             Value = item.IdTipoContrato.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "-- Selecione --", Value = "" });
                ViewBag.listaContrato = lista;
            }
        }
        public void listarComboCargo()
        {
            List<SelectListItem> lista;
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                lista = (from item in bd.Cargos
                         where item.Habilitado == 1
                         select new SelectListItem
                         {
                             Text = item.Cargo,
                             Value = item.IdCargo.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "-- Selecione --", Value = "" });
                ViewBag.listaCargo = lista;
            }
        }

        public ActionResult Index()
        {
            listarComboSexo();
            listarComboContrato();
            listarComboCargo();
            listarComboIdentificacion();
            //List<EmpleadoCLS> listaEmpleado = null;
            List<EmpleadoCLS> listaEmpleado = new List<EmpleadoCLS>();
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                listaEmpleado = (from empleado in bd.Empleados
                                 join tiposContratos in bd.TiposContratos
                                 on empleado.IdTipoContrato equals tiposContratos.IdTipoContrato
                                 join sexos in bd.Sexos
                                 on empleado.IdSexo equals sexos.IdSexo
                                 join tiposIdentificacion in bd.TiposIdentificacion
                                 on empleado.IdTipoIdentificacion equals tiposIdentificacion.IdTipoIdentificacion
                                 join cargos in bd.Cargos
                                 on empleado.IdCargo equals cargos.IdCargo
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
                                    numeroIdentificacion = empleado.NumeroIdentificacion,
                                    //2. 
                                    tipoIdentificacion = tiposIdentificacion.TipoIdentificacion,
                                    nombreCargo = cargos.Cargo,
                                    salario = (long)cargos.Salario,
                                    sexoFM = sexos.Sexo,
                                    nombreContrato = tiposContratos.Contrato
                                 }).ToList();
            }

            return View(listaEmpleado);
        }

        public string Guardar(EmpleadoCLS oEmpleadoCLS, int titulo)
        {
            //rpta es numero de registros afectados
            string rpta = "";
            try
            {
                if(!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class='list-group'>";

                    foreach(var item in query)
                    {
                        rpta += "<li class='list-group-item list-group-item-action list-group-item-danger'>" + item+"</li>";
                    }

                    rpta += "</ul>";
                }
                else
                {
                    using (var bd = new ConsultorioOdonBDEntities1())
                    {
                        if (titulo == -1)
                        {
                            //Agrego
                            Empleados oEmpleado = new Empleados();
                            oEmpleado.Nombre = oEmpleadoCLS.nombre;
                            oEmpleado.Apellido = oEmpleadoCLS.apellido;
                            oEmpleado.FechaContrato = oEmpleadoCLS.fechacontrato;
                            oEmpleado.Direccion = oEmpleadoCLS.direccion;
                            oEmpleado.Barrio = oEmpleadoCLS.barrio;
                            oEmpleado.Telefono = oEmpleadoCLS.telefono;
                            oEmpleado.Celular = oEmpleadoCLS.celular;
                            oEmpleado.NumeroIdentificacion = oEmpleadoCLS.numeroIdentificacion;
                            oEmpleado.IdTipoContrato = oEmpleadoCLS.idtipoContrato;
                            oEmpleado.IdSexo = oEmpleadoCLS.idSexo;
                            oEmpleado.IdTipoIdentificacion = oEmpleadoCLS.idtipoIdentificacion;
                            oEmpleado.IdCargo = oEmpleadoCLS.idCargo;
                            oEmpleado.Habilitado = 1;
                            bd.Empleados.Add(oEmpleado);
                            rpta = bd.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                        }
                        else
                        {
                            //Edito
                            Empleados oEmpleado = bd.Empleados.Where(p => p.IdEmpleado == titulo).First();
                            oEmpleado.Nombre = oEmpleadoCLS.nombre;
                            oEmpleado.Apellido = oEmpleadoCLS.apellido;
                            oEmpleado.FechaContrato = oEmpleadoCLS.fechacontrato;
                            oEmpleado.Direccion = oEmpleadoCLS.direccion;
                            oEmpleado.Barrio = oEmpleadoCLS.barrio;
                            oEmpleado.Telefono = oEmpleadoCLS.telefono;
                            oEmpleado.Celular = oEmpleadoCLS.celular;
                            oEmpleado.NumeroIdentificacion = oEmpleadoCLS.numeroIdentificacion;
                            oEmpleado.IdTipoContrato = oEmpleadoCLS.idtipoContrato;
                            oEmpleado.IdSexo = oEmpleadoCLS.idSexo;
                            oEmpleado.IdTipoIdentificacion = oEmpleadoCLS.idtipoIdentificacion;
                            oEmpleado.IdCargo = oEmpleadoCLS.idCargo;
                            rpta = bd.SaveChanges().ToString();
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                rpta = "";
            }
            

            return rpta;
        }
        //Recuperar informacion para la edicion
        public JsonResult recuperarDatos(int titulo)
        {
            listarComboSexo();
            listarComboContrato();
            listarComboCargo();
            listarComboIdentificacion();
            EmpleadoCLS oEmpleadoCLS = new EmpleadoCLS();
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                Empleados oEmpleado = bd.Empleados.Where(p => p.IdEmpleado == titulo).First();
                oEmpleadoCLS.nombre = oEmpleado.Nombre;
                oEmpleadoCLS.apellido = oEmpleado.Apellido;
                oEmpleadoCLS.fechacontrato = (DateTime)oEmpleado.FechaContrato;
                oEmpleadoCLS.direccion = oEmpleado.Direccion;
                oEmpleadoCLS.barrio = oEmpleado.Barrio;
                oEmpleadoCLS.telefono = (long)oEmpleado.Telefono;
                oEmpleadoCLS.celular = (long)oEmpleado.Celular;
                oEmpleadoCLS.numeroIdentificacion = oEmpleado.NumeroIdentificacion;
                oEmpleadoCLS.idtipoContrato = oEmpleado.TiposContratos.IdTipoContrato;
                oEmpleadoCLS.idSexo = oEmpleado.Sexos.IdSexo;
                oEmpleadoCLS.idtipoIdentificacion = oEmpleado.TiposIdentificacion.IdTipoIdentificacion;
                oEmpleadoCLS.idCargo = oEmpleado.Cargos.IdCargo;
            }
            return Json(oEmpleadoCLS, JsonRequestBehavior.AllowGet);
        }

        //Eliminar
        public string eliminar (EmpleadoCLS oEmpleadoCls)
        {
            string rpta = "";
            try
            {
                int idEmpleado = oEmpleadoCls.idempleado;
                using(var bd = new ConsultorioOdonBDEntities1())
                {
                    Empleados oEmpleado = bd.Empleados.Where(p => p.IdEmpleado == idEmpleado).First();
                    oEmpleado.Habilitado = 0;
                    rpta = bd.SaveChanges().ToString();
                }
            }
            catch (Exception ex)
            {
                rpta = "";
            }
            return rpta;
        }
        public ActionResult Filtro(string nombreEmpleado)
        {
            List<EmpleadoCLS> listaEmpleado = null;
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                if (nombreEmpleado == null)
                    listaEmpleado = (from empleado in bd.Empleados
                                     join tiposContratos in bd.TiposContratos
                                     on empleado.IdTipoContrato equals tiposContratos.IdTipoContrato
                                     join sexos in bd.Sexos
                                     on empleado.IdSexo equals sexos.IdSexo
                                     join tiposIdentificacion in bd.TiposIdentificacion
                                     on empleado.IdTipoIdentificacion equals tiposIdentificacion.IdTipoIdentificacion
                                     join cargos in bd.Cargos
                                     on empleado.IdCargo equals cargos.IdCargo
                                     where empleado.Habilitado == 1
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
                                         numeroIdentificacion = empleado.NumeroIdentificacion,
                                         //2. 
                                         tipoIdentificacion = tiposIdentificacion.TipoIdentificacion,
                                         nombreCargo = cargos.Cargo,
                                         salario = (long)cargos.Salario,
                                         sexoFM = sexos.Sexo,
                                         nombreContrato = tiposContratos.Contrato
                                     }).ToList();
                else
                    listaEmpleado = (from empleado in bd.Empleados
                                     join tiposContratos in bd.TiposContratos
                                     on empleado.IdTipoContrato equals tiposContratos.IdTipoContrato
                                     join sexos in bd.Sexos
                                     on empleado.IdSexo equals sexos.IdSexo
                                     join tiposIdentificacion in bd.TiposIdentificacion
                                     on empleado.IdTipoIdentificacion equals tiposIdentificacion.IdTipoIdentificacion
                                     join cargos in bd.Cargos
                                     on empleado.IdCargo equals cargos.IdCargo
                                     where empleado.Habilitado == 1
                                     && empleado.Nombre.Contains(nombreEmpleado)
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
                                         numeroIdentificacion = empleado.NumeroIdentificacion,
                                         //2. 
                                         tipoIdentificacion = tiposIdentificacion.TipoIdentificacion,
                                         nombreCargo = cargos.Cargo,
                                         salario = (long)cargos.Salario,
                                         sexoFM = sexos.Sexo,
                                         nombreContrato = tiposContratos.Contrato
                                     }).ToList();
            }
            return PartialView("_InfoEmpleado", listaEmpleado);
        }

        //[HttpPost]
        //public ActionResult Agregar(EmpleadoCLS oEmpleadoCLS)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return View(oEmpleadoCLS);
        //    }
        //    else
        //    {
        //        using (var bd = new ConsultorioOdonBDEntities1())
        //        {
        //            Empleados oEmpleado = new Empleados();
        //            oEmpleado.Nombre = oEmpleadoCLS.nombre;
        //            oEmpleado.Apellido = oEmpleadoCLS.apellido;
        //            oEmpleado.FechaContrato = oEmpleadoCLS.fechacontrato;
        //            oEmpleado.Direccion = oEmpleadoCLS.direccion;
        //            oEmpleado.Barrio = oEmpleadoCLS.barrio;
        //            oEmpleado.Telefono = oEmpleadoCLS.telefono;
        //            oEmpleado.Sexos.Sexo = oEmpleadoCLS.sexoFM;
        //            oEmpleado.TiposIdentificacion.Identificacion = oEmpleadoCLS.identificacion;
        //            oEmpleado.TiposIdentificacion.Numero = oEmpleadoCLS.numeroIdentificacion;
        //            oEmpleado.Sueldos.Cargo = oEmpleadoCLS.nombreSueldo;
        //            oEmpleado.Sueldos.Cantidad = oEmpleadoCLS.cantidad;
        //            oEmpleado.TiposContratos.Contrato = oEmpleadoCLS.nombreContrato;
        //            bd.Empleados.Add(oEmpleado);
        //            bd.SaveChanges();


        //        }
        //    }


        //    return RedirectToAction("Index");
        //}
    }
}