using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductRazorView.Models;

namespace ProductRazorView.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }



        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("Products");
            var response = await httpClient.GetAsync($"api/Products");
            var json = await response.Content.ReadAsStringAsync();
            await Console.Out.WriteLineAsync("razor page**************************************************************");
            await Console.Out.WriteLineAsync(json);

            if (response.IsSuccessStatusCode)
            {
                Product = JsonSerializer.Deserialize<IList<Product>>(json);
            }
        }
    }
}
