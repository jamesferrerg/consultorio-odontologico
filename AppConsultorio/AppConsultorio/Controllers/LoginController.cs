using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AppConsultorio.ClasesAuxiliares;
using AppConsultorio.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppConsultorio.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public string Login(UsuarioCLS oUsuario)
        {
            string mensaje = "";

            try
            {
                //Validate Google recaptcha here
                var response = Request["g-recaptcha-response"];
                string secretKey = "6LdH9MUUAAAAALrVwcBywXKUPJZ_VC12t-x4XX1a";
                var client = new WebClient();
                var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
                var obj = JObject.Parse(result);
                var status = (bool)obj.SelectToken("success");

                if (!ModelState.IsValid)
                {
                    
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    mensaje += "<ul class='list-group'>";

                    foreach (var item in query)
                    {
                        mensaje += "<li class='list-group-item list-group-item-action list-group-item-danger'>" + item + "</li>";
                    }

                    mensaje += "</ul>";
                }
                else
                {

                    string nombreUsuario = oUsuario.nombreU;
                    string password = oUsuario.contrasena;
                    // 1. Cifrar la contraseña
                    SHA256Managed sha = new SHA256Managed();
                    byte[] byteContra = Encoding.Default.GetBytes(password);
                    byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                    string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                    // 1.
                    using (var bd = new ConsultorioOdonBDEntities1())
                    {
                        int numeroVeces = bd.Usuarios.Where(p => p.NombreU == nombreUsuario && p.Contrasena == cadenaContraCifrada).Count();
                        mensaje = numeroVeces.ToString();
                        ViewBag.Message = status ? "Google reCaptcha validation success" : "Google reCaptcha validation failed";

                        if (mensaje == "0" || !status)
                        {
                            if (mensaje == "0" && !status)
                            {
                                mensaje = "Usuario y/o contraseña incorrecto <br> Google reCaptcha error de validación";
                            }else if (mensaje == "0")
                            {
                                mensaje = "Usuario y/o contraseña incorrecto";
                            }
                            else
                            {
                                mensaje = "Google reCaptcha error de validación";
                            }
                            
                        }
                            
                        else
                        {

                            Usuarios ousuario = bd.Usuarios.Where(p => p.NombreU == nombreUsuario && p.Contrasena == cadenaContraCifrada).First();
                            //Todo el objeto usuario
                            Session["Usuario"] = ousuario;

                            Empleados oEmpleado = bd.Empleados.Where(p => p.IdEmpleado == ousuario.IdAnon).First();
                            Session["nombreCompleto"] = oEmpleado.Nombre + " " + oEmpleado.Apellido;

                            List<MenuCLS> listaMenu = (from usuario in bd.Usuarios
                                                       join rol in bd.Roles
                                                       on usuario.IdRol equals rol.IdRol
                                                       join rolPagina in bd.RolesPaginas
                                                       on rol.IdRol equals rolPagina.IdRol
                                                       join pagina in bd.Paginas
                                                       on rolPagina.IdPagina equals pagina.IdPagina
                                                       where rol.IdRol == ousuario.IdRol && rolPagina.IdRol == usuario.IdRol && usuario.IdUsuario == ousuario.IdUsuario
                                                       select new MenuCLS
                                                       {
                                                           nombreAccion = pagina.Accion,
                                                           nombreControlador = pagina.Controlador,
                                                           mensajePag = pagina.Mensaje
                                                       }).ToList();

                                                      Session["Rol"] = listaMenu;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                mensaje = "";
            }



            return mensaje;
        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            Session["Rol"] = null;
            return RedirectToAction("Index");
        }

        public string RecuperarContra(string correo, string noIdentificacion)
        {
            string mensaje = "";
            try
            {
                using(var bd =new ConsultorioOdonBDEntities1())
                {
                    int cantidad = 0;
                    int idempleado;
                    cantidad = bd.Empleados.Where(p => p.Email == correo && p.NumeroIdentificacion == noIdentificacion).Count();
                    if (cantidad == 0) mensaje = "No existe una persona registrada con esa información";
                    else
                    {
                        idempleado = bd.Empleados.Where(p => p.Email == correo && p.NumeroIdentificacion == noIdentificacion).First().IdEmpleado;
                        //Verificar si existe usuario
                        int nveces = bd.Usuarios.Where(p => p.IdAnon == idempleado).Count();
                        if (nveces == 0)
                        {
                            mensaje = "No tiene usuario";
                        }
                        else
                        {
                            //Obtener su id
                            Usuarios ousuario = bd.Usuarios.Where(p => p.IdAnon == idempleado).First();
                            //Modificar su clave
                            Random ra = new Random();
                            int n1 = ra.Next(0, 9);
                            int n2 = ra.Next(0, 9);
                            int n3 = ra.Next(0, 9);
                            int n4 = ra.Next(0, 9);
                            string nuevaContra = n1.ToString() + n2 + n3 + n4;
                            // 1. Cifrar la contraseña
                            SHA256Managed sha = new SHA256Managed();
                            byte[] byteContra = Encoding.Default.GetBytes(nuevaContra);
                            byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                            string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                            // 1.

                            ousuario.Contrasena = cadenaContraCifrada;
                            mensaje = bd.SaveChanges().ToString();

                            Correo.enviarCorreo(correo, "Recuperar clave", "Se restablecio su contraseña, ahora su contraseña es: "+nuevaContra, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                mensaje = "";
            }
            return mensaje;
        }
    }
}