using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AnimeRepository : RepositoryBase<Anime>, IAnimeRepository
    {
        public AnimeRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {

        }
    }
}
