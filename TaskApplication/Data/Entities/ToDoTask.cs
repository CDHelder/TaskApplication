using System;
using System.ComponentModel.DataAnnotations;

namespace TaskApplication.Data.Entities
{
    public class ToDoTask
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TypeStatus Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BeginDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
    }

    public enum TypeStatus
    {
        Planned,
        Doing,
        Done
    }
}
