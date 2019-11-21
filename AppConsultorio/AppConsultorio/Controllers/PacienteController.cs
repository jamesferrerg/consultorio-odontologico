using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppConsultorio.Models;

namespace AppConsultorio.Controllers
{
    public class PacienteController : Controller
    {
        // GET: Paciente
        public ActionResult Index()
        {
            List<PacienteCLS> listaPaciente = new List<PacienteCLS>();
            try
            {
                using(var bd = new ConsultorioOdonBDEntities1())
                {
                    listaPaciente = (from paciente in bd.Pacientes
                                     join tipoIdentificacion in bd.TiposIdentificacion
                                     on paciente.IdTipoIdentificacion equals tipoIdentificacion.IdTipoIdentificacion
                                     where paciente.Habilitado == 1
                                     select new PacienteCLS
                                     {
                                         idPaciente = paciente.IdPaciente,
                                         nombre = paciente.Nombre,
                                         apellido = paciente.Apellido,
                                         numeroIdentificacion = paciente.NumeroIdentificacion,
                                         celular = (long)paciente.Celular
                                     }).ToList();
                                    
                }
            }
            catch (Exception ex)
            {
                listaPaciente = new List<PacienteCLS>();
            }
            return View(listaPaciente);
        }

        public ActionResult Filtro (string nombrePaciente)
        {
            List<PacienteCLS> listaPaciente = new List<PacienteCLS>();
            try
            {
                using (var bd = new ConsultorioOdonBDEntities1())
                {
                    if (nombrePaciente == null)
                    {
                        listaPaciente = (from paciente in bd.Pacientes
                                         join tipoIdentificacion in bd.TiposIdentificacion
                                         on paciente.IdTipoIdentificacion equals tipoIdentificacion.IdTipoIdentificacion
                                         where paciente.Habilitado == 1
                                         select new PacienteCLS
                                         {
                                             idPaciente = paciente.IdPaciente,
                                             nombre = paciente.Nombre,
                                             apellido = paciente.Apellido,
                                             numeroIdentificacion = paciente.NumeroIdentificacion,
                                             celular = (long)paciente.Celular
                                         }).ToList();
                    }
                    else
                    {
                        listaPaciente = (from paciente in bd.Pacientes
                                         join tipoIdentificacion in bd.TiposIdentificacion
                                         on paciente.IdTipoIdentificacion equals tipoIdentificacion.IdTipoIdentificacion
                                         where paciente.Habilitado == 1
                                         && paciente.Nombre.Contains(nombrePaciente)
                                         select new PacienteCLS
                                         {
                                             idPaciente = paciente.IdPaciente,
                                             nombre = paciente.Nombre,
                                             apellido = paciente.Apellido,
                                             numeroIdentificacion = paciente.NumeroIdentificacion,
                                             celular = (long)paciente.Celular
                                         }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                listaPaciente = new List<PacienteCLS>();
            }
            return PartialView("_TablaPaciente", listaPaciente);
        }

        //Generando una vista completa de la tabla Paciente

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
                lista.Insert(0, new SelectListItem { Text = " - Seleccionar - ", Value = "" });
                ViewBag.listaSexo = lista;
            }
        }

        public void listarComboDepartamento()
        {
            List<SelectListItem> lista;
            using(var bd = new ConsultorioOdonBDEntities1())
            {
                lista = (from item in bd.Departamentos
                         where item.Habilitado == 1
                         select new SelectListItem
                         {
                             Text = item.Departamento,
                             Value = item.IdDepartamento.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = " - Seleccionar - ", Value = "" });
                ViewBag.listaDepartamento = lista;
            }
        }

        public void listarComboTipoIdentificacion()
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
                lista.Insert(0, new SelectListItem { Text = " - Seleccionar - ", Value = "" });
                ViewBag.listaTipoIdentificacion = lista;
            }
        }
        
