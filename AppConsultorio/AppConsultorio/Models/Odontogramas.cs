//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppConsultorio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Odontogramas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Odontogramas()
        {
            this.Condiciones = new HashSet<Condiciones>();
        }
    
        public int IdOdontograma { get; set; }
        public string Posicion { get; set; }
        public string Observacion { get; set; }
        public Nullable<int> Habilitado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Condiciones> Condiciones { get; set; }
    }
}