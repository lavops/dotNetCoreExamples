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
        
        public IEnumerable<Anime> GetAnimes(Guid studioId, bool trackChanges) =>
            FindByCondition(a => a.StudioId.Equals(studioId), trackChanges).OrderBy(a => a.Name);

        public Anime GetAnime(Guid studioId, Guid id, bool trackChanges) =>
            FindByCondition(a => a.StudioId.Equals(studioId) && a.Id.Equals(id), trackChanges).SingleOrDefault();

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
