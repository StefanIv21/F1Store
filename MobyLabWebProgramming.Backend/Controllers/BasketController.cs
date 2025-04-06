using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BasketController(IUserService userService,IBasketService basketService) : AuthorizedController(userService)
{
    
    [HttpGet(Name =  "GetBasket")]
    public async Task<ActionResult<RequestResponse<BasketDto>>> GetBasket()
    {
        var basket = await basketService.RetrieveBasket(GetUserName());
        if(basket == null)
            return NotFound(new ProblemDetails{Title = "Basket not found"});
        return Ok(basket.MapBasketDto());
    }
    private string GetUserName()
    {
        var currentUser = ExtractClaims().Name;
        return currentUser ?? Request.Cookies["userId"];
    }
    

    [HttpPost]
    public async Task<ActionResult<RequestResponse<BasketDto>>>  AddToBasket(Guid productId,int quantity)
    {
        try
        {
            var response = await basketService.AddToBasket(GetUserName(),ExtractClaims().Name,productId,quantity);
            if(response.IsOk && response.Result != null)
                return CreatedAtRoute("GetBasket",response.Result.MapBasketDto());
            return BadRequest(response.Error);
        }catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error adding the product to the basket.", Detail = e.Message });
        }
    }
    
    [HttpDelete]
    public async Task<ActionResult<RequestResponse>> RemoveFromBasket(Guid productId,int quantity)
    {
        try
        {
            return FromServiceResponse(await basketService.DeleteFromBasket(GetUserName(),productId,quantity));
        } catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error removing the product from the basket.", Detail = e.Message });
        }
    }

    [HttpPut]
    public async Task<ActionResult<RequestResponse>> UpdateBasket([FromBody] BasketDto basketDto)
    {
        try
        {
            return FromServiceResponse(await basketService.UpdateBasket(GetUserName(),basketDto));
        } catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error updating the basket.", Detail = e.Message });
        }
    }
}