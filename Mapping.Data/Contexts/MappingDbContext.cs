using Mapping.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mapping.Data.Contexts
{
    public class MappingDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public MappingDbContext(DbContextOptions<MappingDbContext> options) : base(options)
        {
        }
    }
}
