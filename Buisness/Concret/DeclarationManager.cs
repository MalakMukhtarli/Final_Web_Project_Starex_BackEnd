using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Declarations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class DeclarationManager : IDeclarationService
    {
        private readonly IDeclarationDal _declarationDal;
        public DeclarationManager(IDeclarationDal declarationDal)
        {
            _declarationDal = declarationDal;
        }
        public async Task<List<Declaration>> GetAll()
        {
            return await _declarationDal.GetAll(d => !d.IsDeleted);
        }

        public async Task<Declaration> GetWithId(int id)
        {
            return await _declarationDal.Get(d => d.Id == id && !d.IsDeleted);
        }

        async Task IDeclarationService.Add(Declaration declaration)
        {
            await _declarationDal.Add(declaration);
        }

        async Task IDeclarationService.Delete(int id)
        {
            await _declarationDal.Delete(new Declaration { Id = id });
        }

        async Task IDeclarationService.Update(Declaration declaration)
        {
            await _declarationDal.Update(declaration);
        }
    }
}
