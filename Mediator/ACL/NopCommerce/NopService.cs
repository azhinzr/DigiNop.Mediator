using ApiClient;
using ApiClient.NopCommerce;
using Contracts;
using Contracts.NopCommerce;
using Contracts.NopCommerce.updateProduct;
using Mediator.ACL.NopCommerce.services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator.ACL.NopCommerce
{
    public class NopService : INopService
    {
        private readonly IConfiguration _configuration;
        private readonly INopAliClient _apiClient;

        public NopService(IConfiguration configuration, INopAliClient apiClient)
        {
            _configuration = configuration;
            _apiClient = apiClient;
        }
        public async Task<int> GetProductsCount()
        {
            var response =await _apiClient.Get<int>($"{ _configuration["Nop:GetProductCountUrl"]}");
            return response.Count;
        }  

        public async Task UpdateProduct(UpdateProductDto product)
        {
           await _apiClient.Put<UpdateProductDto>($"{_configuration["Nop:UpdateProductUrl"]}{product.Id}", new { product });
        }

        public async Task<List<Product>> GetProducts(int pageNo)
        {
            var response = await _apiClient.Get<ParineProducts>(string.Format($"{_configuration["Nop:GetProductUrl"]}", pageNo));

            return response.products;
        }

       
    }
}
