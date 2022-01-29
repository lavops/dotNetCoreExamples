using Contracts;
using Entities;
using Repository.Extensions;
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
        
        public async Task<PagedList<Anime>> GetAnimesAsync(Guid studioId, AnimeParameters animeParameters, bool trackChanges)
        {
            var anime = await FindByCondition(a => a.StudioId.Equals(studioId), trackChanges)
                .FilterAnime(animeParameters.MinDate, animeParameters.MaxDate)
                .Search(animeParameters.SearchTerm)
                .Sort(animeParameters.OrderBy)
                .ToListAsync();

            return PagedList<Anime>.ToPagedList(anime, animeParameters.PageNumber, animeParameters.PageSize);
        }
            

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
