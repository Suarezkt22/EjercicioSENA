import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient } from '@angular/common/http';

import { STUDENTS_SERVICE } from './core/interfaces/student-service.interface';
import { StudentService } from './core/services/student.service';
import { SessionService } from './core/services/session.service';
import { SESSION_SERVICE } from './core/interfaces/session-service.interface';
import { provideToastr } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    { provide: STUDENTS_SERVICE, useClass: StudentService },
    { provide: SESSION_SERVICE, useClass: SessionService },
    provideAnimations(),
    provideToastr({
      timeOut: 5000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      progressBar: true,
      closeButton: true,
    }),
  ],
};