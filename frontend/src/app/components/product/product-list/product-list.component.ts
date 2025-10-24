import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PoTableModule, PoFieldModule, PoButtonModule, PoTableColumn, PoInfoModule, PoPageModule, PoPageAction, PoTableColumnSort, PoDialogService, PoTableColumnSortType } from '@po-ui/ng-components';
import { ProductDTO } from '../product.dto';
import { ProductsService } from '../product.service';
import { FormsModule } from '@angular/forms';
import { capitalizeFirstLetter } from '../../../shared/pagination.dto';
import { ProductFilterDTO } from '../product-filter.dto';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    PoTableModule,
    PoFieldModule,
    PoButtonModule,
    PoInfoModule,
    PoPageModule
  ],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements OnInit {

  private productsService = inject(ProductsService);
  private router = inject(Router);
  products: ProductDTO[] = [];
  searchName = '';
  searchCode = '';

  columns: PoTableColumn[] = [
    { property: 'code', label: 'Código do Produto' },
    { property: 'name', label: 'Nome' },
    { property: 'price', label: 'Preço', type: 'currency', format: 'BRL' }
  ];

  public readonly actions: Array<PoPageAction> = [
    { label: 'Novo Produto', url: '/products/new', icon: 'po-icon-plus' }
  ];

  poAlert = inject(PoDialogService);

  tableActions = [
    {
      label: 'Editar',
      icon: 'po-icon-edit',
      action: this.editProduct.bind(this)
    },
    {
      label: 'Excluir',
      icon: 'po-icon-delete',
      action: this.openDialog.bind(this)
    }
  ];

  loading = false;
  totalItems = 0;
  filter: ProductFilterDTO = {
    pageNumber: 1,
    pageSize: 10
  };

  constructor() {
  }
  
  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(loadMore: boolean = false): void {
    this.loading = true;

    this.productsService
      .get(this.filter)
      .subscribe({
        next: (response) => {
          this.totalItems = response.data.totalCount;
          this.products = loadMore ? [...this.products, ...response.data.items] : response.data.items;
          this.loading = false;
        },
        error: () => {
          console.log('Erro ao carregar lista de produtos');
          this.loading = false;
        }
      });
  }

  search(): void {
    this.resetFilters();
    if (this.searchName)
      this.filter.name = this.searchName;

    if (this.searchCode)
      this.filter.code = this.searchCode;
    
    this.loadProducts();
  }

  resetFilters(): void {
    this.filter = {
      pageNumber: 1,
      pageSize: 10
    };
  }

  filteredProducts(): ProductDTO[] {
    return this.products
      .filter(p =>
        p.name.toLowerCase().includes(this.searchName.toLowerCase()) ||
        p.code.toLowerCase().includes(this.searchCode.toLowerCase())
      );
  }

  editProduct(product: any) {
    console.log('Editar produto', product);
    if (product && product.id) this.router.navigate([`/products/${product.id}`]);
  }

  openDialog(product: any): void {
    console.log('Excluir produto', product);
    this.poAlert.confirm({
      componentsSize: 'medium',
      literals: { confirm: 'Confirmar', cancel: 'Cancelar'},
      title: "Confirmar",
      message: "Tem certeza que deseja excluir o registro?",
      confirm: () => this.deleteProduct(product.id),
      cancel: () => {},
      close: () => {}
    });
  }

  deleteProduct(id?: string): void {
    if (!id) return;

    this.productsService.delete(id).subscribe({
        next: () => {
          this.loadProducts();
        },
        error: () => console.log('Erro ao excluir o produto')
      });
  }

  onSortChange(sort: PoTableColumnSort) {
    this.filter.pageNumber = 1;
    if (sort.column?.property)
      this.filter.sortBy = capitalizeFirstLetter(sort.column.property);
    this.filter.sortDirection = sort.type === PoTableColumnSortType.Ascending ? 'asc' : 'desc';
    this.loadProducts();
  }

  onShowMore() {
    this.filter.pageNumber++;
    this.loadProducts(true);
  }
}
