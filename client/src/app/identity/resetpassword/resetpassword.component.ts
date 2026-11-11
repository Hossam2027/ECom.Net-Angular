import { Component, OnInit } from '@angular/core';
import { IdentityService } from '../identity.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-resetpassword',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './resetpassword.component.html',
  styleUrl: './resetpassword.component.scss'
})
export class ResetpasswordComponent implements OnInit {
  model = {
    Email: '',
    Token: '',
    NewPassword: ''
  };
  confirmPassword: string = '';

  constructor(private resetPasswordService: IdentityService, private route: ActivatedRoute,
  ) { }
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.model.Email = params['email'] || '';
      this.model.Token = params['token'] || '';
    });
  }
  submit() {
    console.log('Token being sent:', this.model.Token);
    console.log('Email:', this.model.Email);
    if (this.passwordMismatch) return;

    this.resetPasswordService.resetPassword(this.model).subscribe({
      next: (res) => {
        console.log('Password reset successful:', res);
      },
      error: (err) => {
        console.error('Password reset failed:', err);
      }
    });

  }
  get passwordMismatch(): boolean {
    return this.model.NewPassword !== this.confirmPassword;
  }

}
