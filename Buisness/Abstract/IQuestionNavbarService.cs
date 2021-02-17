using Entity.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IQuestionNavbarService
    {
        Task<QuestionNavbar> GetWithId(int id);
        Task<List<QuestionNavbar>> GetAll();
        Task Add(QuestionNavbar data);
        Task Update(QuestionNavbar data);
        Task Delete(int id);
    }
}
