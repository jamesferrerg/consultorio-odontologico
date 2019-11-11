﻿using System;
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
                lista.Insert(0, new SelectListItem { Text = "--Selecione--", Value = "" });
                ViewBag.listaSexo = lista;
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
                lista.Insert(0, new SelectListItem { Text = "--Selecione--", Value = "" });
                ViewBag.listaContrato = lista;
            }
        }
        
        public ActionResult Index()
        {
            listarComboSexo();
            listarComboContrato();
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
                                    nombreSueldo = sueldos.Cargo,
                                    cantidad = (long)sueldos.Cantidad,
                                    sexoFM = sexos.Sexo,
                                    nombreContrato = tiposContratos.Contrato
                                 }).ToList();
            }

            return View(listaEmpleado);
        }
        public ActionResult Filtro(string nombre)
        {
            List<EmpleadoCLS> listaEmpleado = null;
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                if(nombre==null)
                listaEmpleado = (from empleado in bd.Empleados
                                 join tiposContratos in bd.TiposContratos
                                 on empleado.IdTipoContrato equals tiposContratos.IdTipoContrato
                                 join sexos in bd.Sexos
                                 on empleado.IdSexo equals sexos.IdSexo
                                 join tiposIdentificacion in bd.TiposIdentificacion
                                 on empleado.IdTipoIdentificacion equals tiposIdentificacion.IdTipoIdentificacion
                                 join sueldos in bd.Sueldos
                                 on empleado.IdSueldo equals sueldos.IdSueldo
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
                                     celular = (long)empleado.Celular,
                                     //2. 
                                     identificacion = tiposIdentificacion.Identificacion,
                                     numeroIdentificacion = tiposIdentificacion.Numero,
                                     nombreSueldo = sueldos.Cargo,
                                     cantidad = (long)sueldos.Cantidad,
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
                                     join sueldos in bd.Sueldos
                                     on empleado.IdSueldo equals sueldos.IdSueldo
                                     where empleado.Habilitado == 1
                                     && empleado.Nombre.Contains(nombre)
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
                                         nombreSueldo = sueldos.Cargo,
                                         cantidad = (long)sueldos.Cantidad,
                                         sexoFM = sexos.Sexo,
                                         nombreContrato = tiposContratos.Contrato
                                     }).ToList();
            }
            return PartialView("_InfoEmpleado", listaEmpleado);
        }

        public int Guardar(EmpleadoCLS oEmpleadoCLS, int titulo)
        {
            //rpta es numero de registros afectados
            int rpta = 0;
            
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                if (titulo == 1)
                {
                    Empleados oEmpleado = new Empleados();
                    oEmpleado.Nombre = oEmpleadoCLS.nombre;
                    oEmpleado.Apellido = oEmpleadoCLS.apellido;
                    oEmpleado.FechaContrato = oEmpleadoCLS.fechacontrato;
                    oEmpleado.Direccion = oEmpleadoCLS.direccion;
                    oEmpleado.Barrio = oEmpleadoCLS.barrio;
                    oEmpleado.Telefono = oEmpleadoCLS.telefono;
                    //oEmpleado.IdTipoContrato = oEmpleadoCLS.idtipoIdentificacion;
                    //oEmpleado.IdSexo = oEmpleadoCLS.idSexo;
                    oEmpleado.Habilitado = 1;
                    bd.Empleados.Add(oEmpleado);
                    rpta = bd.SaveChanges();
                }
            }

            return rpta;
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