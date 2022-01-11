using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryAnimeExtensions
    {
        public static IQueryable<Anime> FilterAnime(this IQueryable<Anime> anime, DateTime minDate, DateTime maxDate) =>
            anime.Where(a => a.ReleaseDate >= minDate && a.ReleaseDate <= maxDate);

        public static IQueryable<Anime> Search(this IQueryable<Anime> anime, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return anime;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return anime.Where(a => a.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}
