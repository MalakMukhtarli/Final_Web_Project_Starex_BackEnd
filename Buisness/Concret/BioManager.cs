using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Bios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class BioManager : IBioService
    {

        private readonly IBioDal _bioDal;

        public BioManager(IBioDal bioDal)
        {
            _bioDal = bioDal;
        }
        async Task IBioService.Add(Bio data)
        {
            await _bioDal.Add(data);
        }

        async Task IBioService.Delete(int id)
        {
            await _bioDal.Delete(new Bio { Id = id });
        }

        public async Task<List<Bio>> GetAll()
        {
            return await _bioDal.GetAll();
        }

        public async Task<Bio> GetWithId(int id)
        {
            return await _bioDal.Get(b => b.Id == id);
        }

        async Task IBioService.Update(Bio data)
        {
            await _bioDal.Update(data);
        }
    }
}
