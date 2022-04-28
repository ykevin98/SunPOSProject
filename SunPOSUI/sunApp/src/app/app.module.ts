import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

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

const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  }
];

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    HomeModule,
    MenuCategoryModule
  ],
  providers: [sunposAPIService],
  bootstrap: [AppComponent]
})
export class AppModule { }
