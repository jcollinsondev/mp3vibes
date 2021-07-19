import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <app-header></app-header>
    <app-main>
      <router-outlet></router-outlet>
    </app-main>
    <app-footer></app-footer>
  `
})
export class AppComponent {
  title = 'Client';
}
