using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RefriSilva.Models;

namespace RefriSilva.Data
{
    public class RefriSilvaContext : DbContext
    {
        public RefriSilvaContext (DbContextOptions<RefriSilvaContext> options)
            : base(options)
        {
        }

        public DbSet<RefriSilva.Models.Servico> Servico { get; set; }
    }
}
