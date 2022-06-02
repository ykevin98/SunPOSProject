import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';
import { MenuCategoryComponent } from './menu-category.component';
import { MainLayoutComponent} from '../layout/main-layout/main-layout.component';

const routes: Routes = [
    {
        path: 'menu',
        component: MainLayoutComponent,
        children: [
            { path: '', component: MenuCategoryComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class MenuCategoryRoutingModule {
    
}