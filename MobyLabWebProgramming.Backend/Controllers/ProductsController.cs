using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Backend.Entities;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Handlers;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : BaseResponseController
{
    
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<ProductDto>>>> GetProducts([FromQuery]PaginationSearchQueryParams productParams)
    {
        try
        {
            return FromServiceResponse(await productService.GetProducts(productParams));
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error retrieving products.", Detail = e.Message });
        }
    }
    
    [HttpGet("{id:guid}", Name = "GetProduct")]
    public async Task<ActionResult<RequestResponse<ProductDto>>> GetProduct([FromRoute] Guid id)
    {
        try
        {
            return FromServiceResponse(await productService.GetProduct(id));
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error retrieving the product.", Detail = e.Message });
        }
    }
    
    [Authorize(Roles = "Admin,Personnel")]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> AddProduct([FromForm] ProductDto product)
    {
        try
        {
            return FromServiceResponse(await productService.AddProduct(product));
            
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error adding the product.", Detail = e.Message });
        }
    }
    
    [Authorize(Roles = "Admin,Personnel")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> UpdateProduct([FromRoute] Guid id, [FromForm] ProductDto product)
    {
        try
        {
           return FromServiceResponse(await productService.UpdateProduct(product,id));
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error updating the product.", Detail = e.Message });
        }
    }
    
    [Authorize(Roles = "Admin,Personnel")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> DeleteProduct([FromRoute] Guid id)
    {
        try
        {
            return FromServiceResponse(await productService.DeleteProduct(id));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error deleting the product.", Detail = ex.Message });
        }
    }
}
