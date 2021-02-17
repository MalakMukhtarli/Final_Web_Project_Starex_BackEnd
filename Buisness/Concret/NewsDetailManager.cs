using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Newss;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class NewsDetailManager : INewsDetailService
    {
        private readonly INewsDetailDal _context;

        public NewsDetailManager(INewsDetailDal context)
        {
            _context = context;
        }
        public async Task<NewsDetail> GetWithId(int id)
        {
            return await _context.Get(nd => nd.Id == id && !nd.IsDeleted);
        }

        public async Task<List<NewsDetail>> GetAll()
        {
            return await _context.GetAll(n => !n.IsDeleted);
        }

        async Task INewsDetailService.Add(NewsDetail data)
        {
            await _context.Add(data);
        }

        async Task INewsDetailService.Update(NewsDetail data)
        {
            await _context.Update(data);
        }

        async Task INewsDetailService.Delete(int id)
        {
            await _context.Delete(new NewsDetail { Id = id });
        }
    }
}
