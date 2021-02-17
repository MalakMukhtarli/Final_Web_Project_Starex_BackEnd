using Entity.Entities.HomePages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IIntroService
    {
        Task<Intro> GetWithId(int id);
        Task<List<Intro>> GetAll();
        Task Add(Intro data);
        Task Update(Intro data);
        Task Delete(int id);
    }
}
