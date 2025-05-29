import { Component, Inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
  FormControl,
} from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';
import {
  EnrollCoursesForm,
  GetCoursesResponse,
} from '../../core/models/student.model';
import {
  SESSION_SERVICE,
  ISessionService,
} from '../../core/interfaces/session-service.interface';
import {
  STUDENTS_SERVICE,
  IStudentService,
} from '../../core/interfaces/student-service.interface';
import { sameTeacherValidator } from '../../core/utils/validators';
import { ToastrService } from 'ngx-toastr';
import { extractErrorMessage } from '../../core/utils/utils';

@Component({
  selector: 'app-enroll-courses',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './enroll-courses.component.html',
  styleUrls: ['./enroll-courses.component.css'],
})
export class EnrollCoursesComponent implements OnInit {
  courses: GetCoursesResponse[] = [];
  enrollForm!: FormGroup<EnrollCoursesForm>;
  studentId!: number;
  programId!: number;

  constructor(
    private readonly fb: FormBuilder,
    private readonly router: Router,
    private readonly toastr : ToastrService,
    @Inject(STUDENTS_SERVICE) private readonly studentService: IStudentService,
    @Inject(SESSION_SERVICE) private readonly sessionService: ISessionService
  ) {}

  ngOnInit(): void {
    this.redirectBasedOnProgress();
    this.initializeForm();
    this.loadCourses();
  }

  private initializeForm(): void {
    this.enrollForm = this.fb.group<EnrollCoursesForm>({
      courses: this.fb.array<FormControl<number | null>>(
        [],
        [
          Validators.required,
          Validators.maxLength(3),
          sameTeacherValidator(this.courses),
        ]
      ),
    });
  }

  private async loadCourses(): Promise<void> {
    try {
      const coursesPerProgram = await firstValueFrom(
        this.studentService.getCoursesPerProgram(this.programId)
      );
      this.courses = coursesPerProgram.data;
    } catch (err) {
      this.toastr.error(extractErrorMessage(err))
    }
  }

  public toggleCourseSelection(courseId: number) {
    const currentSelection = this.enrollForm.controls.courses.value;

    if (currentSelection.includes(courseId)) {
      this.enrollForm.controls.courses.setValue(
        currentSelection.filter((id) => id !== courseId)
      );
    } else if (currentSelection.length < 3) {
      this.enrollForm.controls.courses.setValue([
        ...currentSelection,
        courseId,
      ]);
    }

    this.enrollForm.controls.courses.markAsTouched();
  }

  public onCheckboxChange(e: any, courseId: number): void {
    if (e.target.checked) {
      this.enrollForm.controls.courses.push(this.fb.control(courseId));
    } else {
      const index = this.enrollForm.controls.courses.value.indexOf(courseId);
      if (index >= 0) {
        this.enrollForm.controls.courses.removeAt(index);
      }
    }
  }

  public async onSubmit(): Promise<void> {
    if (this.enrollForm.valid) {
      try {
        const courses = (this.enrollForm.value.courses ?? []).filter(
          (course): course is number => course !== null
        );

        await firstValueFrom(
          this.studentService.enrollCourses(this.studentId, courses)
        );

        this.sessionService.setCoursesEnrolled();
        this.router.navigate(['/classmates']);
      } catch (err) {
        this.toastr.error(extractErrorMessage(err))
      }
    }
  }

  private redirectBasedOnProgress(): void {
    const studentId = this.sessionService.getCurrentStudent();
    const programId = this.sessionService.getCurrentProgram();

    if (!studentId || !programId) {
      this.router.navigate(['']);
      this.sessionService.clearSession();
      return;
    }

    this.studentId = studentId;
    this.programId = programId;

    if (!this.sessionService.hasAppliedProgram()) {
      this.router.navigate(['/apply-program']);
      return;
    }

    if (this.sessionService.hasEnrolledCourses()) {
      this.router.navigate(['/classmates']);
    }
  }

  public isCourseDisabled(course: GetCoursesResponse): boolean {
    if (
      this.enrollForm.controls.courses.value.length >= 3 &&
      !this.enrollForm.controls.courses.value.includes(course.courseId)
    ) {
      return true;
    }

    const selectedCourses = this.courses.filter((c) =>
      this.enrollForm.controls.courses.value.includes(c.courseId)
    );

    return selectedCourses.some(
      (c) => c.teacher?.teacherId === course.teacher?.teacherId
    );
  }

  public isTeacherDisabled(course: GetCoursesResponse): boolean {
    const selectedCourses = this.courses.filter((c) =>
      this.enrollForm.controls.courses.value.includes(c.courseId)
    );

    return (
      selectedCourses.some(
        (c) => c.teacher?.teacherId === course.teacher?.teacherId
      ) && !this.enrollForm.controls.courses.value.includes(course.courseId)
    );
  }
}
