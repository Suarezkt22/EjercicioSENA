import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('../app/features/employees/employees.component').then(
        (c) => c.EmployeesComponent
      ),
  },
];
