import { FormControl } from "@angular/forms";

export interface Token {
    token: string
}

export interface UserForm {
  email: FormControl<string | null>;
  password: FormControl<string | null>;
}

export interface UserPayload {
  email: string;
  password: string;
}



