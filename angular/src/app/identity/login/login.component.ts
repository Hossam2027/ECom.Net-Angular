import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { IdentityService } from '../identity.service';
import { ToastrService } from 'ngx-toastr';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {

  formgroup!: FormGroup;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private loginService: IdentityService,
    private toast: ToastrService ,
    private identityService: IdentityService
  ) { }

  ngOnInit(): void {
    this.formgroup = this.formBuilder.group({
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }
  showPassword = false;

togglePassword() {
  this.showPassword = !this.showPassword;
}

  get Email() {
    return this.formgroup.get('Email');
  }

  get Password() {
    return this.formgroup.get('Password');
  }

  submit() {
    if (this.formgroup.invalid) {
      this.formgroup.markAllAsTouched();
      return;
    }

    // const { Email, Password } = this.formgroup.value;

    this.loginService.login(this.formgroup.value).subscribe({
      next: (res: any) => {
        if (res.success) {
          console.log('Login successful:', res);
          this.toast.success('Login successful!');
          this.identityService.setUsername(res.username); // Update the username in the IdentityService
          localStorage.setItem('token', res.token);   // Store the token in localStorage
              localStorage.setItem('username', res.username);// Store the username in localStorage
              console.log('Username stored in localStorage:', res.username , res.token); // ✅ شوف إيه اللي بيتخزن
          this.router.navigate(['/shop']);

        } else {
          console.error('Login failed:', res);
          this.toast.error(res.error?.message  ,'Login failed. Please check your credentials and try again.');
        }

      },
    });
  }


}



