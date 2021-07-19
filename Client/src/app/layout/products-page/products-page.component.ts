import { Component, OnDestroy, OnInit, Type } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { Configs } from 'src/app/config/configs';

import { IProduct } from 'src/app/dtos';

import { ProductService } from 'src/app/services/api/product.service';
import { UrlParamsService } from 'src/app/services/url-params.service';

@Component({
  selector: 'app-products-page',
  templateUrl: './products-page.component.html',
  styleUrls: ['./products-page.component.scss']
})
export class ProductsPageComponent implements OnInit, OnDestroy {

  productList: { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}[] = [];
  private paramsSubscription!: Subscription;
  
  private loading: boolean = false;

  constructor(private route: ActivatedRoute, private products: ProductService, private params: UrlParamsService, private configs: Configs) { }
  
  ngOnInit(): void {
    this.paramsSubscription = this.params
    .subscribe<{ filter: string, type: string }>(this.route, ({ filter, type }) => {
      if (this.loading) return;
      const types = type in this.configs.productTypes ? [ type ] : Object.keys(this.configs.productTypes);
      this.loadProducts(types, filter);
    });
  }

  ngOnDestroy(): void {
    if (this.paramsSubscription) this.paramsSubscription.unsubscribe();
  }

  private loadProducts(types: string[], filter: string): void {
    this.loading = true;

    // Clean products
    this.productList = [];

    let loaded = 0;
    types.map(type => 
      this.getProcutsOfType(type, filter)
      .subscribe(products => {
        loaded++;
        if (loaded === types.length) this.loading = false;
        this.productList = [ ...this.productList, ...products ];
      })
    );
  }

  private getProcutsOfType(productType: string, filter: string): Observable<{ product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}[]> {
    const [ short, extended ] = this.configs.productTypes[productType];

    return this.products.all(short, { filter })
    .pipe(map(all => all.map((product: IProduct) => ({ 
      product, 
      shortType: short, 
      extendedType: extended 
    }))));
  }

}
