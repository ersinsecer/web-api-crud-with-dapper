using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithDapper.Business;
using WebApiWithDapper.Models;
using WebApiWithDapper.ViewModels;

namespace WebApiWithDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductBusiness _productBusiness;

        public ProductsController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [HttpGet]
        public async Task<ProductViewModel> Get()
        {
            return await _productBusiness.GetAllAsync();
        }

        // GET api/products/{id}
        [HttpGet("{id}")]
        public async Task<ProductViewModel> Get(long id)
        {
            return await _productBusiness.GetAsync(id);
        }        

        [ProducesResponseType(201)]
        [HttpPost]
        public async Task Post([FromBody]Product product)
        {
            await _productBusiness.AddAsync(product);
        }
        [HttpDelete("{id}")]
        public bool Delete(long id)
        {
            return _productBusiness.DeleteAsync(id);
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] Product product)
        {
            await _productBusiness.UpdateAsync(product);
        }

    }
}