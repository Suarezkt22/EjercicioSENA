import { Observable } from "rxjs";
import { DataResponse, MessageResponse } from "../models/standard.model";
import { InjectionToken } from "@angular/core";
import { Product, ProductPayload } from "../models/product.model";

export const PRODUCTS_SERVICE = new InjectionToken<IProductService>('ProductService');

export interface IProductService {

  create(payload: ProductPayload) : Observable<MessageResponse>;

  update(productId: number, payload: ProductPayload) : Observable<MessageResponse>;

  getById(productId: number) : Observable<DataResponse<Product>>;

  getAll(): Observable<DataResponse<Product[]>>;

  delete(productId: number) : Observable<MessageResponse>;
  
}
