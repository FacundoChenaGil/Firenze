using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class FirenzeContext : DbContext
    {
        public FirenzeContext(DbContextOptions<FirenzeContext> options)
            : base(options)
        {
            
        }

        public DbSet<Trabajo> Trabajos { get; set; }
        public DbSet<Tipo_Trabajo> TipoTrabajos { get; set;}
    }
}
