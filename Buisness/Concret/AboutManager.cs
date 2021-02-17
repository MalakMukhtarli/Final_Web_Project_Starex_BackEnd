using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class AboutManager :IAboutService
    {
        private readonly IAboutDAL _aboutDAL;

        public AboutManager(IAboutDAL aboutDAL)
        {
            _aboutDAL = aboutDAL;
        }

        public async Task<About> GetWithId(int id)
        {
            return await _aboutDAL.Get(c => c.Id == id);
        }

        public async Task<List<About>> GetAll()
        {
            return await _aboutDAL.GetAll();
        }

        async Task IAboutService.Add(About data)
        {
            await _aboutDAL.Add(data);
        }

        async Task IAboutService.Update(About data)
        {
            await _aboutDAL.Update(data);
        }

        async Task IAboutService.Delete(int id)
        {
            await _aboutDAL.Delete(new About { Id = id });
        }
    }
}
