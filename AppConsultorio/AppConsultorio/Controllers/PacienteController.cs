using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppConsultorio.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PagedList;
using AppConsultorio.Filters;

namespace AppConsultorio.Controllers
{
    [Acceder]
    public class PacienteController : Controller
    {
        // GET: Paciente
        public ActionResult Index(int? page)
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
                                         nombreYapellido = paciente.Nombre+" "+paciente.Apellido,
                                         celular = (long)paciente.Celular
                                     }).ToList();

                    Session["lista"] = listaPaciente;
                }
    
            }
            catch (Exception ex)
            {
                listaPaciente = new List<PacienteCLS>();
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(listaPaciente.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Filtro (string nombrePaciente, int? page)
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
            //return PartialView("_TablaPaciente", listaPaciente);
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View("_TablaPaciente", listaPaciente.ToPagedList(pageNumber, pageSize));
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
                int cantidad = 0;
                cantidad = bd.Pacientes.Where(p => p.NumeroIdentificacion == oPacienteCLS.numeroIdentificacion).Count();
                if (cantidad >= 1)
                {
                    listarCombos();
                    TempData["msg"] = "<script>alert('El paciente ya se encuentra registrado');</script>";
                    return View(oPacienteCLS);
                }
                else
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
                    int cantidad = 0;
                    cantidad = bd.Pacientes.Where(p => p.NumeroIdentificacion == oPacienteCls.numeroIdentificacion && p.IdPaciente!=id).Count();
                    if (cantidad >= 1)
                    {
                        listarCombos();
                        TempData["msg"] = "<script>alert('Ya existe un paciente registrado');</script>";
                        return View(oPacienteCls);
                    }
                    else
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

        public FileResult generarPDF()
        {

            Document doc = new Document();
            byte[] buffer;

            using(MemoryStream ms = new MemoryStream())
            {
                //guardamos del documento en memoria
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                Paragraph title = new Paragraph("Listado de clientes");
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                Paragraph espacio = new Paragraph(" ");
                doc.Add(espacio);
                //Columnas (tabla)
                PdfPTable table = new PdfPTable(3);

                //Ancho de las columnas
                float[] value = new float[3] { 45, 25, 20 };
                table.SetWidths(value);
                //Creando celdas (poniendo contenido)
                PdfPCell celda1 = new PdfPCell(new Phrase("Nombre y apellido"));
                celda1.BackgroundColor = new BaseColor(195, 195, 195);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda1);

                PdfPCell celda2 = new PdfPCell(new Phrase("No. de identificación"));
                celda2.BackgroundColor = new BaseColor(195, 195, 195);
                celda2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda2);

                PdfPCell celda3 = new PdfPCell(new Phrase("Celular"));
                celda3.BackgroundColor = new BaseColor(195, 195, 195);
                celda3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda3);

                List<PacienteCLS> lista = (List<PacienteCLS>)Session["lista"];
                int noRegistros = lista.Count;
                for(int i=0; i<noRegistros; i++)
                {
                    table.AddCell(lista[i].nombreYapellido);
                    table.AddCell(lista[i].numeroIdentificacion);
                    table.AddCell(lista[i].celular.ToString());
                }

                doc.Add(table);
                doc.Close();

                buffer = ms.ToArray();
            }

            return File(buffer, "application/pdf");
        }

        public FileResult generadorExcel()
        {
            byte[] buffer;

            using(MemoryStream ms = new MemoryStream())
            {
                //Todo el documento excel
                ExcelPackage ep = new ExcelPackage();

                //Crear una hoja
                ep.Workbook.Worksheets.Add("Reporte");
                ExcelWorksheet ew = ep.Workbook.Worksheets[1];

                //Ponemos nombre de las columnas
                ew.Cells[1, 1].Value = "Nombre y apellido";
                ew.Cells[1, 2].Value = "No. de identificación";
                ew.Cells[1, 3].Value = "Celular";
                ew.Column(1).Width = 45;
                ew.Column(2).Width = 25;
                ew.Column(3).Width = 20;
                using(var range = ew.Cells[1, 1, 1, 3])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkRed);
                }

                List<PacienteCLS> lista = (List<PacienteCLS>)Session["lista"];
                int noRegistros = lista.Count;
                for (int i = 0; i < noRegistros; i++)
                {
                    ew.Cells[i + 2, 1].Value = lista[i].nombreYapellido;
                    ew.Cells[i + 2, 2].Value = lista[i].numeroIdentificacion;
                    ew.Cells[i + 2, 3].Value = lista[i].celular;
                }

                ep.SaveAs(ms);
                buffer = ms.ToArray();
            }

            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}