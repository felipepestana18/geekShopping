using System.ComponentModel.DataAnnotations;

namespace GeekShooping.Web.Models
{
    public class ProductModel
    {

        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }


        [Range(1, 100)]
        public int Count { get; set; } = 1;

        // fazendo validação para para ver se o texto passa do 24 caracteres
        public string SubstringName()
        {
            if (Name.Length < 24) return Name;
            return $"{Name.Substring(0, 24)} ...";
        }
        public string SubstringDescription()
        {
            if (Description.Length < 355) return Description;
            return $"{Description.Substring(0, 352)} ...";
        }

    }
}
