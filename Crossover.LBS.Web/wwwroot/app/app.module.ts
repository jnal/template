import './rxjs-extensions';

import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';

// Imports for loading & configuring the in-memory web api
//import { InMemoryWebApiModule } from 'angular-in-memory-web-api';
//import { InMemoryDataService }  from './in-memory-data.service';

import { AppComponent }         from './app.component';
import { MachinesComponent } from './machines.component';
import { BackupConfigComponent }  from './backup-config.component';
import { LbsService } from './lbs.service';
import { Ng2DatetimePickerModule } from 'ng2-datetime-picker';
import { LogsComponent } from './logs.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    //InMemoryWebApiModule.forRoot(InMemoryDataService),
      AppRoutingModule,
      Ng2DatetimePickerModule
  ],
  declarations: [
    AppComponent,
      BackupConfigComponent,
      MachinesComponent,
      LogsComponent
  ],
  providers: [ LbsService, { provide: LocationStrategy, useClass: HashLocationStrategy } ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }


/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/