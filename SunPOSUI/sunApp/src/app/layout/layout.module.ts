import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';

import { CommonModule } from "@angular/common";
import { RouterEvent, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";


@NgModule({
    imports: [CommonModule, RouterModule],
    declarations: [HeaderComponent, FooterComponent, MainLayoutComponent],
    exports: [ MainLayoutComponent ]
})

export class LayoutModule 
{
    
}