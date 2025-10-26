import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';

import { AccountService, AlertService } from '../../../services';
import { User } from '../../../models';
import { applicationUrls } from '../../../seedwork';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private alertService: AlertService
  ) { }

  passwordPattern = "^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,12}$";

  // convenience getter for accessing the form fields
  // we can write a getter for each field as well: get firstName() { return this.form.get('firstName')};
  get f() { return this.form.controls; }

  // convenience getter for accessing form values
  get formValue() { return this.form.value };

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      userName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void{
    this.submitted = true;

    // stop here when form is not valid
    if(this.form.invalid){
      console.log('invalid');
      return;
    }

    this.loading = true;

    let user = User.CreateUser(this.formValue.userName, this.formValue.password, this.formValue.firstName, this.formValue.lastName);

    this.accountService.register(user)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('User registered!');
          this.router.navigate(applicationUrls.account.login);
        },
        error: error => {
          console.log("dafuq", error);
          console.log(this.alertService);
          this.alertService.error(error.message);
          this.loading = false;
        }
      });
  }

}
