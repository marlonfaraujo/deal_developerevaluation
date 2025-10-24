import { Routes } from '@angular/router';

export const routes: Routes = [
    
    { path: '', redirectTo: '/products', pathMatch: 'full' },
    {
        path: 'products',
        loadChildren: () =>
            import('./components/product/product.routes').then(m => m.PRODUCT_ROUTES),
    },
    { path: '**', redirectTo: '/products' }
];
