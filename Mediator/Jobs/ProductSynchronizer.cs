using FluentScheduler;
using Mediator.ACL.Digikala.services;
using Mediator.ACL.NopCommerce.services;
using Mediator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator.Jobs
{
    class ProductSynchronizer : IJob
    {
        private readonly INopService _nopService;
        private readonly IDigikalaService _digikalaService;

        public ProductSynchronizer(INopService nopService, IDigikalaService digiService)
        {
            _nopService = nopService;
            _digikalaService = digiService;
        }

        public void Execute()
        {
            Console.WriteLine($" ProductSynchronizer for digikala started"); 

             var total = _nopService.GetProductsCount().GetAwaiter().GetResult();

            var lastPage = total / 50;
            if (total % 50 != 0)
            {
                lastPage++;
            }

            try
            {
                for (var pageNo = 1; pageNo <= lastPage; pageNo++)
                {

                    Console.WriteLine($"page {pageNo} started");
                    var products = _nopService.GetProducts(pageNo).GetAwaiter().GetResult();
                    foreach (var item in products)
                    {  
                        if (!item.published)
                        {
                            Console.WriteLine(" id:" + item.id + "is not published ");
                            continue;
                        } 

                        var combinationFeaturesOfSelectedProduct = item.product_attribute_combinations.ToList();

                        if (!combinationFeaturesOfSelectedProduct.Any())
                        {
                            Console.WriteLine(" id:" + item.id + "has no combination ");
                            continue;
                        }

                        for (var i = 0; i <= combinationFeaturesOfSelectedProduct.Count - 1; i++)
                        {
                            try
                            { 
                                var combSku = Convert.ToInt32(combinationFeaturesOfSelectedProduct[i].sku);

                                if (combSku == 0)
                                {
                                    Console.WriteLine(" id:" + item.id + "has no digikala dkp");
                                    continue;
                                }
                                var digiProduct = _digikalaService.GetProductbyId(combinationFeaturesOfSelectedProduct[i].sku).GetAwaiter().GetResult();
                                if (digiProduct == null)
                                {
                                    Console.WriteLine(" id:" + item.id + "has wrong digikala dkp");
                                    continue;
                                }
                                var isActive = true;
                                if (combinationFeaturesOfSelectedProduct[i].stock_quantity <= 0 && digiProduct.stock.dk_stock <= 0)
                                    isActive = false;
                                var dto = new Contracts.Digikala.updates.UpdateProductDto
                                {
                                    price = Helper.ToDigiPrice((double)combinationFeaturesOfSelectedProduct[i].overridden_price),
                                    seller_stock = combinationFeaturesOfSelectedProduct[i].stock_quantity,
                                    is_active = isActive

                                };
                                _digikalaService.UpdateVariant(dto, combSku);
                                Console.WriteLine($"dkp-{combinationFeaturesOfSelectedProduct[i].sku} was successfully updated in digikala ");
                            }
                            catch (Exception e)
                            {

                                Console.WriteLine(e.Message + "  id in website: " + item.id);
                                continue;
                            }
                        }

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

