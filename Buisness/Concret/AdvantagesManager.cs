using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.HomePages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class AdvantagesManager : IAdvantagesService
    {
        private readonly IAdvantagesDal _advantagesDal;

        public AdvantagesManager(IAdvantagesDal advantagesDal)
        {
            _advantagesDal = advantagesDal;
        }

        public async Task<Advantages> GetWithId(int id)
        {
            return await _advantagesDal.Get(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<List<Advantages>> GetAll()
        {
            return await _advantagesDal.GetAll();
        }

        async Task IAdvantagesService.Add(Advantages data)
        {
            await _advantagesDal.Add(data);
        }

        async Task IAdvantagesService.Update(Advantages data)
        {
            await _advantagesDal.Update(data);
        }

        async Task IAdvantagesService.Delete(int id)
        {
            await _advantagesDal.Delete(new Advantages { Id = id });
        }
    }
}
