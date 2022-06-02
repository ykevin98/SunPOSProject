import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { LoginRoutingModule } from './login-page-routing.module';
import { LoginPageComponent } from './login-page.component';

@NgModule({
    imports:[
        CommonModule,
        LoginRoutingModule,
        NgbModule
    ],
    declarations: [LoginPageComponent],
    bootstrap: [LoginPageComponent]
})

export class LoginPageModule { }