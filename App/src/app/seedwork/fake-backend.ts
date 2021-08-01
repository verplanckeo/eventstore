import { registerLocaleData } from "@angular/common";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HTTP_INTERCEPTORS, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { stringify } from "@angular/compiler/src/util";

import { Observable, of, throwError } from "rxjs";
import { delay, dematerialize, materialize } from "rxjs/operators";

import { Guid } from "guid-typescript";

import { apiUrls, localStorageKeys } from "./constants";
import { User } from "../models";

// list of 'registered' users in local storage
let users = JSON.parse(localStorage.getItem(localStorageKeys.users)) || [];

@Injectable()
export class FakeBackendInterceptor implements HttpInterceptor{
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        const { url, method, headers, body } = request;

        return handleRoute();

        function handleRoute(){
            switch(true){
                case url === apiUrls.user.register && method === 'POST':
                    return register();
                case url === apiUrls.user.login && method === "POST":
                    return login();
                case url === apiUrls.user.users && method === "GET":
                    return getUsers();
                case url.match(`${apiUrls.user.users}/\[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$`) && method === "GET": //http://localhost:4000/users/{guid}
                    return getUserById();
                case url.match(`${apiUrls.user.users}/\[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$`) && method === "DELETE": //http://localhost:4000/users/{guid}
                    return deleteUser();
                case url.match(`${apiUrls.user.users}/\[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$`) && method === "PUT": //http://localhost:4000/users/{guid}
                    return updateUser();
                default:
                    // pass through all other requests
                    return next.handle(request);
            }
        }

        function register(): Observable<HttpResponse<undefined>>{
            const user = body;

            if(users.find((x: { userName: string; }) => x.userName === user.userName)){
                return error(`User with ${user.userName} already exists`);
            }

            user.id = Guid.raw();
            users.push(user);
            localStorage.setItem(localStorageKeys.users, JSON.stringify(users));
            return ok();
        }

        function login(): Observable<HttpResponse<User>>{
            const { userName, password } = body;
            let user = users.find((u: { userName: string, password: string}) => u.userName === userName && u.password === password);

            if(!user) return error('Username or password is incorrect');

            return ok({
                ...user,
                token: 'fake-jwt-token'
            });
        }

        // Functional helper methods

        function isLoggedIn(){
            return headers.get('Authorization') === 'Bearer fake-jwt-token';
        }

        function userDetails(user: User){
            const { id, userName, firstName, lastName } = user;
            return { id, userName, firstName, lastName };
        }

        function getUsers(): Observable<HttpResponse<User[]>>{
            if(!isLoggedIn()) return unauthorized();
            return ok(users);
        }

        function getUserById(): Observable<HttpResponse<User>>{
          if(!isLoggedIn()) return unauthorized();

          const id = getIdFromUrl();
          let user = users.find((u: { id: string }) => u.id === id);

          return ok(user);
        }

        function updateUser(){

          const id = getIdFromUrl();
          let user = users.find((u: { id: string }) => u.id === id); //variable user is a reference, hence we don't need to re-assign it to the collection

          let updatedUser =  JSON.parse(body);

          Object.assign(user, updatedUser);
          localStorage.setItem(localStorageKeys.users, JSON.stringify(users));

          return ok();
        }

        function deleteUser(){
          if(!isLoggedIn()) return unauthorized();

          var splittedUrl = url.split('/');
          var id = splittedUrl[splittedUrl.length -1];
          users = users.filter((u: { id: string }) => u.id !== id);

          localStorage.setItem(localStorageKeys.users, JSON.stringify(users));
          return ok();
        }

        function getIdFromUrl(){
          var splittedUrl = url.split('/');
          return splittedUrl[splittedUrl.length -1];
        }


        // Http helper methods

        function ok(body?: undefined) {
            return of(new HttpResponse({ status: 200, body }))
                .pipe(delay(1000)); // delay observable to simulate server api call
        }

        function error(message: string) {
            return throwError({ error: { message } })
                .pipe(materialize(), delay(1000), dematerialize()); // call materialize and dematerialize to ensure delay even if an error is thrown (https://github.com/Reactive-Extensions/RxJS/issues/648);
        }

        function unauthorized(){
            return throwError({ status: 401, error: { message: 'Unauthorized' }})
                .pipe(materialize(), delay(1000), dematerialize());
        }
    }
}

export const FakeBackendProvider = {
    // use fake backend in place of http service for backend-less development
    provide: HTTP_INTERCEPTORS,
    useClass: FakeBackendInterceptor,
    multi: true
}
