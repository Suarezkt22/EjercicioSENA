import { Component, Inject, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import {
  IStudentService,
  STUDENTS_SERVICE,
} from '../../core/interfaces/student-service.interface';
import {
  SESSION_SERVICE,
  ISessionService,
} from '../../core/interfaces/session-service.interface';
import {
  GetClassmatesResponse,
  GetCoursesResponse,
} from '../../core/models/student.model';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { extractErrorMessage } from '../../core/utils/utils';

@Component({
  selector: 'app-course-classmates',
  templateUrl: './course-classmates.component.html',
  imports: [CommonModule, RouterModule],
  standalone: true,
  styleUrls: ['./course-classmates.component.css'],
})
export class CourseClassmatesComponent implements OnInit {
  enrolledCourses: GetCoursesResponse[] = [];
  classmates!: GetClassmatesResponse
  selectedCourseId: number | null = null;
  isLoading = false;
  studentId!: number;

  constructor(
    private readonly router: Router,
    private readonly toastr : ToastrService,
    @Inject(STUDENTS_SERVICE) private readonly studentService: IStudentService,
    @Inject(SESSION_SERVICE) private readonly sessionService: ISessionService
  ) {}

  ngOnInit(): void {
    this.redirectBasedOnProgress()
    this.initializeClassmates();
  }

  private async initializeClassmates(): Promise<void> {
    try {
      this.isLoading = true;
      await this.loadEnrolledCourses();
    } catch(err) {
      this.toastr.error(extractErrorMessage(err))
      this.logout();
    } finally {
      this.isLoading = false;
    }
  }

  private async loadEnrolledCourses(): Promise<void> {
    try {
      const response = await firstValueFrom(
        this.studentService.getEnrolledCourses(this.studentId)
      );

      this.enrolledCourses = response.data || [];

      if (this.enrolledCourses.length > 0) {
        this.selectedCourseId = this.enrolledCourses[0].courseId;
        await this.loadClassmates();
      }
    } catch (err) {
      this.toastr.error(extractErrorMessage(err))
    }
  }

  async onCourseSelect(courseId: number): Promise<void> {
    this.selectedCourseId = courseId;
    await this.loadClassmates();
  }


  private async loadClassmates(): Promise<void> {
    if (!this.selectedCourseId) return;

    try {
      const response = await firstValueFrom(
        this.studentService.getClassmates(this.studentId, this.selectedCourseId)
      );

      this.classmates = response.data;
    } catch (err) {
      this.toastr.error(extractErrorMessage(err))
    }
  }

  logout(): void {
    this.sessionService.clearSession();
    this.router.navigate(['']);
  }

  getSelectedCourseName(): string {
    const selectedCourse = this.enrolledCourses.find(
      (c) => c.courseId === this.selectedCourseId
    );
    return selectedCourse ? selectedCourse.name : 'Materia seleccionada';
  }

  private redirectBasedOnProgress(): void {
    const studentId = this.sessionService.getCurrentStudent();

    if (!studentId) {
      this.router.navigate(['']);
      this.sessionService.clearSession();
      return;
    }

    this.studentId = studentId;

    if (!this.sessionService.hasAppliedProgram()) {
      this.router.navigate(['/apply-program']);
      return;
    }

    if (this.sessionService.hasEnrolledCourses()) {
      this.router.navigate(['/classmates']);
    }
  }
}
