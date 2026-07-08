namespace MyPractice2.Model.Brand
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<UserBrand> UserBrands { get; set; } = new List<UserBrand>();
    }
}
