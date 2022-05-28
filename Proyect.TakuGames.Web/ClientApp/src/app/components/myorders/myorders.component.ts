import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { CustomerOrder } from '../../models/customerorder';
import { CartService } from '../../services/cart.service';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-myorders',
  templateUrl: './myorders.component.html',
  styleUrls: ['./myorders.component.scss']
})
export class MyordersComponent implements OnInit {
  userId:any;
  listCustomerOrder: CustomerOrder[]
  totalPriceOrder: number
  isLoading: boolean = false;
  private unsubscribe$ = new Subject<void>();

  constructor(private orderService: OrderService, private cartservice: CartService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.userId = localStorage.getItem('userId');
    this.getAllOrderItems();

  }

  getAllOrderItems(): void {
    this.orderService.getAllCustomerOrderUser(this.userId)
      .pipe(takeUntil(this.unsubscribe$))
        .subscribe((customerorder: CustomerOrder[]) => {
          this.listCustomerOrder = customerorder;
          this.getTotalPiceOrder();
          this.isLoading = false;
        }, error => {
          console.log('Error ocurred while ferching order details:', error);
        });
  }

  getTotalPiceOrder(): void {
    this.totalPriceOrder = 0;
    this.listCustomerOrder.forEach(x => x.customerOrderDetails.forEach(item => {
      this.totalPriceOrder += item.totalPrice;
    }))
  }
  
  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

}



