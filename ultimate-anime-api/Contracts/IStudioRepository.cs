using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IStudioRepository
    {
        Task<IEnumerable<Studio>> GetAllStudios(bool trackChanges);
        Task<Studio> GetStudio(Guid studioId, bool trackChanges);
        void CreateStudio(Studio studio);
        Task<IEnumerable<Studio>> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteStudio(Studio studio);
    }
}
