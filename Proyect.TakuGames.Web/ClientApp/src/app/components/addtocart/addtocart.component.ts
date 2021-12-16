import { Component,Input } from '@angular/core';
import { User } from '../../models/user';
import { CartService } from '../../services/cart.service';
import { SubscriptionService } from '../../services/subscription.service';
import { SnackbarService } from '../../services/snackbar.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-addtocart',
  templateUrl: './addtocart.component.html',
  styleUrls: ['./addtocart.component.scss']
})
export class AddtocartComponent {
  @Input()
  gameId: number;
  userId;
  userDataSubcription: any;
  userData = new User();
  showSpinner: boolean;

  constructor(
    private cartService: CartService,
    private snackBarService: SnackbarService,
    private subscriptionService: SubscriptionService,
    private router: Router)
    {
      this.userId = localStorage.getItem('userId');
      this.userDataSubcription = this.subscriptionService.userData.asObservable().subscribe(data => {
        this.userData = data;
      });
    }

  addOneItemToCart(): void {
    this.cartService.addItemToCart(this.userId, this.gameId).subscribe(
      result => {
        this.subscriptionService.cartItemcount$.next(result);
        this.snackBarService.showSnackBar('Un producto se ha agregado al carrito de compras');
        this.activeSpinner();
      }, error => {
        console.log('Error ocurred while addToCart data : ', error);
      });
  }

  addToCartAndGo(): void {
    this.cartService.addItemToCart(this.userId, this.gameId).subscribe(
      result => {
        this.subscriptionService.cartItemcount$.next(result);
        this.router.navigate(['/shopping-cart']);
      }, error => {
        console.log('Error ocurred while addToCart data : ', error);
      });
  }

  private activeSpinner(): void {
    this.showSpinner= true;
    setTimeout(() =>{
    this.showSpinner= false;
    },1000);
  }
}
