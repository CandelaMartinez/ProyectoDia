using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //relacion de muchos a muchos con visita medica
        //un paciente puede tener muchas visitas medicas
        public ICollection<VisitaMedica> ListaVisitasMedicas { get; set; }

        //relacion de muchos a uno con medico. muchos pacientes un medico.
        //foreign key con la table medico
        [Required]
        public int MedicoCabeceraId { get; set; }
        
        [ForeignKey("MedicoCabeceraId")]
        public Medico MedicoCabecera { get; set; }
    }
}
