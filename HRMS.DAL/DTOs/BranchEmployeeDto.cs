namespace HRMS.DAL.DTOs
{
    public class BranchEmployeeDto
    {
        public int BranchId { get; set; }

        public int EmployeeId { get; set; }

        public string ReasonOfTransfer { get; set; }

        public string Refrence1 { get; set; }

        public string Refrence2 { get; set; }

        public string Refrence3 { get; set; }

        public string Refrence4 { get; set; }

        public string Refrence5 { get; set; }

        public bool Active { get; set; }
    }
}
