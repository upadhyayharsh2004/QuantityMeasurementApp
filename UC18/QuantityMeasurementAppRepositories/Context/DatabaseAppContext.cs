using Microsoft.EntityFrameworkCore;
using QuantityMeasurementAppModels.Entities;

namespace QuantityMeasurementAppRepositories.Context
{
    public class DatabaseAppContext : DbContext
    {
        public DatabaseAppContext(DbContextOptions<DatabaseAppContext> options) : base(options)
        {
        }

        public DbSet<QuantityEntity> MeasurementRecordsEntity { get; set; }
        public DbSet<PersonEntity> UsersEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuantityEntity>(entity =>
            {
                entity.ToTable("quantity_measurements_tables_conversion");
                entity.HasKey(e => e.EntityId);
                entity.HasIndex(e => e.EntityOperation).HasDatabaseName("IX_quantity_measurements_operation");
                entity.HasIndex(e => e.EntityMeasurementType).HasDatabaseName("IX_quantity_measurements_measurement_type");
                entity.Property(e => e.EntityId).HasColumnName("entity_id").ValueGeneratedOnAdd();

                entity.Property(e => e.EntitySecondValue).HasColumnName("entity_second_unit").HasMaxLength(50);
                entity.Property(e => e.EntityResultValue).HasColumnName("entity_result_value");
                entity.Property(e => e.EntityMeasurementType).HasColumnName("entity_measurement_type").HasMaxLength(50);
                entity.Property(e => e.IsEntityError).HasColumnName("entity_is_error");
                entity.Property(e => e.EntityErrorMessage).HasColumnName("entity_error_message").HasMaxLength(500);

                entity.Property(e => e.EntityUserId).HasColumnName("entity_user_id").IsRequired();
                entity.Property(e => e.EntityUpdatedAt).HasColumnName("entity_updated_at");

                entity.HasIndex(e => e.EntityUserId).HasDatabaseName("IX_quantity_measurements_user_id");
                entity.Property(e => e.EntityOperation).HasColumnName("entity_operation").IsRequired().HasMaxLength(50);
                entity.Property(e => e.EntityFirstValue).HasColumnName("entity_first_value");
                entity.Property(e => e.EntityFirstUnit).HasColumnName("entity_first_unit").HasMaxLength(50);
                entity.Property(e => e.EntitySecondValue).HasColumnName("entity_second_value");
                entity.Property(e => e.EntityCreatedAt).HasColumnName("entity_created_at");
                entity.HasIndex(e => e.IsEntityError).HasDatabaseName("IX_quantity_measurements_is_error");
            });
            modelBuilder.Entity<PersonEntity>(entity =>
            {
                entity.ToTable("users_authenication_and_authorization");

                entity.HasKey(e => e.EntityId);

                entity.HasIndex(e => e.EntityEmail).IsUnique().HasDatabaseName("IX_users_email");

                entity.Property(e => e.EntityName).HasColumnName("entity_name").IsRequired().HasMaxLength(255);


                entity.Property(e => e.EntityHashPassword).HasColumnName("entity_password_hash").IsRequired().HasMaxLength(100);

                entity.Property(e => e.EntityCreatedAt).HasColumnName("created_entity_at");

                entity.Property(e => e.EntityLastActiveAt).HasColumnName("last_active_at");

                entity.Property(e => e.EntityId).HasColumnName("entity_id").ValueGeneratedOnAdd();

                entity.Property(e => e.EntityEmail).HasColumnName("entity_email").IsRequired().HasMaxLength(255);
            });
        }
    }
}