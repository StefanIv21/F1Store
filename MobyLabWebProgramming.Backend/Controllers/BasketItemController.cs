using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Handlers;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BasketItemController(IBasketItemService basketItemService) : BaseResponseController
{
   
    
    [HttpGet]
    public async Task<ActionResult<RequestResponse<List<BasketItemDto>>>> GetBasketItems(Guid basketId)
    {
        try
        {
            return FromServiceResponse(await basketItemService.GetBasketItems(basketId));
            
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error retrieving basket items.", Detail = e.Message });
        }
    }
    
    [HttpGet("{id:guid}", Name = "GetBasketItem")]
    public async Task<ActionResult<RequestResponse<BasketItemDto>>> GetBasketItem([FromRoute] Guid id)
    {
        try
        {
            return FromServiceResponse(await basketItemService.GetBasketItem(id));
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error retrieving the basket item.", Detail = e.Message });
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> AddBasketItem([FromBody] BasketItemDto basketItem)
    {
        try
        {
            return FromServiceResponse(await basketItemService.AddBasketItem(basketItem));
               
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error adding the basket item.", Detail = e.Message });
        }
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> UpdateBasketItem(Guid id, [FromBody] BasketItemDto basketItem)
    {
        try
        {
            return FromServiceResponse(await basketItemService.UpdateBasketItem(id, basketItem));
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error updating the basket item.", Detail = e.Message });
        }
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> DeleteBasketItem(Guid id)
    {
        try
        {
            return FromServiceResponse(await basketItemService.DeleteBasketItem(id));
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error deleting the basket item.", Detail = e.Message });
        }
    }
    
}