import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { AddUserRoutingModule } from "./add-user.routing.module";
import { AddUserComponent } from "./add-user.component";
import { FormsModule } from "@angular/forms";

@NgModule({
    imports: [
        CommonModule,
        AddUserRoutingModule,
        FormsModule
    ],
    declarations: [ AddUserComponent ]
})

export class AddUserModule{

}