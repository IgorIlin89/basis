using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Application.Handlers.Interfaces;
using ApiCouponProduct.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiCouponProduct.Controllers;

//[Route("[controller]")]
public class ProductController(IGetProductListCommandHandler getProductListCommandHandler,
    IGetProductByIdCommandHandler getProductByIdCommandHandler,
    IDeleteProductByIdCommandHandler deleteProductByIdCommandHandler,
    IUpdateProductCommandHandler updateProductCommandHandler,
    IAddProductCommandHandler addProductCommandHandler) : ControllerBase
{
    [Route("product/list")]
    [HttpGet]
    public async Task<IActionResult> GetProductList()
    {
        var productList = getProductListCommandHandler.Handle();
        return Ok(productList.MapToDtoList());
    }

    [Route("product/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetProductById(int id)
    {
        var command = new GetProductByIdCommand(id);
        var product = getProductByIdCommandHandler.Handle(command);
        return Ok(product.MapToDto());
    }

    [Route("product/{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var command = new DeleteProductCommand(id);
        deleteProductByIdCommandHandler.Handle(command);
        return Ok();
    }

    [Route("product")]
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto updateProductDto)
    {
        var command = new UpdateProductCommand(updateProductDto);
        var product = updateProductCommandHandler.Handle(command);
        return Ok(product.MapToDto());
    }

    [Route("product")]
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductDto productDto)
    {
        var command = new AddProductCommand(productDto);
        var product = addProductCommandHandler.Handle(command);
        return Ok(product.MapToDto());
    }
}