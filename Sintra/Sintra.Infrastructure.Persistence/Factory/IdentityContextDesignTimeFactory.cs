using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sintra.Infrastructure.Persistence.Contexts;

namespace Sintra.Infrastructure.Persistence.Factory
{
    public class IdentityContextDesignTimeFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            optionsBuilder.UseSqlServer("Server=217.163.23.113;Database=SintraDb;User ID=shopuser;Password=Inloya098#;MultipleActiveResultSets=True");
            return new IdentityContext(optionsBuilder.Options);
        }
    }
}