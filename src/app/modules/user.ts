import { Basket } from "./basket";

export interface User {
    response: string;
    errorMessage: string;
  }

export interface currentUser {
    id: string;
    email: string;
    name: string;
    role: string;
    basket?: Basket;
}


export function getRole(user: User): string | null {
    const userObject = JSON.parse(JSON.stringify(user.response)).user; 
    return  userObject.role;
}
  
  export function getEmail(user: User): string | null {
        const userObject = JSON.parse(JSON.stringify(user.response)).user; 
        return  userObject.email;    
  }

  export function getToken(user: User): string | null {
    const usertoken = JSON.parse(JSON.stringify(user.response)).token; 
    return  usertoken;
  }

  export function getBasket(user: User): Basket | null {
    const userObject = JSON.parse(JSON.stringify(user.response)).user; 
    return  userObject.basket;
  }

  export function getBasketStorage(cookie : string): Basket | null {
    const userObject = JSON.parse(cookie).response.user; 
    return  userObject.basket;
  }
  