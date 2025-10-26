import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Alert, AlertType } from '../../models';
import { AlertService } from '../../services';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent implements OnInit, OnDestroy {

  @Input() id = 'default-alert';
  @Input() fade = true;

  alerts: Alert[] = [];
  alertSubscription: Subscription;
  routeSubscription: Subscription;

  constructor(
    private alertService: AlertService,
    private router: Router
  ) { }

  ngOnInit(): void {
    //subscribe to new alert messages
    this.alertSubscription = this.alertService.onAlert(this.id)
      .subscribe(alert => {
        //clear alerts when an empty alert is received
        if(!alert.message){
          //below, in this method, you see we clear the alerts when we navigate away, that is why we only keep the alerts after we change our route
          this.alerts = this.alerts.filter(a => a.keepAfterRouteChange);

          // 'keepAfterRouteChange' flag after reset
          this.alerts.forEach(x => x.keepAfterRouteChange = false);
          return;
        }

        //add alert to array
        this.alerts.push(alert);

        //auto close alert
        if(alert.autoClose){
          setTimeout(() => { this.removeAlert(alert); }, environment.alertTimer);
        }
      });

      this.routeSubscription = this.router.events.subscribe(evt => {
        if(evt instanceof NavigationStart){
          this.alertService.clear(this.id);
        }
      });
  }

  ngOnDestroy(): void{
    // unsubscribe to avoid memory leaks
    this.alertSubscription.unsubscribe();
    this.routeSubscription.unsubscribe();
  }

  removeAlert(alert: Alert){
    if(!this.alerts.includes(alert)) return;

    if(this.fade){

      alert.fade = true;

      // remove alert after faded out
      setTimeout(() => {
        this.alerts = this.alerts.filter(a => a !== alert);
      }, 250);

    }else{
      // remove alert
      this.alerts = this.alerts.filter(a => a !== alert);
    }
  }

  cssClass(alert: Alert){
    if(!alert) return '';

    const classes = ['alert', 'alert-dismisable', 'mt-4', 'container'];

    const alertTypeClass = {
      [AlertType.Success]: 'alert alert-success',
      [AlertType.Info]: 'alert alert-info',
      [AlertType.Warning]: 'alert alert-warning',
      [AlertType.Error]: 'alert alert-danger'
    }

    classes.push(alertTypeClass[alert.type]);

    return classes.join(' ');
  }

}
