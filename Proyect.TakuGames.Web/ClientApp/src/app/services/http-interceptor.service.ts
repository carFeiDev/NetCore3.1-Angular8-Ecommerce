import { HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpInterceptorService {

  constructor() { }
  intercept(request: HttpRequest<any>, newRequest: HttpHandler): Observable<HttpEvent<any>> {
    const headerToken = localStorage.getItem('authToken');
    if (headerToken) {
      request = request.clone({
        setHeaders: {
          Authorization: 'Bearer ${headerToken}'
        }
      });
    }
    return newRequest.handle(request);
  }
}
