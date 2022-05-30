using System;


namespace ProyectoDia.DataAccess
{
    public class VisitaMedica
    {
        public long Id { get; private set; }
        private Paciente Paciente { get; set; }
        private Medico Medico { get; set; }
        private DateTime Fecha { get; set; }
        private string Observaciones { get; set; }

    }
}
