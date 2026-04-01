using Microsoft.EntityFrameworkCore;
using QuantityMeasurementAppModels.Entities;

namespace QuantityMeasurementAppRepositories.Context
{
    public class DatabaseAppContext : DbContext
    {
        public DbSet<QuantityEntity> MeasurementRecordsEntity { get; set; }

        public DatabaseAppContext(DbContextOptions<DatabaseAppContext> options) : base(options)
        {
        }
    }
}
