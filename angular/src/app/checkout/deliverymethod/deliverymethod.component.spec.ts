import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliverymethodComponent } from './deliverymethod.component';

describe('DeliverymethodComponent', () => {
  let component: DeliverymethodComponent;
  let fixture: ComponentFixture<DeliverymethodComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeliverymethodComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DeliverymethodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
