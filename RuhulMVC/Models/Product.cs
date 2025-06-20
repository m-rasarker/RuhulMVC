using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RuhulMVC.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }
        [Length(2, 50)]
        public required string Name { get; set; }


        public required Category category { get; set; }


        public required int Price { get; set; }

        public required int Quantity { get; set; }


        public enum Category
        {
            Electronics,
            Clothing,
            Food           
        }





    }
}
