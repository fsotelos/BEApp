using Healthy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Healthy.Infraestructure
{
    public class HealthyDbContext : DbContext
    {
        
        public virtual DbSet<User> Users { get; set; }
        public HealthyDbContext(DbContextOptions<HealthyDbContext> options) : base(options) { 
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        // Configurar la propiedad para convertir automáticamente a UTC
                        property.SetValueConverter(new UtcDateTimeConverter());
                    }
                }
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=dpg-cm7j1aun7f5s73ebd1jg-a.oregon-postgres.render.com;Port=5432;Database=dbusers_wwpu;Username=dbusers_wwpu_user;Password=FJRGfGrZsfCffQ2rdE2iox3vP8nJ4xgL;SSL Mode=Require;Trust Server Certificate=true;");
            }                //optionsBuilder.UseSqlServer("Server=localhost;Database=PruebaHealthyDB;TrustServerCertificate=True;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
        {
            public UtcDateTimeConverter(ConverterMappingHints mappingHints = null)
                : base(
                      v => v.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v, DateTimeKind.Utc) : v,
                      v => v,
                      mappingHints
                )
            {
            }
        }

    }
}
