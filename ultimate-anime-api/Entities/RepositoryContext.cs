using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Studio> Studios { get; set; }
        public DbSet<Anime> Animes { get; set; }
    }
}
