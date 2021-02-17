using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class QuestionNavbarManager : IQuestionNavbarService
    {
        private readonly IQuestionNavbarDal _context;

        public QuestionNavbarManager(IQuestionNavbarDal context)
        {
            _context = context;
        }

        public async Task<List<QuestionNavbar>> GetAll()
        {
            return await _context.GetAll(q => !q.IsDeleted);
        }

        public async Task<QuestionNavbar> GetWithId(int id)
        {
            return await _context.Get(q => q.Id == id && !q.IsDeleted);
        }

        async Task IQuestionNavbarService.Add(QuestionNavbar data)
        {
            await _context.Add(data);
        }

        async Task IQuestionNavbarService.Update(QuestionNavbar data)
        {
            await _context.Update(data);
        }

        async Task IQuestionNavbarService.Delete(int id)
        {
            await _context.Delete(new QuestionNavbar { Id = id });
        }
    }
}
