import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { IconDefinition, faSearch } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-searchbar',
  templateUrl: './searchbar.component.html',
  styleUrls: ['./searchbar.component.scss']
})
export class SearchbarComponent implements OnInit {

  searchValue: string = '';
  @ViewChild('input') input!: ElementRef;

  icons: { [name: string]: IconDefinition } = { faSearch };

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  search(): void {
    if (!this.searchValue) return;
    this.router.navigate(['/files'], { queryParams: { filter: `Title Like ${this.searchValue}` } });
  }

  focusin(): void {
    this.searchValue = '';
    this.input.nativeElement.focus();
  }
}
