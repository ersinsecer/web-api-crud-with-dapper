using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithDapper.Models;
using WebApiWithDapper.ViewModels;

namespace WebApiWithDapper.Business
{
    public interface IProductBusiness
    {
        Task<ProductViewModel> GetAsync(long id);
        Task<ProductViewModel> GetAllAsync();
        Task AddAsync(Product product);
        bool DeleteAsync(long id);
        Task UpdateAsync(Product product);
    }
}
