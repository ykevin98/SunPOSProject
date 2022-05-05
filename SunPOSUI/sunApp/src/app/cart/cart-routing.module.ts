import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { MainLayoutComponent } from "../layout/main-layout/main-layout.component";
import { CartComponent } from "./cart.component";

const routes: Routes = [
    {
        path: 'cart',
        component: MainLayoutComponent,
        children: [
            { path: '', component: CartComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class CartRoutingModule{

}