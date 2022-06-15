using Microsoft.EntityFrameworkCore;

namespace ProyectoDia.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        //constructor vacio. llama al constructor de la clase que hereda
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        //DbSet representa una coleccion de todas las entidades en mi contexto.
        //agrego aqui las clases que quiero que se mapeen a la base de datos para crear tablas.
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Medico> Medico { get; set; }

        public DbSet<VisitaMedica> VisitaMedica { get; set; }

        //Models Builder es la clase responsable de construir el modelo para un contexto 
        //lo hace sobreescribiendo OnModelCreating
        //por default, el comportamiento es en cascada
        //cambio el comportamiento al borrar un registro para que no me permita  
        //borrar registros que tienen relaciones y asi no dejar huerfanos
        //no action: lanza una exception al intertar borrar un registro que tiene una relacion. 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //establezco la relacion de Medico con Paciente y VisitaMedica
            //si intento borrar un medico que tiene campos relacionados, me lanzara un error
            modelBuilder.Entity<Medico>(medico =>
            {
                medico.HasMany(c => c.ListaPacientes).WithOne(m => m.MedicoCabecera).OnDelete(DeleteBehavior.NoAction);
                medico.HasMany(c => c.ListaVisitasMedicas).WithOne(e => e.Medico).OnDelete(DeleteBehavior.NoAction);
            });
            //establezco la relacion entre Paciente con MedicoCabecera y VisitaMedica
            //si intento borrar un paciente que tiene campos relacionados, me lanzara un error
            modelBuilder.Entity<Paciente>(paciente =>
            {
                paciente.HasOne(m => m.MedicoCabecera).WithMany(c => c.ListaPacientes).OnDelete(DeleteBehavior.NoAction);
                paciente.HasMany(c => c.ListaVisitasMedicas).WithOne(v => v.Paciente).OnDelete(DeleteBehavior.NoAction);
            });

            //la visita medica tiene un paciente y se relaciona con la tabla paciente mediante el campo listavisitasmedicas
            //si intento borrar una VisitaMedica que tiene campos relacionados, lanzara un error
            modelBuilder.Entity<VisitaMedica>(visitaMedica =>
            {
                visitaMedica.HasOne(v =>v.Paciente).WithMany(p => p.ListaVisitasMedicas).OnDelete(DeleteBehavior.NoAction);
                visitaMedica.HasOne(v => v.Medico).WithMany(m => m.ListaVisitasMedicas).OnDelete(DeleteBehavior.NoAction);

            });

        }
    }
}
