import { Observable } from "rxjs";
import { APIEmployee } from "../models/employee.model";
import { APIResponse } from "../models/standard.model";
import { InjectionToken } from "@angular/core";

export const EMPLOYEE_SERVICE = new InjectionToken<IEmployeeService>('EmployeeService');

export interface IEmployeeService {
  getAll(): Observable<APIResponse<APIEmployee[]>>;
  getById(id: number): Observable<APIResponse<APIEmployee>>;
}