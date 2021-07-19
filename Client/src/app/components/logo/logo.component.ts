import { Component, OnInit } from '@angular/core';

import { IconDefinition, faHeadphones } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-logo',
  templateUrl: './logo.component.html',
  styleUrls: ['./logo.component.scss']
})
export class LogoComponent implements OnInit {

  icons: { [name: string]: IconDefinition } = { faHeadphones };

  constructor() { }

  ngOnInit(): void {
  }

}
