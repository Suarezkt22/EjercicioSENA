import { Routes } from "@angular/router";
import { AuthGuard } from "./core/guards/auth.guard";
import { ProgressGuard } from "./core/guards/progress.guard";
import { ApplyProgramComponent } from "./features/apply-program/apply-program.component";
import { EnrollCoursesComponent } from "./features/enroll-courses/enroll-courses.component";
import { RegisterComponent } from "./features/register-student/register-student.component";
import { CourseClassmatesComponent } from "./features/course-classmates/course-classmates.component";


export const routes: Routes = [
  { path: '', component: RegisterComponent },
  { 
    path: 'apply-program', 
    component: ApplyProgramComponent,
    canActivate: [AuthGuard],
    data: { requiredProgress: 'register' }
  },
  { 
    path: 'enroll-courses', 
    component: EnrollCoursesComponent,
    canActivate: [AuthGuard, ProgressGuard],
    data: { requiredProgress: 'apply-program' }
  },
  { 
    path: 'classmates', 
    component: CourseClassmatesComponent,
    canActivate: [AuthGuard, ProgressGuard],
    data: { requiredProgress: 'enroll-courses' }
  },
  { path: '**', redirectTo: '/' }
];
