using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.webapi.ProductController
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new ()
    {
        new Product { Id = 1, Name = "Savaş Sanatı", Price = 10.99m, Category = "Kitap" },
        new Product { Id = 2, Name = "İnsan Ne İle Yaşar", Price = 10.99m, Category = "Kitap" },
        new Product { Id = 3, Name = "Beyaz Zambaklar", Price = 30.99m, Category = "Kitap" },
        new Product { Id = 4, Name = "Bilgisayar", Price = 20.99m, Category = "Elektronik" },
        new Product { Id = 5, Name = "Telefon", Price = 20.99m, Category = "Elektronik" },
        new Product { Id = 6, Name = "Kalem", Price = 10.99m, Category = "Kırtasiye" }


    };

        [HttpGet]
        public IEnumerable<Product> GetList()
        {
            return products;
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product newProduct)
        {
            newProduct.Id = products.Count + 1;
            products.Add(newProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public IActionResult PutProductById(int id, [FromBody] Product updatedProduct)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Category = updatedProduct.Category;

            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            products.Remove(product);
            return NoContent();
        }
    }   
}
