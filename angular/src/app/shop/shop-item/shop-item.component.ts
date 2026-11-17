import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterLink } from "@angular/router";
import { BasketService } from '../../basket/basket.service';
@Component({
  selector: 'app-shop-item',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './shop-item.component.html',
  styleUrl: './shop-item.component.scss'
})
export class ShopItemComponent {
  constructor(private basketService: BasketService) { }
@Input() product: any;
addToCart() {
  this.basketService.addToCart(this.product);
}
}