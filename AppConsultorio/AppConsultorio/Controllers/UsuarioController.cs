using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppConsultorio.Models;
using System.Transactions;
using System.Security.Cryptography;
using System.Text;
using AppConsultorio.Filters;

namespace AppConsultorio.Controllers
{
    [Acceder]
    public class UsuarioController : Controller
    {
        public void listaPersonas()
        {
            List<SelectListItem> listaPersonas = new List<SelectListItem>();
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                List<SelectListItem> listaEmpleados = (from item in bd.Empleados
                                                       where item.Habilitado == 1 && item.TieneUsuario != 1
                                                       select new SelectListItem
                                                       {
                                                           Text = item.Nombre + " " + item.Apellido,
                                                           Value = item.IdEmpleado.ToString()
                                                       }).ToList();

                listaPersonas.AddRange(listaEmpleados);
                listaPersonas = listaPersonas.OrderBy(p => p.Text).ToList();
                listaPersonas.Insert(0, new SelectListItem { Text = " - Seleccione - ", Value = "" });
                ViewBag.listaPersona = listaPersonas;
            }
        }

        public void listarRol()
        {
            List<SelectListItem> listaRol;
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                listaRol = (from item in bd.Roles
                            where item.Habilitado == 1
                            select new SelectListItem
                            {
                                Text = item.NombreR,
                                Value = item.IdRol.ToString()
                            }).ToList();

                listaRol.Insert(0, new SelectListItem { Text = " - Seleccione - ", Value = "" });
                ViewBag.listaRol = listaRol;
            }
        }

        public void listarTipoUsuario()
        {
            List<SelectListItem> listaTipoUsuario;
            using(var bd= new ConsultorioOdonBDEntities1())
            {
                listaTipoUsuario = (from item in bd.TiposUsuarios
                                    where item.Habilitado == 1
                                    select new SelectListItem
                                    {
                                        Text = item.Nombre,
                                        Value = item.IdTipoUsuario.ToString()
                                    }).ToList();

                listaTipoUsuario.Insert(0, new SelectListItem { Text = " - Seleccione - ", Value = "" });
                ViewBag.listaTipoUsuario = listaTipoUsuario;
            }
        }
        
        public void listarCombos()
        {
            listaPersonas();
            listarRol();
            listarTipoUsuario();
        }
        // GET: Usuario
        public ActionResult Index()
        {
            listarCombos();
            List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                listaUsuario = (from usuario in bd.Usuarios
                                join empleado in bd.Empleados
                                on usuario.IdAnon equals empleado.IdEmpleado
                                join rol in bd.Roles
                                on usuario.IdRol equals rol.IdRol
                                join tipoUsuario in bd.TiposUsuarios
                                on usuario.IdTipoUsuario equals tipoUsuario.IdTipoUsuario
                                where usuario.Habilitado == 1
                                select new UsuarioCLS
                                {
                                    idUsuario = usuario.IdUsuario,
                                    nombreEmpleado = empleado.Nombre + " " + empleado.Apellido,
                                    nombreU = usuario.NombreU,
                                    nombreRol = rol.NombreR,
                                    nombreTipoUsuario = tipoUsuario.Nombre
                                }).ToList();

                listaUsuario = listaUsuario.OrderBy(p => p.idUsuario).ToList();
            }


            return View(listaUsuario);
        }

        public ActionResult Filtrar(UsuarioCLS oUsuarioCls)
        {
            string nombreEmpleado = oUsuarioCls.nombreEmpleado;

            listarCombos();
            List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();
            using (var bd = new ConsultorioOdonBDEntities1())
            {
                if (oUsuarioCls.nombreEmpleado == null)
                {
                    listaUsuario = (from usuario in bd.Usuarios
                                    join empleado in bd.Empleados
                                    on usuario.IdAnon equals empleado.IdEmpleado
                                    join rol in bd.Roles
                                    on usuario.IdRol equals rol.IdRol
                                    join tipoUsuario in bd.TiposUsuarios
                                    on usuario.IdTipoUsuario equals tipoUsuario.IdTipoUsuario
                                    where usuario.Habilitado == 1
                                    select new UsuarioCLS
                                    {
                                        idUsuario = usuario.IdUsuario,
                                        nombreEmpleado = empleado.Nombre + " " + empleado.Apellido,
                                        nombreU = usuario.NombreU,
                                        nombreRol = rol.NombreR,
                                        nombreTipoUsuario = tipoUsuario.Nombre
                                    }).ToList();

                    listaUsuario = listaUsuario.OrderBy(p => p.idUsuario).ToList();
                }
                else
                {
                    listaUsuario = (from usuario in bd.Usuarios
                                    join empleado in bd.Empleados
                                    on usuario.IdAnon equals empleado.IdEmpleado
                                    join rol in bd.Roles
                                    on usuario.IdRol equals rol.IdRol
                                    join tipoUsuario in bd.TiposUsuarios
                                    on usuario.IdTipoUsuario equals tipoUsuario.IdTipoUsuario
                                    where usuario.Habilitado == 1
                                    && (
                                    empleado.Nombre.Contains(nombreEmpleado)
                                    || empleado.Apellido.Contains(nombreEmpleado)
                                    )
                                    select new UsuarioCLS
                                    {
                                        idUsuario = usuario.IdUsuario,
                                        nombreEmpleado = empleado.Nombre + " " + empleado.Apellido,
                                        nombreU = usuario.NombreU,
                                        nombreRol = rol.NombreR,
                                        nombreTipoUsuario = tipoUsuario.Nombre
                                    }).ToList();

                    listaUsuario = listaUsuario.OrderBy(p => p.idUsuario).ToList();
                }
                
            }


            return PartialView("_TablaUsuario",listaUsuario);
        }

        public string Guardar(UsuarioCLS oUsuarioCls, int titulo)
        {
            //rpta es numero de registros afectados - vacio es error
            string rpta = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class='list-group'>";

                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item list-group-item-action list-group-item-danger'>" + item + "</li>";
                    }

                    rpta += "</ul>";
                }
                else
                {
                    using (var bd = new ConsultorioOdonBDEntities1())
                    {

                        using (var transaccion = new TransactionScope())
                        {
                            if (titulo.Equals(-1))
                            {
                                Usuarios oUsuario = new Usuarios();
                                oUsuario.NombreU = oUsuarioCls.nombreU;
                                // 1. Cifrar la contraseña
                                SHA256Managed sha = new SHA256Managed();
                                byte[] byteContra = Encoding.Default.GetBytes(oUsuarioCls.contrasena);
                                byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                                string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                                // 1.
                                oUsuario.Contrasena = cadenaContraCifrada;
                                oUsuario.IdTipoUsuario = oUsuarioCls.idTipoUsuario;
                                oUsuario.IdRol = oUsuarioCls.idRol;
                                oUsuario.IdAnon = oUsuarioCls.idAnon;
                                oUsuario.Habilitado = 1;
                                bd.Usuarios.Add(oUsuario);

                                Empleados oEmpleado = bd.Empleados.Where(p => p.IdEmpleado.Equals(oUsuarioCls.idAnon)).First();
                                oEmpleado.TieneUsuario = 1;

                                rpta = bd.SaveChanges().ToString();

                                transaccion.Complete();

                                if (rpta == "0") rpta = "";
                            }
                            else
                            {
                                //Edito
                                Usuarios oUsuario = bd.Usuarios.Where(p => p.IdUsuario == titulo).First();
                                oUsuario.NombreU = oUsuarioCls.nombreU;
                                oUsuario.IdTipoUsuario = oUsuarioCls.idTipoUsuario;
                                oUsuario.IdRol = oUsuarioCls.idRol;

                                rpta = bd.SaveChanges().ToString();
                                transaccion.Complete();


                            }
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

        //Recuperar informacion para la edicion a traves de un objeto
        public JsonResult recuperarDatos(int idUsuario)
        {
            //listarCombos();
            UsuarioCLS oUsuarioCls = new UsuarioCLS();
            using(var bd = new ConsultorioOdonBDEntities1())
            {
                Usuarios oUsuario = bd.Usuarios.Where(p => p.IdUsuario == idUsuario).First();
                oUsuarioCls.nombreU = oUsuario.NombreU;
                oUsuarioCls.idTipoUsuario = (int)oUsuario.IdTipoUsuario;
                oUsuarioCls.idRol = (int)oUsuario.IdRol;

            }

            return Json(oUsuarioCls, JsonRequestBehavior.AllowGet);
        }

        public string eliminar(UsuarioCLS oUsuarioCls)
        {
            string rpta = "";
            try
            {
                int idusuario = oUsuarioCls.idUsuario;
                using (var bd = new ConsultorioOdonBDEntities1())
                {
                    Usuarios oUsuario = bd.Usuarios.Where(p => p.IdUsuario == idusuario).First();
                    oUsuario.Habilitado = 0;
                    rpta = bd.SaveChanges().ToString();
                }
            }
            catch (Exception ex)
            {
                rpta = "";
            }
            return rpta;
        }
    }
}