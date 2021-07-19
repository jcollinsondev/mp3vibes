import { Component, Input, OnInit, Type } from '@angular/core';

import { IconDefinition, faShoppingCart, faDollarSign } from '@fortawesome/free-solid-svg-icons';

import { IProduct } from 'src/app/dtos';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  @Input() products!: { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct> }[];
  @Input() cartOptions: { add?: boolean, remove?: boolean } = { add: false, remove: false };

  icons: { [name: string]: IconDefinition } = { faShoppingCart, faDollarSign };

  constructor(private cart: CartService) { }

  ngOnInit(): void {
  }

  addToCart(product: { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}): void {
    this.cart.add(product);
  }

  removeFromCart(product: { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}): void {
    this.cart.remove(product);
  }

  formatPrice(price: number): string {
    return price.toLocaleString('en-US', { minimumFractionDigits: 2, useGrouping: false });
  }

}
