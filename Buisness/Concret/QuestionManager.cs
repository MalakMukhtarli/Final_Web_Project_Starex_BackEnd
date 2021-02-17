using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class QuestionManager : IQuestionService
    {
        private readonly IQuestionDal _context;
        public QuestionManager(IQuestionDal context)
        {
            _context = context;
        }
        async Task IQuestionService.Add(Question data)
        {
            await _context.Add(data);
        }

        async Task IQuestionService.Delete(int id)
        {
            await _context.Delete(new Question { Id = id });
        }

        public async Task<List<Question>> GetAll()
        {
            return await _context.GetAll(q => !q.IsDelete);
        }

        public async Task<Question> GetWithId(int id)
        {
            return await _context.Get(q => q.Id == id && !q.IsDelete);
        }

        async Task IQuestionService.Update(Question data)
        {
            await _context.Update(data);
        }
    }
}
