using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Studio>> GetAllStudios(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(s => s.Name).ToListAsync();

        public async Task<Studio> GetStudio(Guid studioId, bool trackChanges) =>
            await FindByCondition(s => s.Id.Equals(studioId), trackChanges).SingleOrDefaultAsync();

        public void CreateStudio(Studio studio) => Create(studio);

        public async Task<IEnumerable<Studio>> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeleteStudio(Studio studio)
        {
            Delete(studio);
        }
    }
}
