import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { Employee, EmployeeGridForm, APIEmployee } from '../../core/models/employee.model';
import { CommonModule } from '@angular/common';
import { APIResponse } from '../../core/models/standard.model';
import { EMPLOYEE_SERVICE, IEmployeeService } from '../../core/interfaces/employee-service.interface';
import { LoadingComponent } from "../../shared/components/loading/loading.component";
import { ModalComponent } from "../../shared/components/modal/modal.component";
import { ModalInput, ModalTypes } from '../../shared/components/modal/modal.interface';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, LoadingComponent, ModalComponent],
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css'],
})
export class EmployeesComponent implements OnInit {
  //#region Properties
  /** List of loaded employees */
  public employees: Employee[] = [];
  
  /** Form for employee search */
  public employeesGridForm!: FormGroup<EmployeeGridForm>;
  
  /** View state indicators */
  public isLoading: boolean = false;
  public searchButtonEnable: boolean = true;
  public isFilteredSearch: boolean = false;
  public isModalVisible: boolean = false;

  /** Modal configuration */
  public modalInput!: ModalInput;
  //#endregion

  //#region Constructor
  constructor(
    @Inject(EMPLOYEE_SERVICE)
    private readonly employeeService: IEmployeeService,
    private readonly fb: FormBuilder
  ) {}
  //#endregion

  //#region Lifecycle Hooks
  ngOnInit(): void {
    this.initializeEmployeeGridForm();
    this.searchEmployees();
  }
  //#endregion

  //#region Form Initialization
  /** Initializes the search form */
  private initializeEmployeeGridForm(): void {
    this.employeesGridForm = this.fb.group<EmployeeGridForm>({
      id: this.fb.control(null),
    });
  }
  //#endregion

  //#region Employee Management
  /** Searches employees based on the form */
  public async searchEmployees(): Promise<void> {
    try {
      const employeeId = this.employeesGridForm.controls.id.value;

      if (employeeId) {
        if (this.employees.length === 1 && this.employees[0].id === Number(employeeId)) {
          return;
        }
        await this.loadEmployeeById(employeeId);
        return;
      }

      if (this.employees.length > 1) {
        return;
      }

      await this.loadEmployees();
    } catch (error) {
      if (error instanceof HttpErrorResponse) {
        this.handleEmployeeServiceError(error);
      }
    }
  }

  /** Loads all employees */
  private async loadEmployees(): Promise<void> {
    try {
      this.startLoading();
      const employeesFetched = await this.fetchAllEmployees();
      this.employees = this.mapEmployeesFromAPI(employeesFetched.data);
    } finally {
      this.finishLoading();
    }
  }

  /** Loads a specific employee by ID */
  private async loadEmployeeById(id: number): Promise<void> {
    try {
      this.startLoading();
      const employeeFetched = await this.fetchEmployeeById(id);
      this.employees = [this.mapEmployeeFromAPI(employeeFetched.data)];
      this.setFilteredSearch();
    } finally {
      this.finishLoading();
    }
  }

  /** Clears filters and reloads employees */
  public clean(): void {
    this.loadEmployees();
    this.resetFilteredSearch();
    this.employeesGridForm.reset();
  }

  /** Handles employee service errors */
  private handleEmployeeServiceError(error: HttpErrorResponse): void {
    const errorMessage = error.error?.Message || error.message || "Internal error, please try again later.";
    const modalType = error.status === 500 ? ModalTypes.Error : ModalTypes.Warning;
    this.displayModal(modalType, errorMessage);
  }
  //#endregion

  //#region API Calls
  /** Retrieves all employees from the API */
  private async fetchAllEmployees(): Promise<APIResponse<APIEmployee[]>> {
    return firstValueFrom(this.employeeService.getAll());
  }

  /** Retrieves a specific employee from the API by ID */
  private async fetchEmployeeById(id: number): Promise<APIResponse<APIEmployee>> {
    return firstValueFrom(this.employeeService.getById(id));
  }
  //#endregion

  //#region Mapping
  /** Maps a list of employees from API format */
  private mapEmployeesFromAPI(employees: APIEmployee[]): Employee[] {
    return employees.map((employee) => this.mapEmployeeFromAPI(employee));
  }

  /** Maps a single employee from API format */
  private mapEmployeeFromAPI(employee: APIEmployee): Employee {
    return {
      id: employee.id,
      name: employee.name,
      age: employee.age,
      salary: employee.salary,
      anualSalary: employee.anualSalary,
      profileImg: employee.profileImg,
    };
  }
  //#endregion

  //#region UI State Management
  private startLoading(): void {
    this.searchButtonEnable = false;
    this.isLoading = true;
  }

  private finishLoading(): void {
    this.searchButtonEnable = true;
    this.isLoading = false;
  }

  private setFilteredSearch(): void {
    this.searchButtonEnable = false;
    this.isFilteredSearch = true;
  }

  private resetFilteredSearch(): void {
    this.searchButtonEnable = true;
    this.isFilteredSearch = false;
  }

  private displayModal(type: ModalTypes, message: string): void {
    this.modalInput = { message, modalType: type };
    this.isModalVisible = true;
  }

  public closeModal(): void {
    this.isModalVisible = false;
  }
  //#endregion
}
