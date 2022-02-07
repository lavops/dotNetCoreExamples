using Contracts;
using Entities.DTOs;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ultimate_anime_api.Utility
{
    public class AnimeLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<AnimeDto> _dataShaper;

        public AnimeLinks(LinkGenerator linkGenerator, IDataShaper<AnimeDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }      

        public LinkResponse TryGenerateLinks(IEnumerable<AnimeDto> animeDto, string fields, Guid studioId, HttpContext httpContext)
        {
            var shapedAnime = ShapeData(animeDto, fields);

            if (ShouldGnereateLinks(httpContext))
                return ReturnLinkedAnime(animeDto, fields, studioId, httpContext, shapedAnime);

            return ReturnShapedAnime(shapedAnime);
        }

        private List<Entity> ShapeData(IEnumerable<AnimeDto> animeDto, string fields) =>
            _dataShaper.ShapeData(animeDto, fields).Select(x => x.Entity).ToList();

        private bool ShouldGnereateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private LinkResponse ReturnShapedAnime(List<Entity> shapedAnime) =>
            new LinkResponse { ShapedEntities = shapedAnime };

        private LinkResponse ReturnLinkedAnime(IEnumerable<AnimeDto> animeDto, string fields, Guid studioId, HttpContext httpContext, List<Entity> shapedAnime)
        {
            var animeDtoList = animeDto.ToList();
            for(var index = 0; index < animeDtoList.Count(); index++)
            {
                var animeLinks = CreateLinksForAnime(httpContext, studioId, animeDtoList[index].Id, fields);
                shapedAnime[index].Add("Links", animeLinks);
            }

            var animeCollection = new LinkCollectionWrapper<Entity>(shapedAnime);
            var linkedAnime = CreateLinksForAnime(httpContext, animeCollection);

            return new LinkResponse { HasLinks = true, LinkedEntities = linkedAnime};
        }

        private List<Link> CreateLinksForAnime(HttpContext httpContext, Guid studioId, Guid id, string fields)
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(httpContext, "GetAnimeForStudio", values: new { studioId, id, fields }), 
                         "self",
                         "GET"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteAnimeForStudio", values: new { studioId, id }),
                         "delete_anime",
                         "DELETE"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateAnimeForStudio", values: new { studioId, id }),
                         "update_anime",
                         "PUT"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateAnimeForCompany", values: new { studioId, id }),
                         "partially_update_anime",
                         "PATCH"),
            };

            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForAnime(HttpContext httpContext, LinkCollectionWrapper<Entity> animeWrapper)
        {
            animeWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetAnimeForStudio", values: new { }), "self", "GET"));

            return animeWrapper;
        }
    }
}
