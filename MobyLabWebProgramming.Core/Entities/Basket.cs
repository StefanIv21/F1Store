﻿using MobyLabWebProgramming.Backend.Entities;

namespace MobyLabWebProgramming.Core.Entities;

public class Basket : BaseEntity
{
    public string UserId { get; set; }
    
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();

    public void AddItem(Product product, int quantity)
    {
        if (Items.All(item => item.ProductId != product.Id))
        {
            Items.Add(new BasketItem
            {
                Product = product,
                Quantity = quantity
            });
        }
        var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
    }
    
    public void RemoveItem(Guid productId, int quantity)
    {
        var item = Items.FirstOrDefault(item => item.ProductId == productId);
        if (item != null)
        {
            item.Quantity -= quantity;
            if (item.Quantity <= 0)
            {
                Items.Remove(item); 
            }
        }
    }
    
    
}