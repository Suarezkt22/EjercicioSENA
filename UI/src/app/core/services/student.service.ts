import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IStudentService } from "../interfaces/student-service.interface";
import { Observable } from "rxjs";
import { DataResponse, MessageResponse } from "../models/standard.model";
import { GetClassmatesResponse, GetCoursesResponse, GetAllProgramsResponse } from "../models/student.model";
import { STUDENTS_API, COURSES_API, PROGRAMS_API } from "../../../environments/config";


@Injectable({
  providedIn: 'root'
})
export class StudentService implements IStudentService {
  constructor(private readonly httpClient: HttpClient) {}

  public registerStudent(studentName: string): Observable<DataResponse<number>> {
    return this.httpClient.post<DataResponse<number>>(
      STUDENTS_API.REGISTER_STUDENT,
      studentName
    );
  }

  public applyProgram(studentId: number, programId: number): Observable<MessageResponse> {
    return this.httpClient.patch<MessageResponse>(
      STUDENTS_API.APPLY_PROGRAM,
      { studentId, programId }
    );
  }

  public enrollCourses(studentId: number, coursesIds: number[]): Observable<MessageResponse> {
    return this.httpClient.patch<MessageResponse>(
      STUDENTS_API.ENROLL_COURSES,
      { studentId, coursesIds }
    );
  }

  public deleteStudent(studentId: number): Observable<MessageResponse> {
    return this.httpClient.delete<MessageResponse>(
      `${STUDENTS_API.DELETE_STUDENT}?studentId=${studentId}`
    );
  }

  public getClassmates(studentId: number , courseId : number): Observable<DataResponse<GetClassmatesResponse>> {
    return this.httpClient.get<DataResponse<GetClassmatesResponse>>(
      `${STUDENTS_API.GET_CLASSMATES}?studentId=${studentId}&courseId=${courseId}`
    );
  }

  public getEnrolledCourses(studentId: number): Observable<DataResponse<GetCoursesResponse[]>> {
    return this.httpClient.get<DataResponse<GetCoursesResponse[]>>(
      `${STUDENTS_API.GET_ENROLLED_COURSES}?studentId=${studentId}`
    );
  }

  public getCoursesPerProgram(programId: number): Observable<DataResponse<GetCoursesResponse[]>> {
    return this.httpClient.get<DataResponse<GetCoursesResponse[]>>(
      `${COURSES_API.GET_COURSES_BY_PROGRAM}?programId=${programId}`
    );
  }

  public getAllPrograms(): Observable<DataResponse<GetAllProgramsResponse[]>> {
    return this.httpClient.get<DataResponse<GetAllProgramsResponse[]>>(
      PROGRAMS_API.GET_ALL_PROGRAMS
    );
  }
}
