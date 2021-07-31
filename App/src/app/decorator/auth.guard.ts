import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, RouterState } from '@angular/router';

import { AccountService } from '../services';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private accountService: AccountService     
    ){}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot){
        //add logic to check if user is authenticated
        const user = this.accountService.userValue;
        if(user)
        {
            return true;
        }
        
        // when not logged in, the user is redirected towards the login page
        this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}