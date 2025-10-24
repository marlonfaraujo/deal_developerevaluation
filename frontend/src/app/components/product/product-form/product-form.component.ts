import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProductsService } from '../product.service';
import { ProductDTO } from '../product.dto';
import { CreateProductDTO } from '../createProduct.dto';
import { UpdateProductDTO } from '../updateProduct.dto';
import { Observable } from 'rxjs';
import { PoButtonModule, PoFieldModule, PoLoadingModule, PoPageModule, PoToasterModule, PoToasterType } from '@po-ui/ng-components';
import { ApiResponse } from '../../../shared/apiResponse.dto';

@Component({
  selector: 'app-product-form',
  imports: [CommonModule, ReactiveFormsModule, PoButtonModule, PoFieldModule, PoPageModule, PoLoadingModule, PoToasterModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent implements OnInit {
  private productService = inject(ProductsService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  toasterProperties: IToasterProperties = { message: '', type: PoToasterType.Information, hide: true };
  isEditMode = false;
  isHideLoading = true;

  form = new FormGroup({
    code: new FormControl('', Validators.required),
    name: new FormControl('', Validators.required),
    price: new FormControl(0, [Validators.required, Validators.min(0)])
  });

  ngOnInit(): void {
    const productId = this.route.snapshot.paramMap.get('id');
    if (productId) {
      this.isEditMode = true;
      this.loadProduct(productId);
      this.form.get('code')?.disable();
    }
  }

  private loadProduct(id: string): void {
    this.productService.getById(id).subscribe({
      next: product => this.form.patchValue(product),
      error: err => console.error('Erro ao carregar produto', err)
    });
  }

  save(): void {
    if (this.form.invalid) return;

    this.isHideLoading = false;

    const request$ = this.getRequest();
    request$.subscribe({
      next: () => {
        this.isHideLoading = true;
        this.router.navigate(['/products']);
      },
      error: (err: ApiResponse) => { 
        this.isHideLoading = true;
        this.toasterProperties = { message: err.message || 'Erro ao salvar produto', type: PoToasterType.Error, hide: false };
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/products']);
  }

  private getRequest(): Observable<ProductDTO> {
    if (this.isEditMode) {
      const product: UpdateProductDTO = {
        id: this.route.snapshot.paramMap.get('id')!,
        ...this.form.value 
      } as UpdateProductDTO;
      return this.productService.update(product.id, product);
    }
    const product: CreateProductDTO = this.form.value as CreateProductDTO;
    return this.productService.create(product);
  }
}

interface IToasterProperties {
  message: string;
  type: PoToasterType;  
  hide: boolean;
} 
