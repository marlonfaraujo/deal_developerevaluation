import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ApiResponse } from '../../shared/apiResponse.dto';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
        console.error('Erro HTTP', error);
        const apiResponse : ApiResponse = {
          success: false,
          message: error.error?.message || 'Ocorreu um erro inesperado.'
        };
        return throwError(() => apiResponse);
      })
  );
};

