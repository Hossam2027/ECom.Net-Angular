import { Component, inject } from '@angular/core';
import { FormBuilder, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { MatButtonModule } from '@angular/material/button';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { DeliverymethodComponent } from "../deliverymethod/deliverymethod.component";
import { PaymentComponent } from "../payment/payment.component";
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-stepper',
  standalone: true,
  imports: [
    MatButtonModule,
    MatStepperModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    DeliverymethodComponent,
    MatIconModule,
    PaymentComponent
],
  templateUrl: './stepper.component.html',
  styleUrl: './stepper.component.scss'
})
export class StepperComponent {
  basket: any;
  private checkoutService = inject(CheckoutService);
  private _formBuilder = inject(FormBuilder);
  constructor(private toast: ToastrService
  ) { }
  Address = this._formBuilder.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    street: ['', Validators.required],
    city: ['', Validators.required],
    state: ['', Validators.required],
    zipCode: ['', Validators.required],
    country: ['', Validators.required],
    postalCode: ['', Validators.required],
  });

deliverMethod  = this._formBuilder.group({
  deliveryMethodId: ['', Validators.required]
});

  submit() {
    if (this.Address.invalid) return;

    this.checkoutService.updateaddress(this.Address.value)
      .subscribe({
        next: (res: any) => {
          this.toast.success('Address updated successfully!');

          console.log('Address updated ', res);
        },
        error: (err: any) => {
          console.log(err.status);
          console.log(err.error);
          console.log(err);
        }
      });
  }
  
goToPayment(stepper: MatStepper) {
  stepper.selectedIndex = 3;

}
 }