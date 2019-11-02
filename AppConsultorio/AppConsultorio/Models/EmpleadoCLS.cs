using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppConsultorio.Models
{
    public class EmpleadoCLS
    {
        [Display(Name ="Id Nombre")]
        public int idempleado {get; set;}
        [Display(Name = "Nombres")]
        public string nombre { get; set; }
        [Display(Name = "Apellidos")]
        public string apellido { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de contrato")]
        public DateTime fechacontrato { get; set; }
        [Display(Name = "Dirección")]
        public string direccion { get; set; }
        [Display(Name = "Barrio")]
        public string barrio { get; set; }
        [Display(Name = "Teléfono fijo")]
        public long telefono { get; set; }
        [Display(Name = "Celular")]
        public long celular { get; set; }
        public int tieneusuario { get; set; }
        public int habilitado { get; set; }
    }
}