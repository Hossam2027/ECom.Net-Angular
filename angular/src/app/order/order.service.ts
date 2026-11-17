import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IOrder } from '../shared/orders';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) {}

  // private baseUrl = "https://hossammohammed.runasp.net/api/Orders";
  private baseUrl = "https://localhost:44344/api/Orders";

  getCurrentOrderForUser(id: number) {
    return this.http.get<IOrder>(`${this.baseUrl}/GetOrderById/${id}`);
  }

  getAllOrderForUser() {
    return this.http.get<IOrder[]>(`${this.baseUrl}/GetOrdersForUser`);
  }
}
