import { Routes } from '@angular/router';
import path from 'path';
export const routes: Routes = [
    //   {path: 'shop', component: ShopComponent},
    //   {path: 'home', component: HomeComponent},
    // {path:'details/:id',component:ProductDetailsComponent},
    { path: 'details/:id', loadComponent: () => import('./shop/product-details/product-details.component').then(c => c.ProductDetailsComponent) },
    { path: 'home', loadComponent: () => import('./home/home.component').then(c => c.HomeComponent) },
    { path: 'shop', loadComponent: () => import('./shop/shop.component').then(c => c.ShopComponent) },
    { path: 'basket', loadComponent: () => import('./basket/basket/basket.component').then(c => c.BasketComponent) },
    // { path: 'checkout', loadComponent: () => import('./checkout/stepper/stepper.component').then(c => c.StepperComponent) },
    { path: 'checkout', loadComponent: () => import('./checkout/checkout/checkout/checkout.component').then(c => c.CheckoutComponent) },
    { path: 'sucess', loadComponent: () => import('./checkout/sucess/sucess.component').then(c => c.SucessComponent) },
    { path: 'orderitem', loadComponent: () => import('./orderitem/orderitem.component').then(c => c.OrderitemComponent) },
    { path: 'register', loadComponent: () => import('./identity/register/register.component').then(c => c.RegisterComponent) },
    { path: 'confirm-email', loadComponent: () => import('./identity/active/active.component').then(c => c.ActiveComponent) },
    { path: 'login', loadComponent: () => import('./identity/login/login.component').then(c => c.LoginComponent) },
    { path: 'forget-password', loadComponent: () => import('./identity/forgetpassword/forgetpassword.component').then(c => c.ForgetpasswordComponent) },
    { path: 'reset-password', loadComponent: () => import('./identity/resetpassword/resetpassword.component').then(c => c.ResetpasswordComponent) },
    { path: '**', redirectTo: '', pathMatch: 'full' }



];
