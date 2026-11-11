import { Component, Input, OnInit } from '@angular/core';
import { StepperComponent } from "../../stepper/stepper.component";
import { OrderTotalComponent } from "../../../shared/order-total/order-total.component";
import { Basket } from '../../../basket/basket/basket';
import { BasketService } from '../../../basket/basket.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [StepperComponent, OrderTotalComponent],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss'
})
export class CheckoutComponent implements OnInit   {
  constructor(private basketService: BasketService) {}
  basket: Basket | null = null;
 shippingPrice = 0;

  ngOnInit(): void {
    this.basketService.basket$.subscribe((basket: Basket | null) => {
      this.basket = basket;
    });
    this.basketService.shippingPrice$.subscribe(price => {
      this.shippingPrice = price;
    });
  }


    getSubTotal(): number {
    return this.basket?.items?.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0
    ) || 0;
  }
  getTotal(): number {
    return this.getSubTotal() + this.shippingPrice;
  }

}
