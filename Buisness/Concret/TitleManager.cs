using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Titles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class TitleManager : ITitleService
    {
        private readonly ITitleDal _titleDal;
        public TitleManager(ITitleDal titleDal)
        {
            _titleDal = titleDal;
        }
        public async Task<List<Title>> GetAll()
        {
            return await _titleDal.GetAll();
        }

        public async Task<Title> GetWithId(int id)
        {
            return await _titleDal.Get(t => t.Id == id);
        }

        async Task ITitleService.Update(Title title)
        {
            await _titleDal.Update(title);
        }
    }
}
