using Microsoft.EntityFrameworkCore;

namespace RefriSilva.Data
{
    public class RefriSilvaContext : DbContext
    {
        public RefriSilvaContext(DbContextOptions<RefriSilvaContext> options)
            : base(options)
        {
        }

        public DbSet<RefriSilva.Models.Servico> Servico { get; set; }
    }
}
