using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppConsultorio.Models
{
    public class PacienteCLS
    {
        // 1. Tabla paciente - datos principales
        public int idPaciente { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Longitud maxima de 50 caracteres")]
        public string nombre { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Longitud maxima de 50 caracteres")]
        public string apellido { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Longitud maxima de 50 caracteres")]
        public string numeroIdentificacion { get; set; }

        [Required]
        public long celular { get; set; }

        public int habilitado { get; set; }

        // 1.1 Tabla paciente - demas datos
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime fechaNacimiento { get; set; }

        public int edad {get; set;}

        [StringLength(100, ErrorMessage = "Longitud maxima de 100 caracteres")]
        [EmailAddress(ErrorMessage ="Ingrese un email valido")]
        public string email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Longitud maxima de 100 caracteres")]
        public string direccion { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Longitud maxima de 100 caracteres")]
        public string barrio { get; set; }

        public long telefono { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "Longitud maxima de 50 caracteres")]
        public string ocupacion { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Longitud maxima de 50 caracteres")]
        public string aseguradora { get; set; }

        [StringLength(50, ErrorMessage = "Longitud maxima de 50 caracteres")]
        public string vinculacion { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Longitud maxima de 150 caracteres")]
        public string motivoConsulta { get; set; }

        // 2. Id's de otras tablas
        public int idTipoIdentificacion { get; set; }

        public int idDepartamento { get; set; }

        public int idSexo { get; set; }
        // 3. Propiedades adicionales
        public string identificacionTipo { get; set; }

        public string departamentoLugar { get; set; }

        public string sexoFM { get; set; }
 
    }
}