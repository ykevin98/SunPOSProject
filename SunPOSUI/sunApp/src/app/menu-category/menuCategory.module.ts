import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { MenuCategoryRoutingModule } from './menuCategory-routing.module';
import { MenuCategoryComponent } from './menu-category.component';

@NgModule({
  imports: [
    CommonModule,
    MenuCategoryRoutingModule,
    NgbModule
  ],
  declarations: [MenuCategoryComponent],
  bootstrap: [MenuCategoryComponent]
})

export class MenuCategoryModule { }
