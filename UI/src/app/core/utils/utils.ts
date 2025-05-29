import { HttpErrorResponse } from "@angular/common/http";

export function createApiClient<T extends Record<string, string>>(baseUrl: string, endpoints: T): Record<keyof T, string> {
    return Object.fromEntries(
      Object.entries(endpoints).map(([key, endpoint]) => [key, `${baseUrl}${endpoint}`])
    ) as Record<keyof T, string>;
  }
  
export function extractErrorMessage(error: unknown, defaultMessage: string = 'Error desconocido'): string {

    if (error instanceof HttpErrorResponse) {
      if (error.error && typeof error.error === 'object' && 'Message' in error.error) {
        return error.error.Message;
      }

      return defaultMessage;
    }

    return defaultMessage;
}