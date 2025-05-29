import { FormArray, FormControl } from "@angular/forms";

export interface GetAllProgramsResponse {
  programId: number;
  name: string;
}

export interface GetCoursesResponse {
  courseId: number;
  name: string;
  credits : number;
  teacher?: TeacherDTO | null;
}

interface TeacherDTO {
  teacherId: number;
  name: string;
}

export interface GetClassmatesResponse {
  courseId: number;
  courseName: string;
  classmatesNames: string[];
}

export interface RegisterStudentForm {
  name: FormControl<string | null>;
}

export interface ApplyProgramForm {
  programId: FormControl<number | null>;
}

export interface EnrollCoursesForm {
  courses: FormArray<FormControl<number | null>>;
}

