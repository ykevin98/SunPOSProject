import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import {RouterModule, Routes} from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { sunposAPIService } from './services';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LayoutModule } from './layout/layout.module';
import { HomeModule } from './home/home.module';
import { MenuCategoryModule } from './menu-category/menuCategory.module';
import { MenuCategoryComponent } from './menu-category/menu-category.component';
import { HeaderComponent } from './layout/header/header.component';
import { AddUserComponent } from './add-user/add-user.component';
import { AddUserModule } from './add-user/add-user.module';
import { LoginPageComponent } from './login-page/login-page.component';
import { CartComponent } from './cart/cart.component';
import { CartModule } from './cart/cart.module';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  }
];

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    HomeModule,
    MenuCategoryModule,
    AddUserModule,
    CartModule
  ],
  providers: [sunposAPIService],
  bootstrap: [AppComponent]
})
export class AppModule { }
