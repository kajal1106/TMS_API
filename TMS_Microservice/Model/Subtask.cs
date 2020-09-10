using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace TMS_Microservice.Model
{
    public class Subtask
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        [IgnoreDataMember]
        public Task Task { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string State { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime Finish_date { get; set; }

    }
}
