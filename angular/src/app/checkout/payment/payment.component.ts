import { Component, Input, OnInit } from '@angular/core';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from '../../basket/basket.service';
import { ICreateOrder } from '../../shared/orders';
import { FormGroup } from '@angular/forms';
import { MatButton } from "@angular/material/button";
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [MatButton],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.scss'
})
export class PaymentComponent implements OnInit {
  @Input() deliveryMethodId!: FormGroup;
  @Input() address!: FormGroup;
  constructor(private checkoutService: CheckoutService, private toast: ToastrService
    , private basketService: BasketService,
    private router: Router
  ) { }

  createorder() {
    const basket = this.basketService.currentBasket;
    const order = this.getordercreate(basket);
    this.checkoutService.createorder(order).subscribe({
      next: (response) => {
        console.log('Order created successfully', response);
        this.router.navigate(['/sucess'],{ queryParams: { id: response.id } });
        this.toast.success('Order created successfully');
      },
      error: (error) => {
         console.log('Full Error =>', error);
    console.log('Error Body =>', error.error);
    console.log('Status =>', error.status);
        this.toast.error('Failed to create order');
      }
    });
  }

  getordercreate(basket: any): ICreateOrder {
    return {
      basketId: basket.id,
      deliveryMethodId: this.deliveryMethodId.value.deliveryMethodId,
      shippingAddress: this.address.value
    };
  }

  ngOnInit(): void {
  }

}
