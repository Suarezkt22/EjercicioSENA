import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IProductService } from "../interfaces/product-service.interface";
import { Observable } from "rxjs";
import { ProductPayload, Product } from "../models/product.model";
import { MessageResponse, DataResponse } from "../models/standard.model";
import { PRODUCTS_API } from "../../../environments/config";

@Injectable({
  providedIn: 'root'
})
export class ProductService implements IProductService {
  constructor(private readonly httpClient: HttpClient) {}

  public create(payload: ProductPayload): Observable<MessageResponse> {
    return this.httpClient.post<MessageResponse>(
      PRODUCTS_API,
      payload
    );
  }

  public update(productId: number, payload: ProductPayload): Observable<MessageResponse> {
    return this.httpClient.put<MessageResponse>(
      `${PRODUCTS_API}${productId}`,
      payload
    );
  }

  public getById(productId: number): Observable<DataResponse<Product>> {
    return this.httpClient.get<DataResponse<Product>>(
      `${PRODUCTS_API}${productId}`,
    );
  }

  public getAll(): Observable<DataResponse<Product[]>> {
    return this.httpClient.get<DataResponse<Product[]>>(
      PRODUCTS_API
    );
  }

  public delete(productId: number): Observable<MessageResponse> {
    return this.httpClient.delete<MessageResponse>(
      `${PRODUCTS_API}${productId}`,
    );
  }

}
