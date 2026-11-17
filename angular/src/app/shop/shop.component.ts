import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ToastrService } from 'ngx-toastr';

import { ShopItemComponent } from './shop-item/shop-item.component';
import { ProductService } from '../services/product.service';
import { CategoryService } from '../services/category.service';
import { SearchService } from '../Search/search.service';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    CommonModule,
    ShopItemComponent,
    FormsModule,
    PaginationModule
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {

  products: any[] = [];
  filteredProducts: any[] = [];
  pagedProducts: any[] = [];

  categories: any

  searchText = '';

  totalItems = 0;
  itemsPerPage = 3;
  currentPage = 1;

  startItem = 0;
  endItem = 0;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private searchService: SearchService,
    private toastr: ToastrService,
    private route: ActivatedRoute 
  ) { }

  ngOnInit(): void {

    this.getProducts();

    this.categoryService.getcategories().subscribe({
      next: (res) => {
        this.categories = res;
      }
    });

    this.searchService.search$.subscribe(text => {

      this.searchText = text;

      this.applySearch();

    });

  }

  getProducts() {

    this.productService.getproducts().subscribe({

      next: (res) => {

        this.products = res;

        this.applySearch();

      }

    });

  }

  applySearch() {

    if (!this.products.length) return;

    if (this.searchText.trim() === '') {

      this.filteredProducts = [...this.products];

    } else {

      const search = this.searchText.toLowerCase();

      this.filteredProducts = this.products.filter(product =>
        Object.values(product).some(value =>
          value?.toString().toLowerCase().includes(search)
        )
      );

    }

    this.currentPage = 1;
    this.totalItems = this.filteredProducts.length;

    this.updatePagedProducts();

  }

  filterByCategory(categoryId: number) {

    if (categoryId === 0) {

      this.filteredProducts = [...this.products];

    } else {

      this.filteredProducts = this.products.filter(
        p => Number(p.categoryId) === Number(categoryId)
      );

    }

    this.currentPage = 1;
    this.totalItems = this.filteredProducts.length;

    this.updatePagedProducts();

  }

  onSortChange(event: any) {

    const value = event.target.value;

    switch (value) {

      case 'Name':

        this.filteredProducts.sort((a, b) =>
          a.name.localeCompare(b.name)
        );

        break;

      case 'PriceAsc':

        this.filteredProducts.sort((a, b) =>
          a.price - b.price
        );

        break;

      case 'PriceAsn':

        this.filteredProducts.sort((a, b) =>
          b.price - a.price
        );

        break;

    }

    this.updatePagedProducts();

  }

  updatePagedProducts() {

    const startIndex = (this.currentPage - 1) * this.itemsPerPage;

    const endIndex = startIndex + this.itemsPerPage;

    this.pagedProducts = this.filteredProducts.slice(startIndex, endIndex);

    this.startItem = this.filteredProducts.length ? startIndex + 1 : 0;

    this.endItem = Math.min(endIndex, this.filteredProducts.length);

  }

  onPageChange(event: any) {

    this.currentPage = event.page;

    this.updatePagedProducts();

  }

}