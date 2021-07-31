import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";


@Injectable()
export class HttpHeadersInterceptor implements HttpInterceptor{

  constructor(){

  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
    request = request.clone({
      setHeaders: {
        Accept: 'application/json'
      }
    });

    return next.handle(request);
  }

}
