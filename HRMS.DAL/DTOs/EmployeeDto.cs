using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.DAL.ModelsDto
{
    public class EmployeeDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public long? Phone { get; set; }
        public long? NationalId { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public int DepartmentId { get; set; }
        public int OrgId { get; set; }
        public int TitleId { get; set; }
        public string? BankAccount { get; set; }
        public string? BankName { get; set; }
        public string? Refrence1 { get; set; }
        public string? Refrence2 { get; set; }
        public string? Refrence3 { get; set; }
        public string? Refrence4 { get; set; }
        public string? Refrence5 { get; set; }
        public string? Refrence6 { get; set; }
        public string? Refrence7 { get; set; }
        public string? Refrence8 { get; set; }
        public string? Refrence9 { get; set; }
        public string? Refrence10 { get; set; }
        public bool? Active { get; set; }
    }
}
