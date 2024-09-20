using HRMS.DAL.Handler;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HRMS.DAL.ModelsDto
{
    public class AttachmentDto
    {
        public int? AttachTypeID { get; set; }
        public int? EmployeeId { get; set; }

        //[JsonConverter(typeof(ByteArrayConverter))]
        public byte[]? Attachments { get; set; }

        public bool? Active { get; set; }
        public string? Refrence1 { get; set; }
        public string? Refrence2 { get; set; }
        public string? Refrence3 { get; set; }
        public string? Refrence4 { get; set; }
        public string? Refrence5 { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
