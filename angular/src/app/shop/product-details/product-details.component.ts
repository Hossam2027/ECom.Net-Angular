import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { NgxImageZoomModule } from "ngx-image-zoom";

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [RouterModule, CommonModule, NgxImageZoomModule],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {




  product: any;
MainImage: any;
qunatity: any;
loading: any;
reviews: any;
imageUrl: string = '';
  constructor(private route: ActivatedRoute, private productService: ProductService) { }
  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.productService.getproduct(id).subscribe({
      next: (res) => {
        this.product = res;
           if (res?.photos?.length) {
        this.imageUrl =
          // 'https://hossammohammed.runasp.net/images/products/' +
          'https://localhost:44344/images/products/' +
          res.photos[0].imageName;
      }
      }
    });

  }

}
