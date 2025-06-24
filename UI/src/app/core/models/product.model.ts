import { FormControl } from "@angular/forms";

export interface Product {
  id: number;
  nombre: string;
  descripcion: string;
  precio: number;
  stock: number;
  fechaCreacion: string; 
}

export interface ProductForm {
  nombre: FormControl<string | null>;
  descripcion: FormControl<string | null>;
  precio: FormControl<number | null>;
  stock: FormControl<number | null>;
}

export interface ProductPayload {
  nombre: string;
  descripcion: string;
  precio: number;
  stock: number;
}


