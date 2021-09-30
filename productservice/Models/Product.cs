using System.ComponentModel.DataAnnotations.Schema;

namespace productservice.Models
{
    [Table("Products")]
    public class Product
    {
        public int productId {get; set;}
        public string productName {get; set;}
        public int supplierId {get; set;}
        public int categoryId {get; set;}
        public string quantityPerUnit {get; set;}
        public decimal unitPrice {get; set;}
        public System.Int16 unitsInStock {get; set;}
        public System.Int16 unitsOnOrder {get; set;}
        public System.Int16 reorderLevel {get; set;}
        public bool discontinued {get; set;}      
    }
}