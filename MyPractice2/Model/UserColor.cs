namespace MyPractice2.Model
{
    public class UserColor
    {
        public int UserId { get; set; }
        public User User { get; set; }


        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
