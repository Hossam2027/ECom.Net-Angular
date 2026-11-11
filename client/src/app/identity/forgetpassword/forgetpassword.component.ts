import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { IdentityService } from '../identity.service';

@Component({
  selector: 'app-forgetpassword',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './forgetpassword.component.html',
  styleUrl: './forgetpassword.component.scss'
})
export class ForgetpasswordComponent implements OnInit {
    form: FormGroup;

  constructor(  private fb: FormBuilder,
      private forgetService: IdentityService,
      private toast: ToastrService) {  this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    }); }

  ngOnInit(): void {
  }

  submit() {
    this.forgetService.forgetPassword(this.form.get('email')?.value).subscribe({
      next: (res: any ) => {
        if(res.success) {
          console.log('Forget Password successful:', res);
          this.toast.success('Forget Password successful! Please check your email.');
        } else {
          console.error('Forget Password failed:', res);
          this.toast.error('Forget Password failed. Mail not found.');
        }
      
      }

}
  
    )};
  }