using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Activity8gian.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> PagedProducts { get; set; }

        [BindProperty]
        public SearchParameters SearchParams { get; set; }

        public void OnGet(string? keyword = "", string? searchBy = "", string? sortBy = null, string? sortAsc = "true", int pageSize = 5, int pageIndex = 1)
        {
            // Default search parameters if none are provided
            if (SearchParams == null)
            {
                SearchParams = new SearchParameters
                {
                    SortBy = "Name",
                    SortAsc = sortAsc == "true",
                    PageSize = pageSize,
                    PageIndex = pageIndex
                };
            }

            // Fetch and filter products
            var products = GetProducts();

            // Apply sorting
            if (sortBy == "Name")
            {
                products = SearchParams.SortAsc ? products.OrderBy(p => p.Name).ToList() : products.OrderByDescending(p => p.Name).ToList();
            }
            else if (sortBy == "Price")
            {
                products = SearchParams.SortAsc ? products.OrderBy(p => p.Price).ToList() : products.OrderByDescending(p => p.Price).ToList();
            }
            else if (sortBy == "Category")
            {
                products = SearchParams.SortAsc ? products.OrderBy(p => p.Category).ToList() : products.OrderByDescending(p => p.Category).ToList();
            }

            // Apply search filter
            if (!string.IsNullOrEmpty(keyword))
            {
                products = products.Where(p =>
                    (searchBy == "Name" && p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                    (searchBy == "Price" && p.Price.ToString().Contains(keyword)) ||
                    (searchBy == "Category" && p.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            // Pagination
            var pagedProducts = products
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Set the view model
            PagedProducts = pagedProducts;
            SearchParams.PageIndex = pageIndex;
            SearchParams.PageCount = (int)Math.Ceiling((double)products.Count / pageSize);
        }

        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product()
                {
                    Id = 1,
                    Name = "Product A",
                    Price = 10.99m,
                    Category = "Electronics"
                },
                new Product()
                {
                    Id = 2,
                    Name = "Product B",
                    Price = 20.99m,
                    Category = "Clothing"
                },
                new Product()
                {
                    Id = 3,
                    Name = "Product C",
                    Price = 30.99m,
                    Category = "Groceries"
                },
                new Product()
                {
                    Id = 4,
                    Name = "Product D",
                    Price = 40.99m,
                    Category = "Electronics"
                },
                new Product()
                {
                    Id = 5,
                    Name = "Product E",
                    Price = 50.99m,
                    Category = "Clothing"
                },
                new Product()
                {
                    Id = 6,
                    Name = "Product F",
                    Price = 60.99m,
                    Category = "Electronics"
                },
                new Product()
                {
                    Id = 7,
                    Name = "Product G",
                    Price = 70.99m,
                    Category = "Furniture"
                },
                new Product()
                {
                    Id = 8,
                    Name = "Product H",
                    Price = 80.99m,
                    Category = "Groceries"
                },
                new Product()
                {
                    Id = 9,
                    Name = "Product I",
                    Price = 90.99m,
                    Category = "Clothing"
                },
                new Product()
                {
                    Id = 10,
                    Name = "Product J",
                    Price = 100.99m,
                    Category = "Furniture"
                }
            };
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

    public class SearchParameters
    {
        public string? SearchBy { get; set; }
        public string? Keyword { get; set; }
        public string? SortBy { get; set; }
        public bool SortAsc { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
