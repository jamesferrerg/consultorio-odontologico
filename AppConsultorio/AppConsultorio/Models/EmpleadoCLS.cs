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

        [Required]
        [Display(Name = "Nombres")]
        [StringLength(50,ErrorMessage ="Longitud maxima es 50 caracteres")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        [StringLength(50, ErrorMessage = "Longitud maxima es 50 caracteres")]
        public string apellido { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Fecha de contrato")]
        public DateTime fechacontrato { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        [StringLength(100, ErrorMessage = "Longitud maxima es 50 caracteres")]
        public string direccion { get; set; }

        [Required]
        [Display(Name = "Barrio")]
        [StringLength(50, ErrorMessage = "Longitud maxima es 50 caracteres")]
        public string barrio { get; set; }

        [Display(Name = "Teléfono fijo")]
        public long telefono { get; set; }


        [Display(Name = "Celular")]
        public long celular { get; set; }

        public int tieneusuario { get; set; }

        [Required]
        [Display(Name = "Número de identificación")]
        public string numeroIdentificacion { get; set; }

        [StringLength(100, ErrorMessage = "Longitud maxima de 100 caracteres")]
        [EmailAddress(ErrorMessage = "Ingrese un email valido")]
        public string email { get; set; }

        public int habilitado { get; set; }

        // 1. Id's de otras tablas
        [Display(Name = "Tipo Contrato")]
        public int idtipoContrato { get; set; }

        [Display(Name = "Sexo")]
        public int idSexo { get; set; }

        [Display(Name = "Tipo de identificación")]
        public int idtipoIdentificacion { get; set; }

        [Display(Name = "Cargo")]
        public int idCargo { get; set; }

        // 2. Propiedades adicionales
        // 2.1 Tabla tipo contrato

        [Display(Name = "Contrato")]
        public string nombreContrato { get; set; }

        // 2.2 Tabla sexo

        [Display(Name = "Sexo")]
        public string sexoFM { get; set; }

        // 2.3 Tabla tipo identificacion

        [Display(Name = "Identificación")]
        public string tipoIdentificacion { get; set; }

        //2.4 tabla cargo
        [Display(Name = "Cargo a desempeñar")]
        public string nombreCargo { get; set; }

        [Display(Name = "Salario")]
        public long salario { get; set; }
    }
}