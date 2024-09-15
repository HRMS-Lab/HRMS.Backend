using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.DAL.ModelsDto
{
    public class TitleDto
    {
        public string TitleName { get; set; }
        public string TitleDescription { get; set; }
        public int OrgId { get; set; }
        public string? Refrence1 { get; set; }
        public string? Refrence2 { get; set; }
        public string? Refrence3 { get; set; }
        public string? Refrence4 { get; set; }
        public string? Refrence5 { get; set; }
        public int? OrgChartId { get; set; }
        public bool? Active { get; set; }
    }
}
