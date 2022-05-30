using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDia.DataAccess
{
    public class Paciente
    {
        [Key]
        public int TarjetaSanitaria { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public String Apellido { get; set; }
        private DateTime FechaNacimiento { get; set; }
        public String Telefono { get; set; }
        public String Direccion { get; set; }
        private IEnumerable<VisitaMedica> ListaVisitasMedicas { get; set; }
        private Medico MedicoCabecera { get; set; }
    }
}
