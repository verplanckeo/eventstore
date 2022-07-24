import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService } from '../../../services';
import { User } from '../../../models';
import { GetAllUsersResponse } from 'src/app/services/account.interface';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  users: User[] = null;

  constructor(
    private accountService: AccountService
  ) { }

  ngOnInit(): void {
    let local = this.accountService.getAll()
      .pipe(first())
      .subscribe((response: GetAllUsersResponse) => {
        this.users = Array.from(response.users.map(u => User.CreateUser(u.userName, u.password, u.firstName, u.lastName, u.aggregateRootId)));
        console.log(this.users);
      });

      console.log(local);
      console.log(this.users);
  }

  deleteUser(user: User): void{

    var idValue = user.id;
    user.isDeleting = true;

    this.accountService.delete(idValue)
      .pipe(first())
      .subscribe(() => {
        this.users = this.users.filter(u => u.id !== idValue)
      });
  }
}
