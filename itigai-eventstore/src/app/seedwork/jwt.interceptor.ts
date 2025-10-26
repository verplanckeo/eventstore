import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { AccountService } from "../services";
import { apiUrls, applicationUrls } from '../seedwork';
import { environment } from '../../environments/environment';


@Injectable()
export class JwtInterceptor implements HttpInterceptor{

  constructor(private accountService: AccountService){}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
    const isApiUrl = request.url.startsWith(environment.apiUrl);
    const user = this.accountService.userValue;
    const isLoggedIn = user && user.token;

    if(isApiUrl && isLoggedIn){
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${user.token}`
        }
      });
    }

    return next.handle(request);
  }

}
