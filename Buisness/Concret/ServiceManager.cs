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

        public void Add(Service service)
        {
            _serviceDal.Add(service);
        }

        public void Delete(int id)
        {
            _serviceDal.Delete(new Service { Id = id });
        }

        public List<Service> GetAllService()
        {
            return _serviceDal.GetAll(s => !s.IsDeleted);
        }

        Service IServiceService.GetServiceWithId(int id)
        {
            return _serviceDal.Get(s => s.Id == id && !s.IsDeleted);
        }

        public void Update(Service service)
        {
            _serviceDal.Update(service);
        }
    }
}
