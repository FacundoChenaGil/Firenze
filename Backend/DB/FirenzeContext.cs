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
        public DbSet<Tarjeta_Trabajo> TarjetasTrabajo { get; set; }
        public DbSet<TrabajoXTurno> TrabajosXTurnos { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Estado_Turno> EstadosTurno { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tipo_Usuario> TiposUsuario { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrabajoXTurno>()
                .HasKey(tt => new { tt.Id_Turno_Tt, tt.Id_Trabajo_Tt });

            base.OnModelCreating(modelBuilder);
        }
    }
}
