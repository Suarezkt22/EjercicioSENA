import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { firstValueFrom } from 'rxjs';
import { PRODUCTS_SERVICE, IProductService } from '../../../core/interfaces/product-service.interface';
import { ProductForm, ProductPayload } from '../../../core/models/product.model';
import { extractErrorMessage } from '../../../core/utils/utils';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent implements OnInit {
  //Public properties
  public productForm!: FormGroup<ProductForm>;
  public submitted = false;
  public loading = false;

  constructor(
    private readonly fb: FormBuilder,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly toastr: ToastrService,

    @Inject(PRODUCTS_SERVICE) 
    private readonly productService: IProductService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  // Initialize form
  private initializeForm(): void {
    this.productForm = this.fb.group<ProductForm>({
      nombre: this.fb.control('', [Validators.required]),
      descripcion: this.fb.control('', [Validators.required]),
      precio: this.fb.control(null, [Validators.required, Validators.min(1)]),
      stock: this.fb.control(null, [Validators.required, Validators.min(1)])
    });
  }

  // Form Submission
  public async onSubmit(): Promise<void> {
    this.submitted = true;

    if (this.productForm.invalid) {
      this.toastr.error('Por favor, completa todos los campos requeridos.');
      return;
    }

    await this.fetchCreateProduct() 
  }

  // API Call create product
  private async fetchCreateProduct(){
    try {
      this.loading = true;
      const result = await firstValueFrom(this.productService.create(this.productPayload));
      this.toastr.success(result.message);
      this.redirectToProductsPage()
    } catch (error) {
      this.toastr.error(extractErrorMessage(error));
    } finally {
      this.loading = false;
    }
  }

  // Navigation
  public redirectToProductsPage(){
    this.router.navigate(['../'] , {relativeTo: this.route});
  }

  // Getters
  get inputs() {
    return this.productForm.controls;
  }

  private get productPayload(): ProductPayload{
    const {nombre, descripcion, precio, stock} = this.inputs

    return {
      nombre: nombre.value as string,
      descripcion: descripcion.value as string,
      precio: precio.value as number,
      stock: stock.value as number,
    }
  }
}
