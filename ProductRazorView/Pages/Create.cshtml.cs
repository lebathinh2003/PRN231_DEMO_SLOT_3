﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductRazorView.Models;

namespace ProductRazorView.Pages
{
    public class CreateModel : PageModel
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var data = new StringContent(JsonSerializer.Serialize(Product), System.Text.Encoding.UTF8, "application/json");

            await Console.Out.WriteLineAsync("product:"+ data.ToString());

            var httpClient = _httpClientFactory.CreateClient("Products");
            var response = await httpClient.PostAsync($"api/Products", data);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
