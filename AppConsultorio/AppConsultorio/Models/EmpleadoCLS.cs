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
        [Required]
        [StringLength(50,ErrorMessage ="Longitud maxima es 50 caracteres")]
        public string nombre { get; set; }
        [Display(Name = "Apellidos")]
        [Required]
        [StringLength(50, ErrorMessage = "Longitud maxima es 50 caracteres")]
        public string apellido { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [Display(Name = "Fecha de contrato")]
        [Required]
        public DateTime fechacontrato { get; set; }
        [Display(Name = "Dirección")]
        [Required]
        [StringLength(100, ErrorMessage = "Longitud maxima es 50 caracteres")]
        public string direccion { get; set; }
        [Display(Name = "Barrio")]
        [Required]
        [StringLength(50, ErrorMessage = "Longitud maxima es 50 caracteres")]
        public string barrio { get; set; }
        [Display(Name = "Teléfono fijo")]
        public long telefono { get; set; }
        [Display(Name = "Celular")]
        [Required]
        public long celular { get; set; }
        public int tieneusuario { get; set; }
        [Display(Name = "Número de identificación")]
        [Required]
        public string numeroIdentificacion { get; set; }
        public int habilitado { get; set; }
        // 1. Id's de otras tablas
        [Required]
        public int idtipoContrato { get; set; }
        [Required]
        public int idSexo { get; set; }
        [Required]
        public int idtipoIdentificacion { get; set; }
        [Required]
        public int idCargo { get; set; }
        // 2. Propiedades adicionales
        // 2.1 Tabla tipo contrato
        [Display(Name = "Contrato")]
        [Required]
        public string nombreContrato { get; set; }
        // 2.2 Tabla sexo
        [Display(Name = "Sexo")]
        [Required]
        public string sexoFM { get; set; }
        // 2.3 Tabla tipo identificacion
        [Display(Name = "Identificación")]
        [Required]
        public string tipoIdentificacion { get; set; }
        //2.4 tabla cargo
        [Display(Name = "Cargo a desempeñar")]
        [Required]
        public string nombreCargo { get; set; }
        [Display(Name = "Salario")]
        [Required]
        public long salario { get; set; }
    }
}