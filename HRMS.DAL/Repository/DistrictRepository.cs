﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;
using HRMS.DAL.UnitOfWork;

namespace HRMS.DAL.TypeRepository
{
    public class DistrictRepository : GenericRepository<District>
    {
        public DistrictRepository(IUnitOfWork unitOfWork, string ProcedureName, string TableID, string Prefix) : base(unitOfWork, ProcedureName, TableID, Prefix)
        {

        }


    }
}