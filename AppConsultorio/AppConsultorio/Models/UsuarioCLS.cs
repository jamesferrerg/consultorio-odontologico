using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppConsultorio.Models
{
    public class UsuarioCLS
    {
        public int idUsuario { get; set; }

        [Required]
        public string nombreU { get; set; }

        [Required]
        public string contrasena { get; set; }

        public int habilitado { get; set; }

        //1. Id's de otras tablas
        [Required]
        public int idRol { get; set; }

        [Required]
        public int idAnon { get; set; }

        [Required]
        public int idTipoUsuario { get; set; }

        //2. Propiedades adicionales

        public string nombreEmpleado { get; set; }

        public string nombreRol { get; set; }

        public string nombreTipoUsuario { get; set; }

        public string nombreEmpleadoEnviar { get; set; }
    }
}