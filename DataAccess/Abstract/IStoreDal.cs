﻿using Core.Repository;
using Entity.Entities.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IStoreDal: IEntityRepository<Store>
    {
    }
}
