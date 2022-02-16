import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder,FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { AuthenticationService } from '../../services/authentication.service';
import { CartService } from '../../services/cart.service';
import { SubscriptionService } from '../../services/subscription.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  userId:any;
  userDataSubscription: any;
  loginForm: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationServices: AuthenticationService,
    private subscriptionService: SubscriptionService,
    private cartServices: CartService,
    private userServices: UserService,
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<LoginComponent>) {
      this.buildForm();
     }

  ngOnInit(): void {
    this.userDataSubscription = this.subscriptionService.userData.asObservable().subscribe(
      (data: User) => {
        this.userId = data.userId;
      }
    )
  }

  ngOnDestroy(): void {
    if (this.userDataSubscription) {
      this.userDataSubscription.unsubscribe();
    }
  }

  login(): void  {
    if (this.loginForm.valid) {
      const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || '/';
      this.authenticationServices.login(this.loginForm.value)
        .subscribe(() => {  
          this.setShoppingCart();
          this.closeDialog();
        }, () => {
          this.loginForm.reset();
          this.loginForm.setErrors({ invalidLogin: true });
        });
    }
  }

  routerLinkRegisterUser(): void {
    this.router.navigate(['/register']);
    this.closeDialog();
  }

  setShoppingCart(): void {
    this.cartServices.setCart(this.authenticationServices.oldUserId, this.userId)
      .subscribe(result => {
        this.userServices.cartItemcount$.next(result);
      }, error => {
        console.log('Error ocurred while setting shopping cart:', error);
      });
  }

  private buildForm(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  private closeDialog(): void {
    this.dialogRef.close();
  }

  get username(): AbstractControl {
    return this.loginForm.get('username');
  }

  get password(): AbstractControl {
    return this.loginForm.get('password');
  }

}
