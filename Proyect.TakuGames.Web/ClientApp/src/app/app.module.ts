
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { GamesService } from './services/games.service';
import { NgMaterialModule } from './ng-material/ng-material.module';
import { GameCardComponent } from './components/game-card/game-card.component';
import { GameDetailsComponent } from './components/game-details/game-details.component';
import { GameFilterComponent } from './components/game-filter/game-filter.component';
import { LoginComponent } from './components/login/login.component';
import { UserRegistrationComponent } from './components/user-registration/user-registration.component';
import { AppRoutingModule } from './app-routing-module';
import { HomeComponent } from './home/home.component';
import { HttpInterceptorService } from './services/http-interceptor.service';
import { AddtocartComponent } from './components/addtocart/addtocart.component';
import { MyordersComponent } from './components/myorders/myorders.component';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { CartService } from './services/cart.service';
import { OrderService }from './services/order.service';
import { FilterGamesPipe } from './pipes/filter-games.pipe';
import { ProductSearchComponent } from './components/product-search/product-search.component';
import { CarouselMainComponent } from './components/carousel-main/carousel-main.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { FooterComponent } from './components/footer/footer.component';
import { SimilarGamesComponent } from './components/similar-games/similar-games.component';
import { FavoritelistComponent } from './components/favoritelist/favoritelist.component';
import { AddtofavoritelistComponent } from './components/addtofavoritelist/addtofavoritelist.component';
import { SearchComponent } from './components/search/search.component';
import { PriceFilterComponent } from './components/price-filter/price-filter.component';
import { CheckoutComponent } from './components/checkout/checkout.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GameCardComponent,
    GameDetailsComponent,
    GameFilterComponent,
    LoginComponent,
    UserRegistrationComponent,
    AddtocartComponent,
    MyordersComponent,
    ShoppingCartComponent,
    FilterGamesPipe,
    ProductSearchComponent,
    CarouselMainComponent,
    FooterComponent,
    SimilarGamesComponent,
    FavoritelistComponent,
    AddtofavoritelistComponent,
    SearchComponent,
    PriceFilterComponent,
    CheckoutComponent,
   
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    NgMaterialModule,
    AppRoutingModule,
    CarouselModule.forRoot(),
  ],
  providers: [GamesService,CartService,OrderService,
    { provide: MAT_DIALOG_DATA, useValue: {} },
    { provide: MatDialogRef, useValue: {} },
    { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true },
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
