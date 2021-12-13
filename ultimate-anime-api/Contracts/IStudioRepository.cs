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
        IEnumerable<Studio> GetAllStudios(bool trackChanges);
        Studio GetStudio(Guid studioId, bool trackChanges);
        void CreateStudio(Studio studio);
        IEnumerable<Studio> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteStudio(Studio studio);
    }
}
