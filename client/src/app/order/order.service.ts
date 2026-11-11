import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IOrder } from '../shared/orders';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) {}

  private baseUrl = "http://hossammohammed.runasp.net/api/Orders";

  getCurrentOrderForUser(id: number) {
    return this.http.get<IOrder>(`${this.baseUrl}/GetOrderById/${id}`);
  }

  getAllOrderForUser() {
    return this.http.get<IOrder[]>(`${this.baseUrl}/GetOrdersForUser`);
  }
}
