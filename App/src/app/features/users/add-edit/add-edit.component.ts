import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';

import { User } from 'src/app/models';
import { applicationUrls } from 'src/app/seedwork';
import { AccountService, AlertService } from 'src/app/services';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.css']
})
export class AddEditComponent implements OnInit {
  form: FormGroup;
  id:string; // id of the user we're editting
  isAddMode: boolean;

  // using booleans to set an application state is not a very good practice
  // booleans should be derived from a state
  // TODO: refactor to using a state enum
  loading: boolean = false;
  submitted:boolean = false;



  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private alertService: AlertService,
  ) { }

  // convenience getter for accessing form fields
  get f() { return this.form.controls; }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;

    this.setValidators();

    if(!this.isAddMode){
      this.accountService.getById(this.id)
        .pipe(first())
        .subscribe(user => {
          console.log(user);
          this.form.patchValue(user);
        }); //It accepts an object with control names as keys, and does its best to match the values to the correct controls in the group.
    }
  }

  onSubmit(): void{
    this.submitted = true;

    this.alertService.clear();

    if(this.form.invalid){
      this.alertService.warning("Please fill out the form correctly.");
      return;
    }

    this.loading = true;

    if(this.isAddMode){
      this.createUser();
    }else{
      this.updateUser();
    }
  }

  private setValidators(): void{
    const passwordValidators = [Validators.minLength(6)];
    if(this.isAddMode){
      passwordValidators.push(Validators.required);
    }

    this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      userName: ['', Validators.required],
      password: ['', passwordValidators]
    });
  }

  private getUserModelFromForm(): User{
    return User.CreateUser(this.f.userName.value, this.f.password.value, this.f.firstName.value, this.f.lastName.value);
  }

  private createUser(){
    const user = this.getUserModelFromForm();

    // this.accountService.register(this.form.value).pipe(first()).subscribe({ next: () => {}, error: error => {} });
    // above example is the way more experienced front-end developers do it, I'm learning front-end so I keep it as close as possible to my
    // .NET Backend practices (for now)
    this.accountService.register(user)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success(`User (${user.userName}) registered successfully`, true);
          this.router.navigate(applicationUrls.users);
        },
        error: error => {
          this.alertService.error(error);
          this.loading = false;
        }
      })
    }

  private updateUser(){
    // This also works, it dynamically maps the properties of the form to those of the type in the arguments
    //this.accountService.update(this.id, this.form.value);

    const user = this.getUserModelFromForm();
    this.accountService.update(this.id, user)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success(`User (${user.userName}) updated successfully`, true);
          //this.router.navigate(['../../'], { relativeTo: this.route } );
          this.router.navigate(applicationUrls.users);
        },
        error: error => {
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }
}
