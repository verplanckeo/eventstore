import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Alert, AlertType } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AlertService {
  private subject = new Subject<Alert>();
  private defaultId = 'default-alert';

  // enable subscribing to alert observable
  onAlert(id = this.defaultId) : Observable<Alert>{
    return this.subject.asObservable().pipe(filter(x => x && x.id === id));
  }

  constructor() { }

  success(message: string, keepAfterRouteChange: boolean, fade: boolean): void;
  success(message: string, keepAfterRouteChange: boolean): void;
  success(message: string): void;
  success(message: string, args?:any): void{
    console.log(`SUCCES: ${message}`);

    this.alert(Alert.CreateAlert(AlertType.Success, message, args?.keepAfterRouteChange || true, args?.fade || true));
  }

  info(message: string, keepAfterRouteChange: boolean, fade: boolean): void;
  info(message: string, keepAfterRouteChange: boolean): void;
  info(message: string): void;
  info(message: string, args?:any): void{
    console.log(`INFO: ${message}`);

    this.alert(Alert.CreateAlert(AlertType.Info, message, args?.keepAfterRouteChange || true, args?.fade || true));
  }

  warning(message: string, keepAfterRouteChange: boolean, fade: boolean): void;
  warning(message: string, keepAfterRouteChange: boolean): void;
  warning(message: string): void;
  warning(message: string, args?:any): void{
    console.log(`WARN: ${message}`);

    this.alert(Alert.CreateAlert(AlertType.Warning, message, args?.keepAfterRouteChange || true, args?.fade || true));
  }

  error(message: string, keepAfterRouteChange: boolean, fade: boolean): void;
  error(message: string): void;
  error(message: string, args?:any): void{
    console.log(`ERR: ${message}`);

    this.alert(Alert.CreateAlert(AlertType.Error, message, args?.keepAfterRouteChange || true, args?.fade || true));
  }

  clear(id: string = this.defaultId){
    this.subject.next(Alert.CreateEmptyAlert(id));
  }

  private alert(alert:Alert): void{
    alert.id = alert.id || this.defaultId;

    console.log(alert);

    this.subject.next(alert);
  }
}
