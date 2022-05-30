using ProyectoDia.Business.Models;
using System;
using System.Collections.Generic;

namespace ProyectoDia.ViewModels
{
    public class MedicoViewModel
    {
        public long id { get; private set; }
        private String Nombre { get; set; }
        private String Apellido { get; set; }
        private String matricula { get; set; }

        private IEnumerable<VisitaMedica> ListaVisitasMedicas { get; set; }
        private IEnumerable<Paciente> ListaPacientes { get; set; }
    }
}
