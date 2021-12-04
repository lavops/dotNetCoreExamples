using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IStudioRepository _studioRepository;
        private IAnimeRepository _animeRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IStudioRepository Studio
        {
            get
            {
                if (_studioRepository == null)
                    _studioRepository = new StudioRepository(_repositoryContext);

                return _studioRepository;
            }
        }

        public IAnimeRepository Anime
        {
            get
            {
                if (_animeRepository == null)
                    _animeRepository = new AnimeRepository(_repositoryContext);

                return _animeRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}
