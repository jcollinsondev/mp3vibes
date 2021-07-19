import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { IconDefinition, faFilter } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {

  filter: { price: { min: number, max: number } } = { price: { min: 0, max: 50 } }; 

  icons: { [name: string]: IconDefinition } = { faFilter };

  open: boolean = false;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  setPriceRange(range: { min: number, max: number }): void {
    this.filter = { ...this.filter, price: range }
  }

  apply() {
    this.router.navigate(['/files'], { queryParams: { filter: `Price GreaterThan ${this.filter.price.min},Price LowerThan ${this.filter.price.max}` } });
  }

}
