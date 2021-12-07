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
    public class StudioRepository : RepositoryBase<Studio>, IStudioRepository
    {
        public StudioRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {

        }

        public IEnumerable<Studio> GetAllStudios(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(s => s.Name).ToList();

        public Studio GetStudio(Guid studioId, bool trackChanges) =>
            FindByCondition(s => s.Id.Equals(studioId), trackChanges).SingleOrDefault();

        public void CreateStudio(Studio studio) => Create(studio);

        public IEnumerable<Studio> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
    }
}
