using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class ServiceManager : IServiceService
    {
        private readonly IServiceDal _serviceDal;
        public ServiceManager(IServiceDal serviceDal)
        {
            _serviceDal = serviceDal;
        }

        public async Task<Service> GetWithId(int id)
        {
            return await _serviceDal.Get(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<List<Service>> GetAll()
        {
            return await _serviceDal.GetAll(s => !s.IsDeleted);
        }

        async Task IServiceService.Add(Service service)
        {
            await _serviceDal.Add(service);
        }

        async Task IServiceService.Update(Service service)
        {
            await _serviceDal.Update(service);
        }

        async Task IServiceService.Delete(int id)
        {
            await _serviceDal.Delete(new Service { Id = id });
        }
    }
}
