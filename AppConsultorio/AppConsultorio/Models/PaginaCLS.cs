using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppConsultorio.Models
{
    public class PaginaCLS
    {
        public int idPagina { get; set; }

        public string mensaje { get; set; }

        public string accion { get; set; }

        public string controlador { get; set; }
    }
}