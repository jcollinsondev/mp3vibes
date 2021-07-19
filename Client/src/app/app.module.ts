import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { ProductsPageComponent } from './layout/products-page/products-page.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { MainComponent } from './layout/main/main.component';

import { Configs, APP_CONFIG } from './config/configs';
import { HomeComponent } from './layout/home/home.component';
import { PaddingComponent } from './layout/padding/padding.component';
import { LogoComponent } from './components/logo/logo.component';
import { SearchbarComponent } from './components/searchbar/searchbar.component';
import { CartComponent } from './components/cart/cart.component';
import { FilterComponent } from './components/filter/filter.component';
import { RangeSelectorComponent } from './components/range-selector/range-selector.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    ProductsPageComponent,
    ProductListComponent,
    MainComponent,
    HomeComponent,
    PaddingComponent,
    LogoComponent,
    SearchbarComponent,
    CartComponent,
    FilterComponent,
    RangeSelectorComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    FontAwesomeModule
  ],
  providers: [{ provide: Configs, useValue: APP_CONFIG }],
  bootstrap: [AppComponent]
})
export class AppModule { }
