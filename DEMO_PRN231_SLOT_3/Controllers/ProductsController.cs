using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DEMO_PRN231_SLOT_3.DataAccess;
using DEMO_PRN231_SLOT_3.Models;
using DEMO_PRN231_SLOT_3.Lib;

namespace DEMO_PRN231_SLOT_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
            
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Product))]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(id == 777)
            {
                throw new Exception("day la middleware");
            }

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        //khong hoat dong khi khong co FromForm, bi ignor khi co frombody
        [HttpPost("bind")]
        //[Consumes("application/xml", "application/json")]
        [Produces("application/xml")]
        public async Task<IActionResult> BindTest([Bind("Name, Id")][FromForm] Product product) {
            return Ok($"Product Created: {product.Name} - {product.Price} no {product.Id}");
            
        }
        [HttpGet("header")]
        public async Task<IActionResult> Header([FromHeader(Name = "User-Agent")] string s)
        {
            return Ok(s);
        }

        //https://localhost:7253/api/Products/query?id=1&name=name
        [HttpGet("query")]
        public async Task<IActionResult> Query([FromQuery] int id, string name)
        {
            return Ok(id+name);
        }
        //https://localhost:7253/api/Products/route/10/name
        [HttpGet("route/{id}/{name}")]
        public async Task<IActionResult> Route([FromRoute] int id, string name)
        {
            return Ok(id+name);
        }

        [HttpPost("service")]
        public async Task<IActionResult> Service([FromBody] string str, [FromServices] Utils utils)
        {
            return Ok(utils.ToUpperCase(str));
        }


    }


}


