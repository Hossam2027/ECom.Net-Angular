import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Delivery } from '../shared/delivery';
import { ICreateOrder, IOrder } from '../shared/orders';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  // baseUrl = 'https://hossammohammed.runasp.net/api/Auth/address';
    baseUrl = 'https://localhost:44344/api/Auth/address';

  constructor(private http: HttpClient) { }
  updateaddress(form: any) {
    return this.http.put(this.baseUrl, form );
  }
getDeliveryMethod( ) {
  return this.http.get<Delivery[]>(
    // `https://hossammohammed.runasp.net/api/Orders/GetDeliveryMethod`
    `https://localhost:44344/api/Orders/GetDeliveryMethod`
  );
}

createorder(order: ICreateOrder) {
  // return this.http.post<IOrder>(`https://hossammohammed.runasp.net/api/Orders`, order)
  return this.http.post<IOrder>(`https://localhost:44344/api/Orders`, order)
}
}


      //  private order: any;

  // setOrder(order: any) {
  //   this.order = order;
  // }

  // getOrder() {
  //   return this.order;
  // }