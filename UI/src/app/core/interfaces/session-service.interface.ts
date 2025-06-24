import { InjectionToken } from "@angular/core";

export const SESSION_SERVICE = new InjectionToken<ISessionService>('SessionService');

export interface ISessionService {
  setToken(token: string): void;

  getToken(): string | null;
  
  clearSession(): void
}
