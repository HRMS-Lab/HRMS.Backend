using System;
using System.Collections.Generic;
using HRMS.DAL.Data;

namespace HRMS.DAL
{
    public class Titles
    {

        [StoredProcedureParameter]
        public int TitleId { get; set; }
        [StoredProcedureParameter]
        public string TitleName { get; set; }
        [StoredProcedureParameter]
        public string TitleDescription { get; set; }

    }
}
