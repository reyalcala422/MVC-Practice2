using System.ComponentModel.DataAnnotations;

namespace MyPractice2.Model
{
    public class Color
    {
        public int Id { get; set; }
        [Required]
        public string ColorName { get; set; }

        public ICollection<UserColor> UserColors { get; set; } = new List<UserColor>();
    }
}
