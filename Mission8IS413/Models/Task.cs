using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission8IS413.Models
{
    public class TaskModel
    {
        [Key]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Please enter a task name.")]
        public string TaskName { get; set; }
        public DateOnly TaskDue { get; set; }

        [Required(ErrorMessage = "Please enter a task Quadrant.")]
        public int TaskQuadrant { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? TaskCategory { get; set; }
        public bool TaskCompleted { get; set; }
    }
}
