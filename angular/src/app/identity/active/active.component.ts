import { Component, OnInit } from '@angular/core';
import { IdentityService } from '../identity.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-active',
  standalone: true,
  imports: [],
  templateUrl: './active.component.html',
  styleUrl: './active.component.scss'
})
export class ActiveComponent implements OnInit {
constructor(private route: ActivatedRoute, 
  private toastr: ToastrService,
  private service: IdentityService) { }
isActivated: boolean = false;
ngOnInit(): void {
  this.route.queryParams.subscribe(params => {
    const email = params['email'];
    const token = params['token'];

    if (!email || !token) {
      console.error('Missing email or token in URL');
      return;
    }

    this.service.activate(email, token).subscribe({
      next: (res) => {
        console.log('✅ Activation successful:', res);
        this.toastr.success('Email activated successfully! You can now log in.', 'Success');
        this.isActivated = true;
      },
      error: (err) => {
        console.error('❌ Activation failed:', err);
      
      }
    });
  });
}
  
  }
  

