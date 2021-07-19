import { Injectable, Type } from '@angular/core';
import { Subject, Subscription } from 'rxjs';

import { Configs } from '../config/configs';
import { IProduct } from '../dtos';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private storageKey: string = '__cart__';
  private changes: Subject<{ product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}[]>;

  constructor(private configs: Configs) { 
    this.changes = new Subject<{ product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}[]>();
  }

  subscribe(next?: ((value: {
    product: IProduct;
    shortType: Type<IProduct>;
    extendedType: Type<IProduct>;
  } []) => void) | undefined, error?: ((error: any) => void) | undefined, complete?: (() => void) | undefined): Subscription {
    return this.changes.subscribe(next, error, complete);
  }

  add(product: { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}): void {
    const saved = this.get();
    if (saved.find(source => this.find(source, product))) return;
    this.set([ ...saved, product ]);
  }

  remove(product: { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}): void {
    const saved = this.get();
    this.set(saved.filter(source => !this.find(source, product)));
  }

  get(): { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}[] {
    const saved = sessionStorage.getItem(this.storageKey);
    if (!saved) return [];
    return JSON.parse(saved).map(this.fromSaveFormat.bind(this));
  }

  private find(
    source: { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>},
    toFind: { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}
  ): boolean {
    return source.product.id === toFind.product.id &&
    source.shortType === toFind.shortType &&
    source.extendedType === toFind.extendedType
  }

  private set(products: { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}[]): void {
    sessionStorage.removeItem(this.storageKey);
    sessionStorage.setItem(this.storageKey, JSON.stringify(products.map(this.toSaveFormat.bind(this))));
    this.changes.next(products);
  }

  private toSaveFormat(product: { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}): { product: IProduct, type: string } {
    return {
      product: product.product,
      type: this.findType(product.shortType) ?? 'undefined'
    };
  }

  private fromSaveFormat(product: { product: IProduct, type: string }): { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>} {
    const [ shortType, extendedType ] = this.configs.productTypes[product.type];
    
    return {
      product: product.product,
      shortType,
      extendedType
    };
  }

  private findType(type: Type<IProduct>): string | undefined {
    return Object.keys(this.configs.productTypes)
    .find(key => this.configs.productTypes[key].find(productType => productType === type));
  }
}
