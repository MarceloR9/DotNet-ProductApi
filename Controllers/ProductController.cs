using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Controllers;
using ProductApi.Context;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new {id = product.Id}, product);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return _context.Products.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id) 
        {
            var products = _context.Products.Find(id);

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("tipoproduto/{tipoproduto}")]
        public IActionResult ObterPorTipoProduto(string tipoproduto)
        {
            var products = _context.Products.Where(x => x.TipoProduto.Contains(tipoproduto));
            return Ok(products);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarProduto(int id, Product product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarProduto(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
