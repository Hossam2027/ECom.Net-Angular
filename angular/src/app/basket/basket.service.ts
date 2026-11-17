import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Basket } from './basket/basket';
import { Delivery } from '../shared/delivery';
import { ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class BasketService {
  private shippingPriceSource = new BehaviorSubject<number>(0);
  shippingPrice$ = this.shippingPriceSource.asObservable();

  private basketSource = new BehaviorSubject<Basket | null>(null);
  basket$ = this.basketSource.asObservable();

  // private baseUrl = 'https://hossammohammed.runasp.net/api/Basket/';
    private baseUrl = 'https://localhost:44344/api/Basket/';


  constructor(private http: HttpClient ,   private toastr: ToastrService
) {}

  setShippingPrice(delivery: Delivery): void {
    this.shippingPriceSource.next(delivery.price);
  }

  getBasket(id: string): Observable<Basket> {
    return this.http.get<Basket>(this.baseUrl + id).pipe(
      map((response: Basket) => {
        this.basketSource.next(response);
        return response;
      })
    );
  }

  setBasket(basket: Basket): Observable<Basket> {
    return this.http.post<Basket>(this.baseUrl, basket).pipe(
      map((response: Basket) => {
        this.basketSource.next(response);
        return response;
      })
    );
  }

  addToCart(product: any, quantity: number = 1): void {
    const item = this.mapProductToBasketItem(product, quantity);
    let basket = this.basketSource.value;

    if (!basket) {
      basket = this.createBasket();
    }

    if (!basket.items) {
      basket.items = [];
    }

    const index = basket.items.findIndex(x => x.id === item.id);
    if (index !== -1) {
      basket.items[index].quantity += quantity;
    } else {
      basket.items.push(item);
    }

    this.setBasket(basket).subscribe({
      next: (res) => {
        this.basketSource.next(res);
        this.toastr.success(
          `${item.name} has been added to your cart.`,
          'Added to Cart'
        );
      },
      error: (err) => {
        console.error('Error updating basket', err);
        this.toastr.error(
          'Failed to add product to cart.',
          'Error'
        );
      }
    });
  }

  private createBasket(): Basket {
    const basket: Basket = {
      id: this.uuidv4(),
      items: []
    };
      if (typeof window !== 'undefined') {
    localStorage.setItem('basket_id', basket.id);
  }

  return basket;

  }

  private mapProductToBasketItem(product: any, quantity: number) {
    return {
      id: product.id,
      name: product.name,
      price: product.price,
      quantity: quantity,
      category: product.category,
      imageUrl: product.photos?.length ? product.photos[0].imageName : ''
    };
  }

  private uuidv4(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (c) => {
      const r = Math.random() * 16 | 0;
      const v = c === 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
  get currentBasket(): Basket | null {
  return this.basketSource.value;
}
}