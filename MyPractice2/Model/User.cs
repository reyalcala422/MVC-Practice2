namespace MyPractice2.Model
{
    public class User
    {
        public int? Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public ICollection<UserColor> UserColors { get; set; } = new List<UserColor>();
    }
}
