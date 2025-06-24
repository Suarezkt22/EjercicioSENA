import { NgModule } from "@angular/core";
import { Route, RouterModule } from "@angular/router";
import { AuthGuard } from "../../core/guards/auth.guard";
import { ListComponent } from "./list/list.component";
import { CreateComponent } from "./create/create.component";
import { UpdateComponent } from "./update/update.component";

const routes: Route[] = [
  {
    canActivate: [AuthGuard],
    path: "",
    component: ListComponent
  },
  {
    canActivate: [AuthGuard],
    path: "create",
    component: CreateComponent
  },
  {
    canActivate: [AuthGuard],
    path: "update/:id",
    component: UpdateComponent
  }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule],
})
export class ProductsRoutingModule { }