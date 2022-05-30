using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDia.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base (options)
        {

        }
        //add here all the models that I need to be migrated to de DB.
        public DbSet<Paciente> Paciente { get; set; }
    }
}
