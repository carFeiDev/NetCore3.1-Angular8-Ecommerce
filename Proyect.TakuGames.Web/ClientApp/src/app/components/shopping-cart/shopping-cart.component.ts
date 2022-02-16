import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { User } from 'src/app/models/user';
import { ShoppingCart } from '../../models/shoppingcart';
import { CartService } from '../../services/cart.service';
import { SnackbarService } from '../../services/snackbar.service';
import { SubscriptionService } from '../../services/subscription.service';
import { LoginComponent } from '../login/login.component';


@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.scss']
})
export class ShoppingCartComponent implements OnInit {
  userId;
  totalPrice: number;
  private unsubscribe$ = new Subject<void>();
  listItems: ShoppingCart[];
  totalPriceCart: number;
  userDataSubcription: any;
  userData = new User();
  showSpinner: boolean = false;
  showSpinnerIncr: boolean= false;
  
  constructor(
    private cartService: CartService,
    private snackbarService: SnackbarService,
    private snackBarService: SnackbarService,
    private subscriptionService: SubscriptionService,
    private router: Router,
    private dialog: MatDialog) {
      this.userDataSubcription = this.subscriptionService.userData.asObservable()
      .subscribe(data => {
        this.userData = data;
      });
  }

  ngOnInit():void {
   // this.showSpinner= true;
    this.userId = localStorage.getItem('userId');  
    this.getAllCartItems();
    this.activeSpinner();
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  getAllCartItems() :void {
    this.cartService.getCartItems(this.userId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((data: ShoppingCart[]) => {               
        this.listItems = data;
        this.getTotalPriceCart();        
        //this.showSpinner=false;     
      }, error => {
        console.log('Error ocurred :', error);
      });
  }

  purchaseOrder(): void {
    if(this.userData.isLoggedIn){      
      this.cartService.BuyCart(this.userId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(result => {   
        this.cartService.clearCart(this.userId)
          .subscribe((response) => {
            this.subscriptionService.cartItemcount$.next(response); 
            this.snackbarService.showSnackBar('La orden de compra se completo');
            this.getAllCartItems();
      });
    });
    } else {
      this.router.navigate(['/login']);
    }
  }

  addOneCartItem(gameId: number): void {
    this.cartService.addItemToCart(this.userId, gameId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        result => {
          this.subscriptionService.cartItemcount$.next(result);
          this.snackBarService.showSnackBar('Se agrego una unidad al  carrito');
          this.getAllCartItems();
          this.activeSpinner();
        }, error => {
          console.log('Error ocurred while add cart game : ', error);
        });
  }

  deleteOneCartItem(gameId: number): void {
    this.cartService.deleteOneCartItem(this.userId, gameId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        result => {       
          this.subscriptionService.cartItemcount$.next(result);      
          this.snackBarService.showSnackBar('Una unidad  se ha eliminado del producto');
          this.getAllCartItems(); 
          this.subscriptionService.cartItemcount$.next(result);
        }, error => {
          console.log('Error ocurred  : ', error);
        });
  }

  deleteCartItem(gameId: number): void {
    this.cartService.removeCartItems(this.userId, gameId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        result => {
          this.subscriptionService.cartItemcount$.next(result);
          this.snackBarService.showSnackBar('Producto eliminado del carrito');
          this.getAllCartItems();
        }, error => {
          console.log('Error ocurred while deleting cart game : ', error);
        });
  }

  activeSpinner(): void {
    this.showSpinnerIncr= true;
    setTimeout(() =>{
      this.showSpinnerIncr= false;
    },1000);
  }

  getTotalPriceCart(): void {
    this.totalPriceCart = 0;
    this.listItems.forEach(item => {
      this.totalPriceCart += item.game.price * item.quantity;      
    })
  } 
   
  checkOutUser(): void {
    if(this.userData.isLoggedIn){      
      this.router.navigate(['/checkout']);
    } else {
      this.dialog.open(LoginComponent, {
        height: '770px',  
      });
    }
  }    
}
