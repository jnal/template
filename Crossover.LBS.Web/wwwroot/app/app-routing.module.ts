import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MachinesComponent }      from './machines.component';
import { BackupConfigComponent }  from './backup-config.component';
import { LogsComponent } from './logs.component';

const routes: Routes = [
    { path: '', redirectTo: '/machines', pathMatch: 'full' },
    { path: 'backupconfig/:method/:id', component: BackupConfigComponent },
    { path: 'logs/:id', component: LogsComponent },
  { path: 'machines', component: MachinesComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}


/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/