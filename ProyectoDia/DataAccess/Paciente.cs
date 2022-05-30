using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDia.DataAccess
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El numero de tarjeta sanitaria es obligatorio")]
        public String TarjetaSanitaria { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public String Apellido { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El telefono es obligatorio")]
        public String Telefono { get; set; }

        [Required(ErrorMessage = "La direccion es obligatoria")]
        public String Direccion { get; set; }
        private IEnumerable<VisitaMedica> ListaVisitasMedicas { get; set; }
        private Medico MedicoCabecera { get; set; }
    }
}
