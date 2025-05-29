// core/services/session.service.ts
import { Injectable } from '@angular/core';
import { ISessionService } from '../interfaces/session-service.interface';

@Injectable({
  providedIn: 'root'
})
export class SessionService implements ISessionService {
  private readonly currentStudentKey = 'current_student';
  private readonly hasAppliedProgramKey = 'has_applied_program';
  private readonly currentProgramKey = 'current_program';
  private readonly hasEnrolledCoursesKey = 'has_enrolled_courses';

  setCurrentStudent(studentId: number): void {
    localStorage.setItem(this.currentStudentKey, studentId.toString());
  }

  getCurrentStudent(): number | null {
    const studentId = localStorage.getItem(this.currentStudentKey);
    return studentId ? parseInt(studentId, 10) : null;
  }

  clearSession(): void {
    localStorage.removeItem(this.currentStudentKey);
    localStorage.removeItem(this.hasAppliedProgramKey);
    localStorage.removeItem(this.hasEnrolledCoursesKey);
  }

  setProgramApplied(programId : number): void {
    localStorage.setItem(this.hasAppliedProgramKey, 'true');
    localStorage.setItem(this.currentProgramKey, programId.toString());
  }

  getCurrentProgram(): number | null {
    const programId = localStorage.getItem(this.currentProgramKey);
    return programId ? parseInt(programId, 10) : null;
  }

  hasAppliedProgram(): boolean {
    return localStorage.getItem(this.hasAppliedProgramKey) === 'true';
  }

  setCoursesEnrolled(): void {
    localStorage.setItem(this.hasEnrolledCoursesKey, 'true');
  }

  hasEnrolledCourses(): boolean {
    return localStorage.getItem(this.hasEnrolledCoursesKey) === 'true';
  }
}