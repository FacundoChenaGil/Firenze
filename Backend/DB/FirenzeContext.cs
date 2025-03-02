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

            // Crear clave unica en la tabla usuarios en Nombre_Usuario_Us
            modelBuilder.Entity<Usuario>()
                .HasAlternateKey(us => us.Nombre_Usuario_Us)
                .HasName("UK_Nombre_Usuario_Us");

            // Precargar Tipos de usuario en BD
            modelBuilder.Entity<Tipo_Usuario>().HasData(
                new Tipo_Usuario { Id_Tipo_Usuario_Tus = 1, Descripcion_Tus = "Admin"},
                new Tipo_Usuario { Id_Tipo_Usuario_Tus = 2, Descripcion_Tus = "Cliente"}
             );

            // Precargar Tipos de trabajo en BD
            modelBuilder.Entity<Tipo_Trabajo>().HasData(
                new Tipo_Trabajo { Id_Tipo_Trabajo_Ttr = 1, Descripcion_Ttr = "Pedicuría"},
                new Tipo_Trabajo { Id_Tipo_Trabajo_Ttr = 2, Descripcion_Ttr = "Manicuría"}
                );

            // Precargar Estados Turno en BD
            modelBuilder.Entity<Estado_Turno>().HasData(
                new Estado_Turno { Id_Estado_Turno_Et = 1, Descripcion_Et = "Ausente" },
                new Estado_Turno { Id_Estado_Turno_Et = 2, Descripcion_Et = "Cancelado"},
                new Estado_Turno { Id_Estado_Turno_Et = 3, Descripcion_Et = "Pendiente de Pago"},
                new Estado_Turno { Id_Estado_Turno_Et = 4, Descripcion_Et = "Solicitado"},
                new Estado_Turno { Id_Estado_Turno_Et = 5, Descripcion_Et = "Finalizado"}
                );
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
