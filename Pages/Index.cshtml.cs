using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Activity8gian.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // A list of products to display
        public List<Product> Products { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; } = "Name";

        [BindProperty(SupportsGet = true)]
        public bool SortAsc { get; set; } = true;

        [BindProperty(SupportsGet = true)]
        public string SearchBy { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string Keyword { get; set; } = string.Empty;

        public void OnGet()
        {
            // Static list of products
            Products = new List<Product>
            {
                new Product { Id = 1, Name = "Product A", Price = 10.99m, Category = "Electronics" },
                new Product { Id = 2, Name = "Product B", Price = 20.99m, Category = "Clothing" },
                new Product { Id = 3, Name = "Product C", Price = 30.99m, Category = "Groceries" },
                new Product { Id = 4, Name = "Product D", Price = 40.99m, Category = "Electronics" },
                new Product { Id = 5, Name = "Product E", Price = 50.99m, Category = "Clothing" },
                new Product { Id = 6, Name = "Product F", Price = 60.99m, Category = "Electronics" },
                new Product { Id = 7, Name = "Product G", Price = 70.99m, Category = "Furniture" },
                new Product { Id = 8, Name = "Product H", Price = 80.99m, Category = "Groceries" },
                new Product { Id = 9, Name = "Product I", Price = 90.99m, Category = "Clothing" },
                new Product { Id = 10, Name = "Product J", Price = 100.99m, Category = "Furniture" }
            };

            // Filtering logic
            if (!string.IsNullOrEmpty(SearchBy) && !string.IsNullOrEmpty(Keyword))
            {
                Products = SearchBy.ToLower() switch
                {
                    "name" => Products.Where(p => p.Name.Contains(Keyword, StringComparison.OrdinalIgnoreCase)).ToList(),
                    "price" => Products.Where(p => p.Price.ToString().Contains(Keyword)).ToList(),
                    "category" => Products.Where(p => p.Category.Contains(Keyword, StringComparison.OrdinalIgnoreCase)).ToList(),
                    _ => Products
                };
            }

            // Sorting logic
            Products = SortBy.ToLower() switch
            {
                "name" => SortAsc ? Products.OrderBy(p => p.Name).ToList() : Products.OrderByDescending(p => p.Name).ToList(),
                "price" => SortAsc ? Products.OrderBy(p => p.Price).ToList() : Products.OrderByDescending(p => p.Price).ToList(),
                "category" => SortAsc ? Products.OrderBy(p => p.Category).ToList() : Products.OrderByDescending(p => p.Category).ToList(),
                _ => SortAsc ? Products.OrderBy(p => p.Id).ToList() : Products.OrderByDescending(p => p.Id).ToList()
            };
        }

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public string Category { get; set; } = string.Empty;
        }
    }
}
