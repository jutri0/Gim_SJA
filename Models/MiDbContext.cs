using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Configuration;

namespace GymAJT.Models
{
    public class MiDbContext : DbContext
    {

        public MiDbContext()
        {
        }
        public MiDbContext(DbContextOptions<MiDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string _connectionString = configuration.GetValue<string>("ConnectionStrings:Database1Connection");
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>()
                .HasKey(u => u.IdUsuarioPk);
            modelBuilder.Entity<Equipos>()
                .HasKey(e => e.IdEquipoPk);
            modelBuilder.Entity<Devoluciones>()
                .HasKey(d => d.IdDevolucionPk);
            modelBuilder.Entity<Alumnos>()
                .HasKey(a => a.IdAlumnoPk);
            modelBuilder.Entity<Prestamos>()
                .HasKey(p => p.IdPrestamoPk);
            modelBuilder.Entity<ViewModelAlumno>()
                .HasNoKey();
            modelBuilder.Entity<ViewModelEquipo>()
                .HasNoKey();
            modelBuilder.Entity<ViewModelSql>()
                .HasNoKey();
        }

    }
}
