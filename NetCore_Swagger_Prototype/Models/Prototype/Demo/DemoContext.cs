using Microsoft.EntityFrameworkCore;

namespace NetCore_Swagger_Prototype.Models.Prototype.Demo
{
    public class DemoContext : DbContext
    {

        public DemoContext(DbContextOptions<DemoContext> options)
            : base(options)
        {
        }

        public DbSet<DemoCrud> DemoCrud { get; set; }
    }
}