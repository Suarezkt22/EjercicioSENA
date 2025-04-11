import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { EMPLOYEE_SERVICE } from './core/interfaces/employee-service.interface';
import { APIEmployeeService } from './core/services/api-employee.service';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes), 
    provideHttpClient() , 
    {provide: EMPLOYEE_SERVICE, useClass: APIEmployeeService}, provideAnimationsAsync(),
  ]
};
