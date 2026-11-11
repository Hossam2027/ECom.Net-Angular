import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Delivery } from '../shared/delivery';
import { ICreateOrder, IOrder } from '../shared/orders';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = 'http://hossammohammed.runasp.net/api/Auth/address';
  constructor(private http: HttpClient) { }
  updateaddress(form: any) {
    return this.http.put(this.baseUrl, form );
  }
getDeliveryMethod( ) {
  return this.http.get<Delivery[]>(
    `http://hossammohammed.runasp.net/api/Orders/GetDeliveryMethod`
  );
}

createorder(order: ICreateOrder) {
  return this.http.post<IOrder>(`http://hossammohammed.runasp.net/api/Orders`, order)
}
}


      //  private order: any;

  // setOrder(order: any) {
  //   this.order = order;
  // }

  // getOrder() {
  //   return this.order;
  // }