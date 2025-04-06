using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class BasketService(IRepository<WebAppDatabaseContext> repository,WebAppDatabaseContext context,IHttpContextAccessor httpContextAccessor) : IBasketService
{
    public async Task<Basket?> RetrieveBasket(string userId,CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(userId))
        {
            httpContextAccessor.HttpContext.Response.Cookies.Delete("userId");
            return null;
        }
        var basket = await context.Baskets
            .Include(b => b.Items)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync( x => x.UserId == userId );
        return basket;
    }
    
    public async Task<ServiceResponse<Basket>> AddToBasket(string userId,string? claimUserId,Guid productId,int quantity,CancellationToken cancellationToken)
    {
        
        var basket = await RetrieveBasket(userId,cancellationToken);
        if (basket == null)
        {
            basket = CreateBasket(claimUserId);
        }
        var basketItem = await context.Products.FindAsync(productId);
        if (basketItem == null)
        {
            return ServiceResponse.FromError<Basket>(CommonErrors.BasketItemNotFound);
        }
        
        basket.AddItem(basketItem, quantity);
        

        var result = await context.SaveChangesAsync() > 0;

        if (result) return ServiceResponse.ForSuccess(basket);
        
        return ServiceResponse.FromError<Basket>(CommonErrors.CouldNotSave);
    }
    
    public async Task<ServiceResponse> UpdateBasket(string userId,BasketDto basketDto,CancellationToken cancellationToken = default)
    {
        var basket = await RetrieveBasket(userId,cancellationToken);
        if (basket == null)
        {
            return ServiceResponse.FromError(CommonErrors.BasketNotFound);
        }
        basket.Items.Clear();
        foreach (var item in basketDto.Items)
        {
            var product = await context.Products.FindAsync(item.ProductId);
            if (product == null)
            {
                return ServiceResponse.FromError(CommonErrors.BasketItemNotFound);
            }
            basket.AddItem(product, item.Quantity);
        }
        basket.UserId = basketDto.UserId;
        var result = await context.SaveChangesAsync() > 0;
        if (result) return ServiceResponse.ForSuccess();
        return ServiceResponse.FromError(CommonErrors.CouldNotSave);
    }
    
    public async Task<ServiceResponse> DeleteFromBasket(string userId,Guid productId,int quantity,CancellationToken cancellationToken = default)
    {
        var basket = await RetrieveBasket(userId,cancellationToken);
        if (basket == null)
        {
            return ServiceResponse.FromError(CommonErrors.BasketNotFound);
        }
        basket.RemoveItem(productId, quantity);
        var result = await context.SaveChangesAsync() > 0;
        if (result) return ServiceResponse.ForSuccess();
        return ServiceResponse.FromError(CommonErrors.CouldNotSave);
    }
    
    
    private Basket CreateBasket(string? userId)
    {
        if (userId == null)
        {
            userId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions
            {
                IsEssential = true,
                Expires = DateTime.Now.AddMonths(1)
            };
            httpContextAccessor.HttpContext.Response.Cookies.Append("userId", userId, cookieOptions);
        }
        var basket = new Basket
        {
            UserId = userId
        };
        context.Baskets.Add(basket);
        return basket;
    }
    

  
    
}