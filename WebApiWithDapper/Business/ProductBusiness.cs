using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithDapper.Data;
using WebApiWithDapper.Models;
using WebApiWithDapper.ViewModels;

namespace WebApiWithDapper.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _productRepository;
        public ProductBusiness(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductViewModel> GetAsync(long id)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            var product = await _productRepository.GetAsync(id);
            if (product == null)
            {
                productViewModel.Message = "İlgili ürün bulunamadı.";
            }
            else
            {
                productViewModel.Products.Add(product);
            }
            return productViewModel;

        }

        public async Task<ProductViewModel> GetAllAsync()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            IEnumerable<Product> products = await _productRepository.GetAllAsync();

            if (products.ToList().Count == 0)
            {
                productViewModel.Message = "Ürün bulunamdı...";
            }
            else
            {
                productViewModel.Products.AddRange(products);
            }

            return productViewModel;
        }
        public async Task AddAsync(Product product)
        {
            Product _product = new Product()
            {
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };

            await _productRepository.AddAsync(_product);
        }

        public bool DeleteAsync(long id)
        {
            return _productRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }
    }
}
