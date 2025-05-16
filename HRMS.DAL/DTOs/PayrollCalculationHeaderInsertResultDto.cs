using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRMS.DAL.DTOs
{
    public class PayrollCalculationHeaderInsertResultDto
    {
        public int NewPayClacHeaderID { get; set; }

        [JsonPropertyName("Number of contracts affected")] // Optional: use this if you're using System.Text.Json and column name has spaces
        public int NumberOfContractsAffected { get; set; }
    }
}
