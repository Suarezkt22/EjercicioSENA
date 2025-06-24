import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { firstValueFrom } from 'rxjs';
import { PRODUCTS_SERVICE, IProductService } from '../../../core/interfaces/product-service.interface';
import { Product, ProductForm, ProductPayload } from '../../../core/models/product.model';
import { extractErrorMessage } from '../../../core/utils/utils';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrl: './update.component.css'
})
export class UpdateComponent {
  // Public properties
  public productForm!: FormGroup<ProductForm>;
  public submitted = false;
  public productloading = false;
  public savingLoading = false;
  
  // Private properties
  private productId = 0;

  constructor(
    private readonly fb: FormBuilder,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly toastr: ToastrService,

    @Inject(PRODUCTS_SERVICE)
    private readonly productService: IProductService
  ) { }

  ngOnInit(): void {
    this.recoverProductSaved();
  }

  private extractProductIdFromRoute(): void {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      this.productId = id ? parseInt(id, 10) : 0;
    });
  }

  // Recover product saved data
  private async recoverProductSaved(){
    this.productloading = true
    this.extractProductIdFromRoute()
    const product = await this.fetchGetByIdProduct()
    this.initializeForm(product)
    this.productloading = false
  }

  // Initialize form
  private initializeForm(productSavedData ?: Product): void {
    this.productForm = this.fb.group<ProductForm>({
      nombre: this.fb.control(productSavedData?.nombre ?? null, [Validators.required]),
      descripcion: this.fb.control(productSavedData?.descripcion ?? null, [Validators.required]),
      precio: this.fb.control(productSavedData?.precio ?? null, [Validators.required, Validators.min(1)]),
      stock: this.fb.control(productSavedData?.stock ?? null, [Validators.required, Validators.min(1)])
    });
  }

  // Form Submission
  public async onSubmit(): Promise<void> {
    this.submitted = true;

    if (this.productForm.invalid) {
      this.toastr.error('Por favor, completa todos los campos requeridos.');
      return;
    }

    await this.fetchUpdateProduct()
  }

  // API Calls
  private async fetchGetByIdProduct() {
    try {
      const response = await firstValueFrom(this.productService.getById(this.productId));
      return response.data;
    } catch (error) {
      this.toastr.error(extractErrorMessage(error));
      this.redirectToProductsPage()
      return;
    }
  }

  private async fetchUpdateProduct() {
    try {
      this.savingLoading = true;
      const result = await firstValueFrom(this.productService.update(this.productId, this.productPayload));
      this.toastr.success(result.message);
      this.redirectToProductsPage()
    } catch (error) {
      this.toastr.error(extractErrorMessage(error));
    } finally {
      this.savingLoading = false;
    }
  }

  // Navigation
  public redirectToProductsPage() {
    this.router.navigate(['../../'], { relativeTo: this.route });
  }

  // Getters
  get inputs() {
    return this.productForm.controls;
  }

  private get productPayload(): ProductPayload {
    const { nombre, descripcion, precio, stock } = this.inputs

    return {
      nombre: nombre.value as string,
      descripcion: descripcion.value as string,
      precio: precio.value as number,
      stock: stock.value as number,
    }
  }
}
