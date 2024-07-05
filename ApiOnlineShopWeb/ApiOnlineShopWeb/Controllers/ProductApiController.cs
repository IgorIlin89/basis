using ApiOnlineShopWeb.Database.Interfaces;
using ApiOnlineShopWeb.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ApiOnlineShopWeb.Domain;

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

    [Route("getproductbyid{id}")]
    [HttpGet]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = _productRepository.GetProductById(id);

        var response = JsonSerializer.Serialize(new ProductDto
        {
            ProductId = product.Id,
            Name = product.Name,
            Producer = product.Producer,
            Category = product.Category,
            Picture = product.Picture,
            Price = product.Price
        });

        return Ok(response);
    }

    [Route("productdelete{id}")]
    [HttpGet]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        _productRepository.DeleteProduct(id);
        return Ok();
    }

    [Route("productedit")]
    [HttpPost]
    public async Task<ActionResult> EditProduct([FromBody] ProductDto productDto)
    {
        var productToEdit = new Product
        {
            Id = productDto.ProductId.Value,
            Name = productDto.Name,
            Producer = productDto.Producer,
            Category = productDto.Category,
            Picture = productDto.Picture,
            Price = productDto.Price
        };

        _productRepository.EditProduct(productToEdit);

        return Ok();
    }

    [Route("productadd")]
    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] ProductDto productDto)
    {
        var productToAdd = new Product
        {
            Name = productDto.Name,
            Producer = productDto.Producer,
            Category = productDto.Category,
            Picture = productDto.Picture,
            Price = productDto.Price,
        };

        _productRepository.AddProduct(productToAdd);

        return Ok();
    }
}
