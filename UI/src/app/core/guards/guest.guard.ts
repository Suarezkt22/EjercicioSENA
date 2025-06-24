import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { SESSION_SERVICE, ISessionService } from '../interfaces/session-service.interface';

@Injectable({
  providedIn: 'root',
})
export class GuestGuard {
  constructor(
    @Inject(SESSION_SERVICE) private readonly sessionService: ISessionService,
    private readonly router: Router
  ) {}

  canActivate(): boolean {
    const hasSession = !!this.sessionService.getToken();
    if (hasSession) {
      this.router.navigate(['/products']); 
    }
    return !hasSession;
  }
}
