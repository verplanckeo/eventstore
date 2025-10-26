import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HomeComponent } from './features/home/home.component';
import { AlertComponent } from './features/alert/alert.component';

//can not add these interceptors to the index file because it would add circular dependencies
import { JwtInterceptor } from './seedwork/jwt.interceptor';
import { ErrorInterceptor } from './seedwork/error.interceptor';
import { HttpHeadersInterceptor } from './seedwork/http.interceptor';
import { FakeBackendProvider } from './seedwork/fake-backend';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  providers: [
    provideHttpClient(),
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HttpHeadersInterceptor, multi: true },
    //FakeBackendProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
