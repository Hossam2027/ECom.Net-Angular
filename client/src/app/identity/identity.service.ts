import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  baseUrl = 'http://hossammohammed.runasp.net/api/Auth/activate';

  usernameSource = new BehaviorSubject<string | null>(null);
  username$ = this.usernameSource.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router
  ) {
    if (typeof window !== 'undefined') {
      this.usernameSource.next(window.localStorage.getItem('username'));
    }
  }

  setUsername(username: string) {
    window.localStorage.setItem('username', username);
    this.usernameSource.next(username);
  }

  clearUsername() {
    window.localStorage.removeItem('username');
    this.usernameSource.next(null);
  }

  activate(email: string, token: string) {
    return this.http.post(this.baseUrl, { email, token });
  }

  login(model: any) {
    return this.http.post(
      'http://hossammohammed.runasp.net/api/Auth/login',
      model
    );
  }

  forgetPassword(Email: string) {
    return this.http.post(
      'http://hossammohammed.runasp.net/api/Auth/forget-password',
      { Email }
    );
  }

  resetPassword(model: any) {
    return this.http.post(
      'http://hossammohammed.runasp.net/api/Auth/reset-password',
      model
    );
  }

  register(model: any) {
    return this.http.post(
      'http://hossammohammed.runasp.net/api/Auth/register',
      model
    );
  }
}