        public void listarCombos()
        {
            listarComboSexo();
            listarComboDepartamento();
            listarComboTipoIdentificacion();
        }
        public ActionResult Agregar()
        {
            listarCombos();
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(PacienteCLS oPacienteCLS)
        {
            if (!ModelState.IsValid)
            {
                listarCombos();
                return View(oPacienteCLS);
            }
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                Pacientes oPaciente = new Pacientes();
                oPaciente.Nombre = oPacienteCLS.nombre;
                oPaciente.Apellido = oPacienteCLS.apellido;
                oPaciente.NumeroIdentificacion = oPacienteCLS.numeroIdentificacion;
                oPaciente.FechaNacimiento = oPacienteCLS.fechaNacimiento;
                oPaciente.Edad = oPacienteCLS.edad;
                oPaciente.Email = oPacienteCLS.email;
                oPaciente.Direccion = oPacienteCLS.direccion;
                oPaciente.Barrio = oPacienteCLS.barrio;
                oPaciente.Telefono = oPacienteCLS.telefono;
                oPaciente.Celular = oPacienteCLS.celular;
                oPaciente.Ocupacion = oPacienteCLS.ocupacion;
                oPaciente.Aseguradora = oPacienteCLS.aseguradora;
                oPaciente.Vinculacion = oPacienteCLS.vinculacion;
                oPaciente.MotivoConsulta = oPacienteCLS.motivoConsulta;
                oPaciente.IdSexo = oPacienteCLS.idSexo;
                oPaciente.IdDepartamento = oPacienteCLS.idDepartamento;
                oPaciente.IdTipoIdentificacion = oPacienteCLS.idTipoIdentificacion;
                oPaciente.Habilitado = 1;

                bd.Pacientes.Add(oPaciente);
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Mostrar informacion completa de la tabla empleado a traves del boton
        public ActionResult InfoCompleto(int id)
        {
            listarCombos();
            PacienteCLS oPacienteCls = new PacienteCLS();
            try
            {
                if (!ModelState.IsValid)
                {
                    listarCombos();
                    return View(oPacienteCls);
                }
                using (var bd = new ConsultorioOdonBDEntities1())
                {
                    Pacientes oPaciente = bd.Pacientes.Where(p => p.IdPaciente.Equals(id)).First();
                    oPacienteCls.idPaciente = oPaciente.IdPaciente;
                    oPacienteCls.nombre = oPaciente.Nombre;
                    oPacienteCls.apellido = oPaciente.Apellido;
                    oPacienteCls.numeroIdentificacion = oPaciente.NumeroIdentificacion;
                    oPacienteCls.fechaNacimiento = (DateTime)oPaciente.FechaNacimiento;
                    oPacienteCls.edad = (int)oPaciente.Edad;
                    oPacienteCls.email = oPaciente.Email;
                    oPacienteCls.direccion = oPaciente.Direccion;
                    oPacienteCls.barrio = oPaciente.Barrio;
                    oPacienteCls.telefono = (long)oPaciente.Telefono;
                    oPacienteCls.celular = (long)oPaciente.Celular;
                    oPacienteCls.ocupacion = oPaciente.Ocupacion;
                    oPacienteCls.aseguradora = oPaciente.Aseguradora;
                    oPacienteCls.vinculacion = oPaciente.Vinculacion;
                    oPacienteCls.motivoConsulta = oPaciente.MotivoConsulta;
                    oPacienteCls.idSexo = (int)oPaciente.IdSexo;
                    oPacienteCls.idDepartamento = (int)oPaciente.IdDepartamento;
                    oPacienteCls.idTipoIdentificacion = (int)oPaciente.IdTipoIdentificacion;

                    oPacienteCls.sexoFM = oPaciente.Sexos.Sexo;
                    oPacienteCls.identificacionTipo = oPaciente.TiposIdentificacion.TipoIdentificacion;
                    oPacienteCls.departamentoLugar = oPaciente.Departamentos.Departamento;

                }
            }
            catch (Exception ex)
            {
                oPacienteCls = new PacienteCLS();
            }
            return View(oPacienteCls);
        }

        //Editar informacion
        public ActionResult Editar(int id)
        {
            listarCombos();
            PacienteCLS oPacienteCls = new PacienteCLS();
            try
            {
                using (var bd = new ConsultorioOdonBDEntities1())
                {
                    Pacientes oPaciente = bd.Pacientes.Where(p => p.IdPaciente.Equals(id)).First();
                    oPacienteCls.idPaciente = oPaciente.IdPaciente;
                    oPacienteCls.nombre = oPaciente.Nombre;
                    oPacienteCls.apellido = oPaciente.Apellido;
                    oPacienteCls.numeroIdentificacion = oPaciente.NumeroIdentificacion;
                    oPacienteCls.fechaNacimiento = (DateTime)oPaciente.FechaNacimiento;
                    oPacienteCls.edad = (int)oPaciente.Edad;
                    oPacienteCls.email = oPaciente.Email;
                    oPacienteCls.direccion = oPaciente.Direccion;
                    oPacienteCls.barrio = oPaciente.Barrio;
                    oPacienteCls.telefono = (long)oPaciente.Telefono;
                    oPacienteCls.celular = (long)oPaciente.Celular;
                    oPacienteCls.ocupacion = oPaciente.Ocupacion;
                    oPacienteCls.aseguradora = oPaciente.Aseguradora;
                    oPacienteCls.vinculacion = oPaciente.Vinculacion;
                    oPacienteCls.motivoConsulta = oPaciente.MotivoConsulta;
                    oPacienteCls.idSexo = (int)oPaciente.IdSexo;
                    oPacienteCls.idDepartamento = (int)oPaciente.IdDepartamento;
                    oPacienteCls.idTipoIdentificacion = (int)oPaciente.IdTipoIdentificacion;

                    oPacienteCls.sexoFM = oPaciente.Sexos.Sexo;
                    oPacienteCls.identificacionTipo = oPaciente.TiposIdentificacion.TipoIdentificacion;
                    oPacienteCls.departamentoLugar = oPaciente.Departamentos.Departamento;

                }
            }
            catch (Exception ex)
            {
                oPacienteCls = new PacienteCLS();
            }
            return View(oPacienteCls);
        }

        [HttpPost]
        public ActionResult Editar(PacienteCLS oPacienteCls)
        {
            int id = oPacienteCls.idPaciente;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oPacienteCls);
                }
                using (var bd = new ConsultorioOdonBDEntities1())
                {
                    Pacientes oPaciente = bd.Pacientes.Where(p => p.IdPaciente.Equals(id)).First();
                    oPaciente.IdPaciente = oPacienteCls.idPaciente;
                    oPaciente.Nombre = oPacienteCls.nombre;
                    oPaciente.Apellido = oPacienteCls.apellido;
                    oPaciente.NumeroIdentificacion = oPacienteCls.numeroIdentificacion;
                    oPaciente.FechaNacimiento = oPacienteCls.fechaNacimiento;
                    oPaciente.Edad = oPacienteCls.edad;
                    oPaciente.Email = oPacienteCls.email;
                    oPaciente.Direccion = oPacienteCls.direccion;
                    oPaciente.Barrio = oPacienteCls.barrio;
                    oPaciente.Telefono = oPacienteCls.telefono;
                    oPaciente.Celular = oPacienteCls.celular;
                    oPaciente.Ocupacion = oPacienteCls.ocupacion;
                    oPaciente.Aseguradora = oPacienteCls.aseguradora;
                    oPaciente.Vinculacion = oPacienteCls.vinculacion;
                    oPaciente.MotivoConsulta = oPacienteCls.motivoConsulta;
                    oPaciente.IdSexo = oPacienteCls.idSexo;
                    oPaciente.IdDepartamento = oPacienteCls.idDepartamento;
                    oPaciente.IdTipoIdentificacion = oPacienteCls.idTipoIdentificacion;

                    bd.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                oPacienteCls = new PacienteCLS();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                using (var bd = new ConsultorioOdonBDEntities1())
                {
                    Pacientes oPaciente = bd.Pacientes.Where(p => p.IdPaciente.Equals(id)).First();
                    oPaciente.Habilitado = 0;
                    bd.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar - "+ ex.Message);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}