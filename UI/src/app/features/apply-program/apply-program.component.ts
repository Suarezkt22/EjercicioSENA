import { Component, Inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import {
  ApplyProgramForm,
  GetAllProgramsResponse,
} from '../../core/models/student.model';
import {
  SESSION_SERVICE,
  ISessionService,
} from '../../core/interfaces/session-service.interface';
import {
  STUDENTS_SERVICE,
  IStudentService,
} from '../../core/interfaces/student-service.interface';
import { extractErrorMessage } from '../../core/utils/utils';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-apply-program',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './apply-program.component.html',
  styleUrls: ['./apply-program.component.css'],
})
export class ApplyProgramComponent implements OnInit {
  programs: GetAllProgramsResponse[] = [];
  applyForm!: FormGroup<ApplyProgramForm>;
  studentId!: number;

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
    this.loadPrograms();
  }


  private initializeForm(): void {
    this.applyForm = this.fb.group<ApplyProgramForm>({
      programId: this.fb.control(null, Validators.required),
    });
  }

  private async loadPrograms(): Promise<void> {
    try {
      this.programs = (
        await firstValueFrom(this.studentService.getAllPrograms())
      ).data;
    } catch (err) {
      this.toastr.error(extractErrorMessage(err))
    }
  }

  public async onSubmit(): Promise<void> {
    const programId = this.applyForm.value.programId;

    if (this.applyForm.valid && programId) {
      try {
        await firstValueFrom(
          this.studentService.applyProgram(this.studentId, programId)
        );
        this.sessionService.setProgramApplied(programId);
        this.router.navigate(['/enroll-courses']);
      } catch (err) {
        this.toastr.error(extractErrorMessage(err))
      }
    }
  }

   private redirectBasedOnProgress(): void {
    const studentId = this.sessionService.getCurrentStudent();

    if (!studentId) {
      this.router.navigate(['']);
      this.sessionService.clearSession();
      return;
    }

    this.studentId = studentId;

    if (this.sessionService.hasAppliedProgram()) {
      this.router.navigate(['/enroll-courses']);
    }
  }
}
