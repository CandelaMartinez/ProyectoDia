using ProyectoDia.Business.Models;
using System;
using System.Collections.Generic;

namespace ProyectoDia.ViewModels
{
    public class PacienteViewModel
    {
        public int TarjetaSanitaria { get; set; }
        private String Nombre { get; set; }
        private String Apellido { get; set; }
        private DateTime FechaNacimiento { get; set; }
        private String Telefono { get; set; }
        private String Direccion { get; set; }
        private IEnumerable<VisitaMedica> ListaVisitasMedicas { get; set; }
        private Medico MedicoCabecera { get; set; }
    }
}
