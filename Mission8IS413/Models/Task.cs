using System.ComponentModel.DataAnnotations;

namespace Mission8IS413.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Please enter a task name.")]
        public string TaskName { get; set; }
        public DateOnly TaskDue { get; set; }

        [Required(ErrorMessage = "Please enter a task category.")]
        public int TaskQuadrant { get; set; }
        public string TaskCategory { get; set; }
        public bool TaskCompleted { get; set; }
    }
}
