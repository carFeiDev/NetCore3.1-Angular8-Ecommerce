import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { GameDetailsComponent } from "./components/game-details/game-details.component";
import { LoginComponent } from "./components/login/login.component";
import { UserRegistrationComponent } from "./components/user-registration/user-registration.component";
import { AdminAuthGuard } from "./guards/admin-auth.guard";
import { HomeComponent } from "./home/home.component";
import { MyordersComponent } from "./components/myorders/myorders.component";
import { ShoppingCartComponent } from "./components/shopping-cart/shopping-cart.component";
import { ProductSearchComponent } from "./components/product-search/product-search.component";
import { FavoritelistComponent } from "./components/favoritelist/favoritelist.component";
import { AuthGuard } from "./guards/auth.guard";
import { CheckoutComponent } from "./components/checkout/checkout.component";

const appRoutes: Routes = [

  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'filter', component: ProductSearchComponent },
  { path: 'search', component: ProductSearchComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: UserRegistrationComponent },
  { path: 'register/:id', component: UserRegistrationComponent },
  { path: 'games/details/:id', component: GameDetailsComponent },
  { path: 'myorders', component: MyordersComponent },
  { path: 'shopping-cart', component: ShoppingCartComponent },
  { path: 'product-search', component: ProductSearchComponent },
  { path: 'checkout', component: CheckoutComponent, canActivate: [AuthGuard] },
  { path: 'myorders', component: MyordersComponent, canActivate: [AuthGuard] },
  { path: 'favoritelist', component: FavoritelistComponent ,canActivate: [AuthGuard]},
  {
    path: 'admin/games', loadChildren: () => import('./admin/admin.module').then(mod => mod.AdminModule),  
    canLoad: [AdminAuthGuard],
    canActivate: [AdminAuthGuard]
  },
];

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(appRoutes),
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
