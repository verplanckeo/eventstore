import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";

import { AccountService } from "../services";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor{

  constructor(private accountService: AccountService){}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{

    return next.handle(request).pipe(catchError(
      err => {
        if([401, 403].includes(err.status)){
          //this.accountService.logout();
        }

        const error = err.error?.message || err.statusText;
        console.error(error);
        return throwError(error);
      }
    ));
  }
}
