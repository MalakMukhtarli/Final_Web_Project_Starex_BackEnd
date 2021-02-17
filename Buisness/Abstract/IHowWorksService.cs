using Entity.Entities.HomePages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IHowWorksService
    {
        Task<HowWorks> GetWithId(int id);
        Task<List<HowWorks>> GetAll();
        Task Add(HowWorks data);
        Task Update(HowWorks data);
        Task Delete(int id);
    }
}
