import { Component } from '@angular/core';
import { NgbConfig } from '@ng-bootstrap/ng-bootstrap'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'sunApp';
  constructor (ngbConfig: NgbConfig){
    ngbConfig.animation = false;
  }
}
