import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout/layout.component';
import { AddEditComponent } from './add-edit/add-edit.component';



@NgModule({
  declarations: [
    LayoutComponent,
    AddEditComponent
  ],
  imports: [
    CommonModule
  ]
})
export class TicketsModule { }
