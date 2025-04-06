export interface Product {
    id: string
    createdAt: string
    updatedAt: string
    name: string
    description: string
    price: number
    type: string
    brand: string
    stock: number
  }

export interface ProductParams{
    search?: string
    page: number
    pageSize: number
  
}
  