using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Notfications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class NotficationManager : INotficationService
    {
        private readonly INotficationDal _notficationDal;

        public NotficationManager(INotficationDal notficationDal)
        {
            _notficationDal = notficationDal;
        }

        async Task INotficationService.Add(Notfication data)
        {
            await _notficationDal.Add(data);
        }

        async Task INotficationService.Delete(int id)
        {
            await _notficationDal.Delete(new Notfication { Id = id });
        }

        public async Task<List<Notfication>> GetAll()
        {
            return await _notficationDal.GetAll(n=>!n.IsDeleted);
        }

        public async Task<Notfication> GetWithId(int id)
        {
            return await _notficationDal.Get(n=>n.Id == id && !n.IsDeleted);
        }

        async Task INotficationService.Update(Notfication data)
        {
            await _notficationDal.Update(data);
        }
    }
}
