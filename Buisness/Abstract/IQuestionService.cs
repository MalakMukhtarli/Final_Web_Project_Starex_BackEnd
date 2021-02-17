using Entity.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IQuestionService
    {
        Task<Question> GetWithId(int id);
        Task<List<Question>> GetAll();
        Task Add(Question data);
        Task Update(Question data);
        Task Delete(int id);
    }
}
