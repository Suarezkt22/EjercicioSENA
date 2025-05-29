import { InjectionToken } from "@angular/core";

export const SESSION_SERVICE = new InjectionToken<ISessionService>('SessionService');

export interface ISessionService {
  setCurrentStudent(studentId: number): void;
  getCurrentStudent(): number | null;
  clearSession(): void;
  setProgramApplied(programId : number): void;
  getCurrentProgram(): number | null
  hasAppliedProgram(): boolean;
  setCoursesEnrolled(): void;
  hasEnrolledCourses(): boolean;
}
