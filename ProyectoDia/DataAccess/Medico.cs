using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDia.DataAccess
{
    public class Medico
    {
        [Key]
        public int Id { get;  set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String matricula { get; set; }
        //relacion de muchos a muchos con visita medica
        //un medico puede tener muchas visitas medicas
        public bool Activo { get; set; }
        public ICollection<VisitaMedica> ListaVisitasMedicas { get; set; }

        //relacion de uno a muchos con paciente
        //un medico puede tener muchos pacientes
        public ICollection<Paciente> ListaPacientes { get; set; }


    }
}
