using System;
using System.Collections.Generic;

namespace HRMS.DAL;

public partial class ProjectRegion
{
    public int RegProjId { get; set; }

    public int RegionId { get; set; }

    public int ProjectId { get; set; }

    public string Description { get; set; }

    public string Refrence1 { get; set; }

    public string Refrence2 { get; set; }

    public string Refrence3 { get; set; }

    public string Refrence4 { get; set; }

    public bool Active { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual Project Project { get; set; }

    public virtual Region Region { get; set; }
}
