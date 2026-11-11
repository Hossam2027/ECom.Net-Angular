import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from './core/nav-bar/nav-bar.component';
import { CategoryService } from './services/category.service';
import { CommonModule } from '@angular/common';
import { ShopComponent } from "./shop/shop.component";
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavBarComponent, CommonModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent   {


}
