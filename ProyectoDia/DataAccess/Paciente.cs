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
        private String Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        private String Apellido { get; set; }
        private DateTime FechaNacimiento { get; set; }
        private String Telefono { get; set; }
        private String Direccion { get; set; }
        private IEnumerable<VisitaMedica> ListaVisitasMedicas { get; set; }
        private Medico MedicoCabecera { get; set; }
    }
}
