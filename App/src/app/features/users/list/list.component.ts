import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService } from '../../../services';
import { User } from '../../../models';

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
    this.accountService.getAll()
      .pipe(first())
      .subscribe(users => this.users = users);
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
