import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';

@Component({
  selector: 'app-sucess',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './sucess.component.html',
  styleUrl: './sucess.component.scss'
})
export class SucessComponent implements OnInit {
  id: number = 0;
constructor(private route: ActivatedRoute) { }
ngOnInit(): void {
  this.route.queryParams.subscribe(params => {
    this.id = params['id'];
        // console.log('Order ID:', this.id); // للتأكد

  });
}
}
