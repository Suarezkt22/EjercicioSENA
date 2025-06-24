import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CreateComponent } from "./create/create.component";
import { UpdateComponent } from "./update/update.component";
import { ListComponent } from "./list/list.component";
import { ProductsRoutingModule } from "./products-routing.module";
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
  declarations: [
    CreateComponent,
    UpdateComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    ProductsRoutingModule,
    MatPaginatorModule,
    MatSortModule
  ],
})
export class ProductsModule {}
