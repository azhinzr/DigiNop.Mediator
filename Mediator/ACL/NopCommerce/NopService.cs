using ApiClient.NopCommerce;
using Contracts.NopCommerce;
using Mediator.ACL.NopCommerce.services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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
        public async Task<List<Product>> GetProducts(int pageNo)
        {
            var response = await _apiClient.Get<List<Product>>(string.Format($"{_configuration["Nop:GetProductUrl"]}", pageNo));

            return response.products;
        } 
    }
}
