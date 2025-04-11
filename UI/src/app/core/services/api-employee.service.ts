import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { APIEmployee } from "../models/employee.model";
import { Observable } from "rxjs";
import { APIResponse } from "../models/standard.model";
import { IEmployeeService } from "../interfaces/employee-service.interface";
import { EMPLOYEES_API } from "../../../environments/config";

@Injectable({
    providedIn: 'root'
  })
export class APIEmployeeService implements IEmployeeService {
    constructor(private readonly httpClient : HttpClient){}

    public getAll() : Observable<APIResponse<APIEmployee[]>>{
        return this.httpClient.get<APIResponse<APIEmployee[]>>(EMPLOYEES_API.GET_ALL_EMPLOYEES);
    }

    public getById(id: number) : Observable<APIResponse<APIEmployee>>{
        return this.httpClient.get<APIResponse<APIEmployee>>(`${EMPLOYEES_API.GET_BY_ID_EMPLOYEE}/${id}`);
    }
}