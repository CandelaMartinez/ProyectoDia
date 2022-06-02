using ProyectoDia.Business.Models;
using System;
using System.Collections.Generic;

namespace ProyectoDia.ViewModels
{
    public class MedicoViewModel
    {
        public long id { get; private set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String matricula { get; set; }

        public IEnumerable<VisitaMedica> ListaVisitasMedicas { get; set; }
        public IEnumerable<Paciente> ListaPacientes { get; set; }
    }
}
