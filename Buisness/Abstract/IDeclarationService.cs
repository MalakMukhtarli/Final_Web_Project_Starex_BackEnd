using Entity.Entities.Declarations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IDeclarationService
    {
        Task<Declaration> GetWithId(int id);
        Task<List<Declaration>> GetAll();
        Task Add(Declaration declaration);
        Task Update(Declaration declaration);
        Task Delete(int id);
    }
}
