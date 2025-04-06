export interface Basket {
    id: string
    userId: string
    items: BasketItem[]
  }
  
  export interface BasketItem {
    productId: string
    name: string
    price: number
    brand: string
    type: string
    quantity: number
  }
  