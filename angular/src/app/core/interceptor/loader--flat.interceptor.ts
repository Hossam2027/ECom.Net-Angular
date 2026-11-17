import { HttpInterceptorFn } from '@angular/common/http';

export const loaderFlatInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req);
};
