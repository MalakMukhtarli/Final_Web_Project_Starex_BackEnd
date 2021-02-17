using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.HomePages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class HowWorksManager : IHowWorksService
    {

        private readonly IHowWorksDal _howWorksDal;

        public HowWorksManager(IHowWorksDal howWorksDal)
        {
            _howWorksDal = howWorksDal;
        }

        async Task IHowWorksService.Add(HowWorks data)
        {
            await _howWorksDal.Add(data);
        }

        async Task IHowWorksService.Delete(int id)
        {
            await _howWorksDal.Delete(new HowWorks { Id = id });
        }

        public async Task<List<HowWorks>> GetAll()
        {
            return await _howWorksDal.GetAll();
        }

        public async Task<HowWorks> GetWithId(int id)
        {
            return await _howWorksDal.Get(a => a.Id == id);
        }

        async Task IHowWorksService.Update(HowWorks data)
        {
            await _howWorksDal.Update(data);
        }
    }
}
