import { Observable } from "rxjs";
import { DataResponse, MessageResponse } from "../models/standard.model";
import { InjectionToken } from "@angular/core";
import { GetAllProgramsResponse, GetClassmatesResponse, GetCoursesResponse } from "../models/student.model";

export const STUDENTS_SERVICE = new InjectionToken<IStudentService>('StudentService');

export interface IStudentService {
  registerStudent(studentData: any): Observable<DataResponse<number>>;

  applyProgram(studentId: number, programId: number): Observable<MessageResponse>;

  enrollCourses(studentId: number, courses: number[]): Observable<MessageResponse>;

  deleteStudent(studentId: number): Observable<MessageResponse>;

  getClassmates(studentId: number, courseId: number): Observable<DataResponse<GetClassmatesResponse>>;

  getEnrolledCourses(studentId: number): Observable<DataResponse<GetCoursesResponse[]>>;

  getCoursesPerProgram(programId : number) : Observable<DataResponse<GetCoursesResponse[]>>;
  
  getAllPrograms(): Observable<DataResponse<GetAllProgramsResponse[]>>;
}
