import { Routes } from "@angular/router";
import { AuthGuard } from "./core/guards/auth.guard";
import { GuestGuard } from "./core/guards/guest.guard";

export const routes: Routes = [
  { 
    path: '', 
    loadChildren: () => import('./features/users/users.module').then(m => m.UsersModule),
    canActivate: [GuestGuard]
  },
  { 
    path: 'products', 
    loadChildren: () => import('./features/products/products.module').then(m => m.ProductsModule),
    canActivate: [AuthGuard],
  },
];
