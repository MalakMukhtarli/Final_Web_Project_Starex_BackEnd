using Entity.Entities.Titles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface ITitleService
    {
        Task<Title> GetWithId(int id);
        Task<List<Title>> GetAll();
        Task Update(Title title);
    }
}
