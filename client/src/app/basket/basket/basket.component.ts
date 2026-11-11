import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from '../basket.service';
import { Basket } from '../basket/basket';
import { OrderTotalComponent } from "../../shared/order-total/order-total.component";
@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule,  OrderTotalComponent],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss'
})
export class BasketComponent implements OnInit {
  basket$ = this.basketService.basket$;
  @Input() showCheckoutButton = true;
shippingPrice = 0;

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    const basketId = localStorage.getItem('basket_id');

    if (basketId) {
      this.basketService.getBasket(basketId).subscribe();
    }
      this.basketService.shippingPrice$.subscribe(price => {
    this.shippingPrice = price;
  });
  }

  // حساب total price
 getTotal(basket: Basket | null): number {
  if (!basket?.items) return 0;

  const subtotal = basket.items.reduce((sum, item) =>
    sum + item.price * item.quantity, 0
  );

  return subtotal + this.shippingPrice;
}

  // حذف عنصر
  removeItem(item: any) {
    const basket = this.getCurrentBasket();
    if (!basket) return;

    basket.items = basket.items.filter(x => x.id !== item.id);

    this.basketService.setBasket(basket).subscribe();
  }

  // helper
  private getCurrentBasket(): Basket | null {
    let current: Basket | null = null;

    this.basketService.basket$.subscribe(value => {
      current = value;
    }).unsubscribe();

    return current;
  }
  decrease(item: any) {
  if (item.quantity > 1) {
    item.quantity--;
  } else {
    this.removeItem(item);
  }

  this.basketService.setBasket(this.getCurrentBasket()!).subscribe()  ;
}
increase(item: any) {
  item.quantity++;
  this.basketService.setBasket(this.getCurrentBasket()!).subscribe()  ; 
}}