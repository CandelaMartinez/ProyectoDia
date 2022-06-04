using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDia.DataAccess
{
    public class VisitaMedica
    {
        [Key]
        public int Id { get;  set; }

        //relacion de muchas visitas medicas a un paciente
        [Required]
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente Paciente { get; set; }

        //relacion de muchas visitas medicas a un medico
        [Required]
        public int MedicoId { get; set; }

        [ForeignKey("MedicoId")]
        public Medico Medico { get; set; }

        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }

    }
}
