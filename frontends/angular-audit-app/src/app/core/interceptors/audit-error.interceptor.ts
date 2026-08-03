import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError, retry } from 'rxjs';

export const auditErrorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    retry({ count: 1, delay: 1000 }),
    catchError((error: HttpErrorResponse) => {
        let customErrorMessage = 'An unexpected error occurred while processing your request.';

        if (error.status === 0) {
        customErrorMessage = 'Unable to connect to the server. Please verify network connectivity.';
        } else if (error.status === 400) {
        customErrorMessage = 'Bad Request: Invalid payload format or validation parameters.';
        } else if (error.status === 401) {
        customErrorMessage = 'Unauthorized: Authentication credentials missing or expired.';
        } else if (error.status === 403) {
        customErrorMessage = 'Forbidden: You do not have permission to perform this action.';
        } else if (error.status === 404) {
        customErrorMessage = 'Resource Not Found.';
        } else if (error.status === 409) {
        customErrorMessage = 'Conflict: The request could not be completed due to a state conflict.';
        } else if (error.status >= 500) {
        customErrorMessage = 'Internal Server Error: An unexpected failure occurred on the server.';
        }
        console.error(`[HTTP Error ${error.status}]:`, error.message);
      
        return throwError(() => new Error(customErrorMessage));
    })
  );
};