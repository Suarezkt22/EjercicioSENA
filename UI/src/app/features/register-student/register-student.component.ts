import { Component, Inject, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ISessionService, SESSION_SERVICE } from '../../core/interfaces/session-service.interface';
import { IStudentService, STUDENTS_SERVICE } from '../../core/interfaces/student-service.interface';
import { firstValueFrom } from 'rxjs';
import { RegisterStudentForm } from '../../core/models/student.model';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { extractErrorMessage } from '../../core/utils/utils';

@Component({
  selector: 'app-register-student',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register-student.component.html',
  styleUrls: ['./register-student.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm!: FormGroup<RegisterStudentForm>;

  constructor(
    private readonly fb: FormBuilder,
    private readonly router: Router,
    private readonly toastr : ToastrService,
    @Inject(STUDENTS_SERVICE) private readonly studentService: IStudentService,
    @Inject(SESSION_SERVICE) private readonly sessionService: ISessionService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    if (this.sessionService.getCurrentStudent()) {
      this.redirectBasedOnProgress();
    }
  }

  private initializeForm(): void {
    this.registerForm = this.fb.group<RegisterStudentForm>({
      name: this.fb.control("", Validators.required)
    });
  }

  public async onSubmit(): Promise<void> {
    if (this.registerForm.valid) {
      try {
        const response = await firstValueFrom(this.studentService.registerStudent(this.registerForm.value));

        this.sessionService.setCurrentStudent(response.data);

        this.router.navigate(['/apply-program']);
      } catch (err) {
        this.toastr.error(extractErrorMessage(err))
      }
    }
  }

  private redirectBasedOnProgress(): void {
    if (!this.sessionService.hasAppliedProgram()) {
      this.router.navigate(['/apply-program']);
    } else if (!this.sessionService.hasEnrolledCourses()) {
      this.router.navigate(['/enroll-courses']);
    } else {
      this.router.navigate(['/classmates']);
    }
  }
}

