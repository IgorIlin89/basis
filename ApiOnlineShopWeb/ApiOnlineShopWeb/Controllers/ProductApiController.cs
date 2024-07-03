using ApiOnlineShopWeb.Database.Interfaces;
using ApiOnlineShopWeb.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiOnlineShopWeb.Controllers;

public class ProductApiController(IProductRepository _productRepository) : ControllerBase
{
    [Route("productlist")]
    [HttpGet]
    public async Task<ActionResult> GetProductList()
    {
        var productList = _productRepository.GetProductList();

        List<ProductDto> productListDto = new List<ProductDto>();

        foreach (var element in productList)
        {
            productListDto.Add(new ProductDto
            {
                ProductId = element.Id,
                Name = element.Name,
                Producer = element.Producer,
                Category = element.Category,
                Picture = element.Picture,
                Price = element.Price
            });
        }

        var response = JsonSerializer.Serialize(productListDto);

        return Ok(response);
    }
}
