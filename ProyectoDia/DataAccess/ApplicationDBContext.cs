using Microsoft.EntityFrameworkCore;

namespace ProyectoDia.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        //agrego aqui las clases que quiero que se mapeen a la base de datos para crear tablas.
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Medico> Medico { get; set; }

        public DbSet<VisitaMedica> VisitaMedica { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //comportamiento on cascade para el borrado de medicos
            //si borro un medico se borran sus pacientes.
            modelBuilder.Entity<Medico>(medico =>
            {
                medico.HasMany(c => c.ListaPacientes).WithOne(m => m.MedicoCabecera).OnDelete(DeleteBehavior.NoAction);
                medico.HasMany(c => c.ListaVisitasMedicas).WithOne(e => e.Medico).OnDelete(DeleteBehavior.NoAction);
            });
            //muchas pacientes tienen un medico, por el campo de paciente, medicocabecera
            //se relaciona a muchos con el campo listapacientes de la clase medico
            modelBuilder.Entity<Paciente>(paciente =>
            {
                paciente.HasOne(m => m.MedicoCabecera).WithMany(c => c.ListaPacientes).OnDelete(DeleteBehavior.NoAction);
                paciente.HasMany(c => c.ListaVisitasMedicas).WithOne(v => v.Paciente).OnDelete(DeleteBehavior.NoAction);
            });

            //la visita medica tiene un paciente y se relaciona con la tabla paciente mediante el campo listavisitasmedicas
            modelBuilder.Entity<VisitaMedica>(visitaMedica =>
            {
                visitaMedica.HasOne(v =>v.Paciente).WithMany(p => p.ListaVisitasMedicas).OnDelete(DeleteBehavior.NoAction);
                visitaMedica.HasOne(v => v.Medico).WithMany(m => m.ListaVisitasMedicas).OnDelete(DeleteBehavior.NoAction);

            });




        }
    }
}
