import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
baseUrl = 'https://localhost:44344/api/Product';
  constructor(private http: HttpClient  ) { }
  getproducts() {
return this.http.get<any[]>(`${this.baseUrl}/getall`);  }
  getproduct(id: number) {
return this.http.get<any>(`${this.baseUrl}/${id}`);  }
}
