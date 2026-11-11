import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { A11yModule } from "@angular/cdk/a11y";
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { IdentityService } from '../identity.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, A11yModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  formgroup!: FormGroup;
  constructor(private fb: FormBuilder,
    private router: Router,
    private toastr: ToastrService
    , private registerService: IdentityService) { }
  ngOnInit(): void {
    this.formgroup = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(4)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });

  };

  get name() {
    return this.formgroup.get('name');
  }
  get email() {
    return this.formgroup.get('email');
  }
  get displayName() {
    return this.formgroup.get('displayName');
  }
  get password() {
    return this.formgroup.get('password');
  }
  submit() {

    if (this.formgroup.valid) {

      this.registerService.register(this.formgroup.value).subscribe({

        next: (res: any) => {
          if (res.success) {
            console.log('Success', res);
            this.toastr.success('Registered Successfully , Please check your email to activate your account', 'Success  ');
            this.router.navigate(['/login']);
          
          } else {
            console.error('Error', res);
            this.toastr.error(res.message );
          }

      

        }
      });   
   
       
      
  

    }
  }
}

