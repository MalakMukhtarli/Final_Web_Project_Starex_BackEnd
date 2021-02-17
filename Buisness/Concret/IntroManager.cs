using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.HomePages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class IntroManager : IIntroService
    {
        private readonly IIntroDal _introDal;

        public IntroManager(IIntroDal introDal)
        {
            _introDal = introDal;
        }
        public async Task<Intro> GetWithId(int id)
        {
            return await _introDal.Get(i => i.Id == id);
        }

        async Task<List<Intro>> IIntroService.GetAll()
        {
            return await _introDal.GetAll();
        }

        async Task IIntroService.Add(Intro data)
        {
            await _introDal.Add(data);
        }

        async Task IIntroService.Update(Intro data)
        {
            await _introDal.Update(data);
        }

        async Task IIntroService.Delete(int id)
        {
            await _introDal.Delete(new Intro { Id = id });
        }
    }
}
