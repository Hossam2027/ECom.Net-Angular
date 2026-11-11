import { Component, Inject, OnInit, PLATFORM_ID, HostListener } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Observable, map } from 'rxjs';
import { NavigationEnd } from '@angular/router';

import { BasketService } from '../../basket/basket.service';
import { IdentityService } from '../../identity/identity.service';
import { SearchService } from '../../Search/search.service';
import { filter } from 'rxjs/operators';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatBadgeModule } from '@angular/material/badge';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    FormsModule,
    MatIconModule,
    MatButtonModule,
    MatBadgeModule
  ],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent implements OnInit {

  count$!: Observable<number>;
  showSearch = false;
username: string | null = null;

  searchText = '';

  visible = false;

  isMenuOpen = false;

  constructor(
    private basketService: BasketService,
    private searchService: SearchService,
    private router: Router,
    private identityService: IdentityService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) { }

  ngOnInit(): void {

    if (isPlatformBrowser(this.platformId)) {

      const basketId = localStorage.getItem('basket_id');

      if (basketId) {
        this.basketService.getBasket(basketId).subscribe();
      }
         this.identityService.username$.subscribe(name => {
    this.username = name;
  });
      // this.username = localStorage.getItem('username') || '';
    }
    

    this.count$ = this.basketService.basket$.pipe(
      map(basket =>
        basket?.items?.reduce((sum, item) => sum + item.quantity, 0) || 0
      )
    );
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.showSearch = this.router.url.startsWith('/shop');
      });

    // لتحديد القيمة عند أول تحميل
    this.showSearch = this.router.url.startsWith('/shop');
  }

  filterBySearch() {
    this.searchService.setSearch(this.searchText);
  }

  resetSearch() {
    this.searchText = '';
    this.searchService.setSearch('');
  }

  logout() {
    localStorage.removeItem('username');
    localStorage.removeItem('token'); // لو موجود
    this.username = '';
    this.visible = false;
    this.router.navigate(['/login']);
  }

  toggle(event: MouseEvent) {
    event.stopPropagation();
    this.visible = !this.visible;
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {

    const target = event.target as HTMLElement;

    if (!target.closest('.account')) {
      this.visible = false;
    }

  }
  isLoggedIn(): boolean {
    return !!localStorage.getItem('username');
    // أو استخدم token لو هو اللي بتخزنه
    // return !!localStorage.getItem('token');
  }

}