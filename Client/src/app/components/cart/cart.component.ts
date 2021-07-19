import { Component, OnInit, Type } from '@angular/core';

import { IProduct } from 'src/app/dtos';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  productList: { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}[] = [];
  total!: string;

  constructor(private cart: CartService) { }

  ngOnInit(): void {
    this.setProductList(this.cart.get());
    this.cart.subscribe(this.setProductList.bind(this));
  }

  setProductList(products: { product: IProduct, shortType: Type<IProduct>, extendedType: Type<IProduct>}[]): void {
    this.productList = products;
    this.total = this.productList
    .reduce((total, { product }) => total = total + product.price, 0)
    .toLocaleString('en-US', { minimumFractionDigits: 2, useGrouping: false });
  }

}
