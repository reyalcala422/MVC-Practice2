namespace MyPractice2.Model.Brand
{
    public class UserBrand
    {
        public int UserBrandId { get; set; }
        public User UserIdBrand { get; set; }


        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
