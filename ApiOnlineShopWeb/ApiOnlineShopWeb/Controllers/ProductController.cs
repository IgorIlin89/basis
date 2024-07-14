using ApiOnlineShopWeb.Database.Interfaces;
using ApiOnlineShopWeb.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ApiOnlineShopWeb.Domain;

namespace ApiOnlineShopWeb.Controllers;

public class ProductController(IProductRepository _productRepository) : ControllerBase
{
    [Route("product/list")]
    [HttpGet]
    public async Task<ActionResult> GetProductList()
    {
        var productList = _productRepository.GetProductList();

        if (productList == null)
        {
            return NotFound();
        }

        List<ProductDto> response = new List<ProductDto>();

        foreach (var element in productList)
        {
            response.Add(new ProductDto
            {
                ProductId = element.Id,
                Name = element.Name,
                Producer = element.Producer,
                Category = element.Category,
                Picture = element.Picture,
                Price = element.Price
            });
        }

        return Ok(response);
    }

    [Route("product")]
    [HttpGet]
    public async Task<IActionResult> GetProductById([FromQuery]string id)
    {
        var product = _productRepository.GetProductById(Int32.Parse(id));

        if (product == null)
        {
            return NotFound();
        }

        var response = new ProductDto
        {
            ProductId = product.Id,
            Name = product.Name,
            Producer = product.Producer,
            Category = product.Category,
            Picture = product.Picture,
            Price = product.Price
        };

        return Ok(response);
    }

    [Route("product/{id}")]
    [HttpDelete]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        _productRepository.DeleteProduct(id);
        return Ok();
    }

    [Route("product")]
    [HttpPut]
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

        var response = _productRepository.GetProductById(productDto.ProductId.Value);

        if (response == null)
        {
            return StatusCode(500);
        }

        return Ok(response);
    }

    [Route("product")]
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

        var response = _productRepository.GetProductByName(productDto.Name);

        if (response == null)
        {
            return StatusCode(500);
        }

        return Ok(response);
    }
}
