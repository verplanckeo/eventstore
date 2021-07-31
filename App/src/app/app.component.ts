import { Component, OnInit } from '@angular/core';
import { User } from './models';
import { applicationUrls } from './seedwork';
import { AccountService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'itigai-user-app';

  user: User;

  linkHome = applicationUrls.root;
  linkUsers = applicationUrls.users;
  linkVideo = applicationUrls.video;

  constructor(
    private accountService: AccountService
  ){ }

  ngOnInit(): void{
    this.accountService.user.subscribe(x => this.user = x);
  }

  logout(): void{
    this.accountService.logout();
  }
}
