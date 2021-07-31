import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VideoRoutingModule } from './video-routing.module';

import { DemoComponent } from './demo/demo.component';
import { LayoutComponent } from './layout/layout.component';



@NgModule({
  imports: [
    CommonModule,
    VideoRoutingModule
  ],
  declarations: [
    DemoComponent,
    LayoutComponent
  ]
})
export class VideoModule { }
