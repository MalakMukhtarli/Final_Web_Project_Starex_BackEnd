using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Newss;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class NewsManager : INewsService
    {
        private readonly INewsDal _context;

        public NewsManager(INewsDal context)
        {
            _context = context;
        }

        async Task INewsService.Add(News data)
        {
            await _context.Add(data);
        }

        async Task INewsService.Delete(int id)
        {
            await _context.Delete(new News { Id = id });
        }

        public async Task<List<News>> GetAll()
        {
            return await _context.GetAll(n => !n.IsDeleted);
        }

        public async Task<News> GetWithId(int id)
        {
            return await _context.Get(n => n.Id == id && !n.IsDeleted);
        }

        async Task INewsService.Update(News data)
        {
            await _context.Update(data);
        }
    }
}
