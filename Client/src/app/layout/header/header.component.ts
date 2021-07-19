import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';

import { IconDefinition, faHamburger, faBars, faTimes, faShoppingCart } from '@fortawesome/free-solid-svg-icons';

import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, AfterViewInit {

  @ViewChild('menu') menu!: ElementRef;
  @ViewChild('cartIcon') cartIcon!: ElementRef;

  icons: { [name: string]: IconDefinition } = { faHamburger, faBars, faTimes, faShoppingCart };

  constructor(private cart: CartService) { }
  
  ngOnInit(): void { 
  }

  ngAfterViewInit(): void {
    const initialProducts = this.cart.get();
    if (initialProducts.length > 0) this.cartIcon.nativeElement.setAttribute('data-cart', initialProducts.length);
    this.cart.subscribe(products => {
      if (products.length > 0) this.cartIcon.nativeElement.setAttribute('data-cart', products.length);
      else this.cartIcon.nativeElement.removeAttribute('data-cart');
    });
  }

  openMenu(): void {
    this.menu.nativeElement.classList.add('visible');
  }

  closeMenu(): void {
    this.menu.nativeElement.classList.remove('visible');
  }

}
