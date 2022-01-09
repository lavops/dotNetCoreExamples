using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
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
        
        public async Task<IEnumerable<Anime>> GetAnimesAsync(Guid studioId, AnimeParameters animeParameters, bool trackChanges) =>
            await FindByCondition(a => a.StudioId.Equals(studioId), trackChanges)
            .OrderBy(a => a.Name)
            .Skip((animeParameters.PageNumber - 1) * animeParameters.PageSize)
            .Take(animeParameters.PageSize)
            .ToListAsync();

        public async Task<Anime> GetAnimeAsync(Guid studioId, Guid id, bool trackChanges) =>
            await FindByCondition(a => a.StudioId.Equals(studioId) && a.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void CreateAnimeForStudio(Guid studioId, Anime anime)
        {
            anime.StudioId = studioId;
            Create(anime);
        }

        public void DeleteAnime(Anime anime)
        {
            Delete(anime);
        }
    }
}
