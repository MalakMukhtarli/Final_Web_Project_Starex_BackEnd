using Core.Repository;
using Entity.Entities.Titles;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ITitleDal:IEntityRepository<Title>
    {
    }
}
