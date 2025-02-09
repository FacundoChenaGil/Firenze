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

            // Crear clave concatenada TrabajosXTurnos
            modelBuilder.Entity<TrabajoXTurno>()
                .HasKey(tt => new { tt.Id_Turno_Tt, tt.Id_Trabajo_Tt });

            // Crear clave unica en la tabla usuarios en Correo_Electronico_Us
            modelBuilder.Entity<Usuario>()
                .HasAlternateKey(us => us.Correo_Electronico_Us)
                .HasName("UK_Correo_Electronico_Us");

            // Precargar Tipos de usuario en BD
            modelBuilder.Entity<Tipo_Usuario>().HasData(
                new Tipo_Usuario { Id_Tipo_Usuario_Tus = 1, Descripcion_Tus = "Admin"},
                new Tipo_Usuario { Id_Tipo_Usuario_Tus = 2, Descripcion_Tus = "Cliente"}
             );
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
