using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyTodoApp.Models
{
    public class Todo
    {
        public int Id {get; set;}

        [Required(ErrorMessage = "This field is required!")]
        public string Title { get; set; }

        [DisplayName("Concluded")]
        public bool Done { get; set; }

        [DisplayName("Created at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DisplayName("Last update")]
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        public string User { get; set; }
    }
}