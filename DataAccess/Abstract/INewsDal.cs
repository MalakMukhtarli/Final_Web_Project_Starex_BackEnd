﻿using Core.Repository;
using Entity.Entities.Newss;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface INewsDal: IEntityRepository<News>
    {
    }
    
}
