import { Inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { SESSION_SERVICE, ISessionService } from '../interfaces/session-service.interface';

@Injectable({
  providedIn: 'root'
})
export class ProgressGuard {
  constructor(
    @Inject(SESSION_SERVICE) private readonly sessionService: ISessionService,
    private readonly router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const requiredProgress = route.data['requiredProgress'];
    let hasProgress = false;

    switch(requiredProgress) {
      case 'register':
        hasProgress = true;
        break;
      case 'apply-program':
        hasProgress = this.sessionService.hasAppliedProgram();
        break;
      case 'enroll-courses':
        hasProgress = this.sessionService.hasEnrolledCourses();
        break;
    }

    if(!hasProgress) {
      this.router.navigate(['/']);
    }

    return hasProgress;
  }
}