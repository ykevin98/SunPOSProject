import { NgModule } from "@angular/core";
import { Routes, RouterModule, Route } from '@angular/router';
import { MainLayoutComponent } from "../layout/main-layout/main-layout.component";
import { AddUserComponent } from "./add-user.component";

const routes: Routes = [
    {
        path: 'addUser',
        component: MainLayoutComponent,
        children: [
            { path: '', component: AddUserComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class AddUserRoutingModule{

}