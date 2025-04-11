import { FormControl } from "@angular/forms";

export interface APIEmployee {
    id: number
    name: string
    age: number
    salary: number
    profileImg: string
    anualSalary: number
}
  
export interface Employee {
    id: number
    name: string
    age: number
    salary: number
    profileImg: string
    anualSalary: number
}

export interface EmployeeGridForm{
    id: FormControl<number | null>;
}

