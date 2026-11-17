import { Component, Input } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Basket } from '../../basket/basket/basket';

@Component({
  selector: 'app-order-total',
  standalone: true,
  imports: [CurrencyPipe, RouterLink, CommonModule],
  templateUrl: './order-total.component.html',
  styleUrls: ['./order-total.component.scss']
})
export class OrderTotalComponent {
  @Input() shippingPrice = 0;
  @Input() basket!: Basket | null;
  @Input() showCheckoutButton = true;

  getSubTotal(): number {
    return this.basket?.items?.reduce((sum, item) => sum + item.price * item.quantity, 0) || 0;
  }

  getTotal(): number {
    return this.getSubTotal() + this.shippingPrice;
  }
}