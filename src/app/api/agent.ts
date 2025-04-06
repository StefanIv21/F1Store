import axios from "axios";
import { PaginatedResponse } from "../modules/pagination";
import _, { update } from 'lodash';
import { getToken } from "../modules/user";

axios.defaults.baseURL = "http://localhost:5000/api";
axios.defaults.withCredentials = true;

const responseBody = (response: any) => response.data;

axios.interceptors.request.use((config) => {
  const user = localStorage.getItem('user');
  
  if (!user) return config;  

  const token = getToken(JSON.parse(user));
  
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});


axios.interceptors.response.use(
  async response => {
    const pagination = response.headers["pagination"];
    
    if (pagination) {
      response.data = new PaginatedResponse(response.data, JSON.parse(pagination));
    }
    return response;
  },
  (error) => {
    if (error.response) {
      const { data, status, config } = error.response;
      console.error("API Error: ", data);
      console.error("Status Code: ", status);
      console.error("Request Config: ", config);
      
      if (status === 404) {
        console.error("Resource not found");
      } else if (status === 500) {
        console.error("Server error occurred");
      }
    } else {
      console.error("Network error or no response");
    }

    return Promise.reject(error); 
  }
);

const requests = {
  get: (url: string, params?:URLSearchParams) => axios.get(url, {params}).then(responseBody),
  post: (url: string, body: {}) =>axios.post(url, body,{
    headers: {
      'Content-Type': 'application/json'
    }}).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  del: (url: string) => axios.delete(url).then(responseBody),
  postForm: (url: string, data: FormData) => axios.post(url, data, {
    headers: { 'Content-Type': 'multipart/form-data' }
  }).then(responseBody),
  putForm: (url: string, data: FormData) => axios.put(url, data, {
    headers: { 'Content-Type': 'multipart/form-data' }
  }).then(responseBody)
};

const Catalog = {
  list: (params : URLSearchParams) => requests.get("/products",params),
details: (id: string) => requests.get(`/products/${id}`)
}

const Basket = {
    get: () => requests.get("basket"),
    addItem: (productId: string,quantity = 1) => requests.post(`basket?productId=${productId}&quantity=${quantity}`,{}),
    removeItem: (productId: string,quantity = 1) => requests.del(`basket?productId=${productId}&quantity=${quantity}`)
}

const Address = {
  add: (values:any) => requests.post("address",values),
  update: (values:any) => requests.put("address",values),
  fetchAddress: () => requests.get("address"),
}

const Account = {
  login: (values:any) => requests.post("Authorization/Login",values),
  register: (values:any) => requests.post("user/add",values),
  currentUser: () => requests.get("user/currentUser"),
}

const Feedback = {
  add: (values:any) => requests.post("feedback",values),
}

function createFormData(item: any)
{
  let formData = new FormData();
  for (let key in item) {
      formData.append(key, item[key]);
  }
  return formData;
}

const Admin = {
  createProduct: (values:any) => requests.postForm("products",createFormData(values)),
  updateProduct: (values:any) => requests.putForm(`products/${values.id}`,createFormData(values)),
  deleteProduct: (id: string) => requests.del(`products/${id}`),
}

const agent = {
    Catalog,
    Basket,
    Account,
    Address,
    Admin,
    Feedback
    };

export default agent;

