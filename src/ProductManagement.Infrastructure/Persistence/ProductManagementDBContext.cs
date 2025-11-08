using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Infrastructure.Persistence
{
    public class ProductManagementDBContext : DbContext
    {
        public ProductManagementDBContext(DbContextOptions<ProductManagementDBContext> options)
            : base(options)
        {
        }

        // DbSets → tabelas
        public DbSet<Product> Products => Set<Product>();

        public DbSet<ProductEvent> ProductEvents => Set<ProductEvent>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Product
            modelBuilder.Entity<Product>(builder =>
            {
                builder.ToTable("products");
                builder.HasKey(p => p.Id);

                builder.Property(p => p.Id)
                    .ValueGeneratedNever(); // UUID controlado pela app

                builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(p => p.Category)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(p => p.UnitCost)
                    .HasPrecision(18, 2)
                    .IsRequired();

                builder.Property(p => p.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .IsRequired();
            });


            modelBuilder.Entity<ProductEvent>(builder =>
            {
                builder.ToTable("product_events");
                builder.HasKey(e => e.Id);

                builder.Property(e => e.ProductId).IsRequired();

                builder.Property(e => e.EventType)
                    .HasConversion<int>()
                    .IsRequired();

                builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(e => e.UnitCost)
                    .HasPrecision(18, 2)
                    .IsRequired();

                builder.Property(e => e.OccurredAt)
                    .IsRequired();
            });
        }
    }
}
