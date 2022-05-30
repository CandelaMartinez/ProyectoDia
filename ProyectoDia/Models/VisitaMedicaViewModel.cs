using ProyectoDia.Business.Models;
using System;

namespace ProyectoDia.ViewModels
{
    public class VisitaMedicaViewModel
    {
        public long Id { get; private set; }
        private Paciente Paciente { get; set; }
        private Medico Medico { get; set; }
        private DateTime Fecha { get; set; }
        private string Observaciones { get; set; }
    }
}
