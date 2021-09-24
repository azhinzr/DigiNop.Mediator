using ApiClient;
using ApiClient.Digikala;
using Contracts;
using Contracts.Digikala;
using Contracts.Digikala.updates;
using Mediator.ACL.Digikala.services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator.ACL.Digikala
{
    public class DigikalaService : IDigikalaService
    {
        private readonly IConfiguration _configuration;
        private readonly IDigikalaApiClient _apiClient;

        public DigikalaService(IConfiguration configuration, IDigikalaApiClient apiClient)
        {
            _configuration = configuration;
            _apiClient = apiClient;
        } 

        public async Task<Item> GetProductbyId(string Id)
        { 
            var response = await _apiClient.Get<Data>(string.Format($"{_configuration["Digikala:GetProductUrl"]}", Id));
            return response.Data.items.FirstOrDefault();
        }

        public async Task UpdateVariant(UpdateProductDto dto, int id)
        {
            var response =await _apiClient.Put<UpdateProductDto>(string.Format($"{_configuration["Digikala:UpdateProductUrl"]}", id), dto);
            //if (response.Status != "ok")
            //{
            //    throw new Exception($"problem in updating product with variant id in digikala {id} ---" + response.data != null ? $" digi error : " +
            //        $"-{response.data.price}" +
            //        $"-{response.data.package_length}" +
            //        $"-{response.data.package_weight}" +
            //        $"-{response.data.package_width}" +
            //        $"-{response.data.package_height}" +
            //        $"-{response.data.seller_physical_stock}" : "");
            //}
        } 
    } 
}
