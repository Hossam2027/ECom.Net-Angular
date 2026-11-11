import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  baseUrl = 'http://hossammohammed.runasp.net/api/Categories';
  constructor(private http: HttpClient) { }
  getcategories() {
    return this.http.get(this.baseUrl);
  }
}

