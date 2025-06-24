import { Injectable } from '@angular/core';
import { ISessionService } from '../interfaces/session-service.interface';

@Injectable({
  providedIn: 'root',
})
export class SessionService implements ISessionService {
  private readonly tokenKey = 'authToken';

  public setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  public getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  public clearSession(): void {
    localStorage.removeItem(this.tokenKey);
  }
}
