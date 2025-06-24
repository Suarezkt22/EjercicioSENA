import { NgModule } from "@angular/core";
import { Route, RouterModule } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { GuestGuard } from "../../core/guards/guest.guard";

const routes: Route[] = [
  {
    path: "",
    component: LoginComponent,
    canActivate: [GuestGuard]
  },
  {
    path: "register",
    component: RegisterComponent,
    canActivate: [GuestGuard]
  }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule],
})
export class UsersRoutingModule { }