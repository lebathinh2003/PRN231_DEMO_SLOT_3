using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductRazorView.Models;

namespace ProductRazorView.Pages
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = _httpClientFactory.CreateClient("Products");
            var response = await httpClient.GetAsync($"api/Products/{id}");
            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Product = JsonSerializer.Deserialize<Product>(json);
                return Page();
            }

            return NotFound();

        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var data = new StringContent(JsonSerializer.Serialize(Product), System.Text.Encoding.UTF8, "application/json");


            var httpClient = _httpClientFactory.CreateClient("Products");
            var response = await httpClient.PutAsync($"api/Products/{Product.Id}", data);

            return RedirectToPage("./Index");
        }

     
    }
}
