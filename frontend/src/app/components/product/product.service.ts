import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { environment } from '../../core/config/environment';
import { ProductDTO } from './product.dto';
import { PagedResponse } from '../../shared/pagedResponse.dto';
import { UpdateProductDTO } from './updateProduct.dto';
import { CreateProductDTO } from './createProduct.dto';
import { ApiResponseWithData } from '../../shared/apiResponseWithData.dto';

@Injectable({ providedIn: 'root' })
export class ProductsService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiBaseUrl;

  get(params?: any): Observable<PagedResponse<ProductDTO>> {
    return this.http.get<PagedResponse<ProductDTO>>(`${this.baseUrl}/api/product`, { params });
  }

  getById(id: string): Observable<ProductDTO> {
    return this.http.get<ApiResponseWithData<ProductDTO>>(`${this.baseUrl}/api/product/${id}`)
      .pipe(map((response: ApiResponseWithData<ProductDTO>) => {
          return response.data;
      }));
  }

  create(product: CreateProductDTO): Observable<ProductDTO> {
    return this.http.post<ApiResponseWithData<ProductDTO>>(`${this.baseUrl}/api/product`, product)
      .pipe(map((response: ApiResponseWithData<ProductDTO>) => {
          return response.data;
      }));
  }

  update(id: string, product: UpdateProductDTO): Observable<ProductDTO> {
    return this.http.put<ApiResponseWithData<ProductDTO>>(`${this.baseUrl}/api/product/${id}`, product)
      .pipe(map((response: ApiResponseWithData<ProductDTO>) => {
          return response.data;
      }));
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/product/${id}`);
  }
}
