import { Component, Inject } from '@angular/core';
import { Product } from '../../../core/models/product.model';
import { IProductService, PRODUCTS_SERVICE } from '../../../core/interfaces/product-service.interface';
import { firstValueFrom } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { extractErrorMessage } from '../../../core/utils/utils';
import { ISessionService, SESSION_SERVICE } from '../../../core/interfaces/session-service.interface';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class ListComponent {
  // Public properties
  public products: Product[] = [];
  public displayedColumns: string[] = ['id', 'nombre', 'precio', 'stock', 'fechaCreacion', "acciones"]
  public loadingProductId: number | null = null;

  constructor(
    private readonly toastr: ToastrService,
    private readonly router: Router,
    private readonly route: ActivatedRoute,

    @Inject(PRODUCTS_SERVICE)
    private readonly productService: IProductService,

    @Inject(SESSION_SERVICE)
    private readonly sessionService: ISessionService
  ) { }

  ngOnInit(): void {
    this.fetchProducts();
  }

  // Navigation
  public onCloseSession() {
    this.sessionService.clearSession()
    this.router.navigate(["/"])
  }

  public onCreateProduct() {
    this.router.navigate(["create"], { relativeTo: this.route })
  }

  public onEdit(product: Product): void {
    this.router.navigate(["update", product.id], { relativeTo: this.route })
  }

// API calls
  private async fetchProducts() {
    try {
      const result = await firstValueFrom(this.productService.getAll());
      this.products = result.data
    } catch (error) {
      this.toastr.error(extractErrorMessage(error));
    }
  }

  public async onDelete(product: Product) {
    try {
      this.loadingProductId = product.id
      const result = await firstValueFrom(this.productService.delete(product.id))
      await this.fetchProducts()
      this.toastr.success(result.message)
    } catch (error) {
      this.toastr.error(extractErrorMessage(error));
    } finally {
      this.loadingProductId = null;
    }
  }
}
