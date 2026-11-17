import { Component, inject, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutService } from '../checkout.service';
import { BasketService } from '../../basket/basket.service';
import { MatStepperModule } from '@angular/material/stepper';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatRadioModule } from '@angular/material/radio';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Delivery } from '../../shared/delivery';

@Component({
  selector: 'app-deliverymethod',
  standalone: true,
  imports: [CommonModule, MatStepperModule, MatFormFieldModule, MatRadioModule, ReactiveFormsModule],
  templateUrl: './deliverymethod.component.html',
  styleUrls: ['./deliverymethod.component.scss']
})
export class DeliverymethodComponent implements OnInit {
  private checkoutService = inject(CheckoutService);
  private basketService = inject(BasketService);

  @Input() delivery!: FormGroup;
  deliverys: Delivery[] = [];
order: any;

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethod().subscribe({
      next: (res: Delivery[]) => {
        this.deliverys = res;

        // تحميل سعر الشحن إذا كان فيه قيمة سابقة
        const currentId = this.delivery.get('deliveryMethodId')?.value;
        if (currentId) {
          const delivery = this.deliverys.find(d => d.id === currentId);
          if (delivery) this.basketService.setShippingPrice(delivery);
        }

        // متابعة التغييرات
        this.delivery.get('deliveryMethodId')?.valueChanges.subscribe(id => {
          const delivery = this.deliverys.find(d => d.id === id);
          if (delivery) {
            this.basketService.setShippingPrice(delivery);
          }
        });
      },
      error: (err) => console.error('Failed to load delivery methods', err)
    });
  }
}