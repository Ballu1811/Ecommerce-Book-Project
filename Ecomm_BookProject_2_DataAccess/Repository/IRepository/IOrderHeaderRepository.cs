﻿using Ecomm_BookProject_2_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_BookProject_2_DataAccess.Repository.IRepository
{
   public interface IOrderHeaderRepository:IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
    }
}
