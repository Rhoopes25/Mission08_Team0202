using System.ComponentModel.DataAnnotations;

namespace Mission8IS413.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = "";
    }
}
