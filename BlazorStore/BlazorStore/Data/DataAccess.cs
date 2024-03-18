using BlazorStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorStore.Data
{
	public class DataAccess
	{
		private readonly ProductContext _cartContext;

		public DataAccess(ProductContext productContext)
		{
			_cartContext = productContext;
		}

		public async Task<List<Product>> GetAllProducts()
		{
			var productsTask = _cartContext.Products.ToListAsync();
			var products = await productsTask;
			return products;
		}

		public async Task AddProduct(Product product)
		{
			await _cartContext.Products.AddAsync(product);
			await _cartContext.SaveChangesAsync();
		}

		public async Task<Product?> GetProductById(int id)
		{
			var result = await _cartContext.Products.FindAsync(id);
			return result;
		}

		public async Task UpdateProduct(Product product)
		{
			_cartContext.Products.Update(product);
			await _cartContext.SaveChangesAsync();
		}
	}
}
