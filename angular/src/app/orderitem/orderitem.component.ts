import { Component, OnInit, Pipe } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { OrderService } from '../order/order.service';
import { IOrder } from '../shared/orders';

@Component({
  selector: 'app-orderitem',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './orderitem.component.html',
  styleUrl: './orderitem.component.scss'
})
export class OrderitemComponent implements OnInit   {
order: IOrder = {} as IOrder;
  id: number = 0;
 
  constructor(private route: ActivatedRoute, 
    private toast:ToastrService,private orderService: OrderService) {}
  // ngOnInit(): void {
  //   this.route.queryParams.subscribe((param) => {
  //     this.id = param['id']; // Assuming the query parameter is named 'orderId'
  //   });

  //   this.orderService.getCurrentOrderForUser(this.id).subscribe({
  //     next: (response) => {
  //       this.order = response;
  //       console.log("Orderid :", this.order);
  //     },
  //     error: (err) => {
  //       console.log(err);
  //     },
  //   });
  // }
 
//  ngOnInit(): void {
//   this.route.queryParams.subscribe((param) => {
//     this.id = +param['id']; // Convert the id to a number

//     if (this.id) {
//       this.orderService.getCurrentOrderForUser(this.id).subscribe({
//         next: (response) => {
//           this.order = response;
//           console.log(this.order);
//         },
//         error: (err) => console.log(err),
//       });
//     }
//   });
// }
// ngOnInit(): void {
//   this.route.queryParams.subscribe((param) => {
//     this.id = param['id']; // ✅ حوّله لـ number بالـ +
    
//     if (this.id) { // ✅ تأكد إن الـ id موجود الأول
//       this.orderService.getCurrentOrderForUser(this.id).subscribe({
//         next: (response) => {
//           this.order = response;
//           console.log(this.order);
//         },
//         error: (err) => {
//           console.log(err);
//         },
//       });
//     }
//   });
// }
ngOnInit(): void {
  this.route.queryParams.subscribe((param) => {

    this.id = +param['id'];

    if (this.id) {

      this.orderService.getCurrentOrderForUser(this.id).subscribe({
        next: (response) => {
          this.order = response;
          console.log(this.order);
          console.log('Full Error =>', response);
    console.log('Status =>', response.status);
        },
        error: (err) => {
          console.log(err);
        }
      });

    }
  });
}
get total(): number {
  return (this.order?.subTotal ?? 0) +
         (this.order?.deliveryMethod?.price ?? 0);
}
getStatusText(status: number): string {
  switch (status) {
    case 0: return 'Pending';
    case 1: return 'Processing';
    case 2: return 'Shipped';
    case 3: return 'Delivered';
    case 4: return 'Cancelled';
    default: return 'Unknown';
  }
}
}


