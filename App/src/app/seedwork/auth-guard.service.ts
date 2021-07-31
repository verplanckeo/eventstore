import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { applicationUrls } from './constants';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(
    private router: Router,
    private accountService: AccountService
  ) { } 

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot){
    const user = this.accountService.userValue;
    if(user){
      // user is authenticated / authorized -> return true
      return true;
    }

    // not logged in, so redirect user to login page with the return url
    this.router.navigate(applicationUrls.account.login, { queryParams: { returnUrl: state.url } });
    return false;
  }
}
