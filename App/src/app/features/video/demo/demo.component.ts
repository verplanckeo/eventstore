import { Component, OnInit } from '@angular/core';
import { ɵnoSideEffects } from '@angular/core/src/r3_symbols';
//import * as videojs from 'video.js';

declare var videojs: any; //source js file is referenced in angular.json

@Component({
  selector: 'app-demo',
  templateUrl: './demo.component.html',
  styleUrls: ['./demo.component.css']
})
export class DemoComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    //this.loadVideo();
  }

  loadVideo(): void{
    var player = videojs('videoplayer', {
      resizeManager: true
    });
    player.vr({ projection: 'AUTO' });
  }

}
