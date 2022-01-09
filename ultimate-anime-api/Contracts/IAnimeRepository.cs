using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAnimesAsync(Guid studioId, AnimeParameters animeParameters, bool trackChanges);
        Task<Anime> GetAnimeAsync(Guid studioId, Guid id, bool trackChanges);
        void CreateAnimeForStudio(Guid studioId, Anime anime);
        void DeleteAnime(Anime anime);
    }
}
