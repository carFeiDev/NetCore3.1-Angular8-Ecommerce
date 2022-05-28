import { Component, OnDestroy} from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs'
import { MatDialog } from '@angular/material/dialog';
import { User } from '../../models/user';
import { UserType } from '../../models/usertype';
import { AuthenticationService } from '../../services/authentication.service';
import { SubscriptionService } from '../../services/subscription.service';
import { UserService } from '../../services/user.service';
import { FavoritelistService } from 'src/app/services/favoritelist.service';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-nav-menus',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnDestroy {
  isExpanded = false;
  userId;
  userDataSubcription: any;
  cartItemCount$: Observable<number>;
  userData = new User();
  favoriteListCount$: Observable<number>;
  userType = UserType;
  userImage
  
  constructor(private router: Router,
    private authService: AuthenticationService,
    private userService: UserService,
    private subscriptionService: SubscriptionService,
    private favoritelistService: FavoritelistService,
    public dialog: MatDialog ) {
    this.userDataSubcription = this.subscriptionService.userData.asObservable()
      .subscribe(data => {
        this.userData = data;
      });
    this.userId = localStorage.getItem('userId');
    this.favoritelistService.getFavoritelistItems(this.userId).subscribe();
    this.userService.getCartItemCount(this.userId).subscribe((data: number) => {
      this.subscriptionService.cartItemcount$.next(data);
    });
  }
  
  ngOnInit(): void {
    this.userDataSubcription = this.subscriptionService.userData.asObservable()
      .subscribe(data => {
        this.userData = data;
      });
    this.cartItemCount$ = this.subscriptionService.cartItemcount$;
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  ngOnDestroy(): void {
    if (this.userDataSubcription) {
      this.userDataSubcription.unsubscribe();
    }
  }

  openDialog(): void {
    this.dialog.open(LoginComponent, {
      height: '620px',

    });
  }

  getGameDetails() {
    this.userService.getUserById(this.userId)
      .subscribe((result: User) => {
        this.userImage = result;
      }, error => {
        console.log("Error ocurred while fetching game data:", error);
      }
    )
  }
}



