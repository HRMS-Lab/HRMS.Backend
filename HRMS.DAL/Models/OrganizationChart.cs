using System;
using System.Collections.Generic;
using HRMS.DAL.Data;

namespace HRMS.DAL;

public partial class OrganizationChart
{
    public int OrgChartId { get; set; }

    public int? OrgId { get; set; }

    public string? OrgChartName { get; set; }

    public string? OrgChartDescription { get; set; }

    public int? OrgChartLevel { get; set; }

    public string? Refrence1 { get; set; }

    public string? Refrence2 { get; set; }

    public string? Refrence3 { get; set; }

    public string? Refrence4 { get; set; }

    public string? Refrence5 { get; set; }

    public bool?  Active { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual Organization Org { get; set; }

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
}
